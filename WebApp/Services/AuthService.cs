using System.Net.Http.Json;
using BusinessObjects;
using System.Text.Json;
using Microsoft.JSInterop;

namespace WebApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly SystemAccountService _systemAccountService;
        private SystemAccount? _currentUser;
        private string? _adminEmail;
        private string? _adminPassword;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, SystemAccountService systemAccountService)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _systemAccountService = systemAccountService;
        }

        public SystemAccount? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;
        public bool IsAdmin => _currentUser?.AccountRole == null; // Admin không có role (null)
        public bool IsStaff => _currentUser?.AccountRole == 1;
        public bool IsLecturer => _currentUser?.AccountRole == 2;

        public async Task InitializeAsync()
        {
            await LoadAdminCredentials();
            await CheckAuthenticationAsync();
        }

        private async Task LoadAdminCredentials()
        {
            // Set fallback credentials first
            _adminEmail = "admin@funews.com";
            _adminPassword = "admin123";

            try
            {
                var response = await _httpClient.GetAsync("appsettings.json");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var config = JsonSerializer.Deserialize<JsonElement>(json);
                    if (config.TryGetProperty("AdminCredentials", out var adminCreds))
                    {
                        var email = adminCreds.GetProperty("Email").GetString();
                        var password = adminCreds.GetProperty("Password").GetString();
                        
                        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                        {
                            _adminEmail = email;
                            _adminPassword = password;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed, but keep fallback credentials
                Console.WriteLine($"Failed to load admin credentials from appsettings.json: {ex.Message}");
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Ensure admin credentials are loaded
            if (string.IsNullOrEmpty(_adminEmail))
            {
                await LoadAdminCredentials();
            }

            // Debug logging
            Console.WriteLine($"Login attempt - Email: {email}");
            Console.WriteLine($"Admin Email: {_adminEmail}");
            Console.WriteLine($"Passwords match: {password == _adminPassword}");

            // Check if admin login first
            if (email == _adminEmail && password == _adminPassword)
            {
                Console.WriteLine("Admin login successful via appsettings");
                _currentUser = new SystemAccount
                {
                    AccountId = 0,
                    AccountName = "Administrator",
                    AccountEmail = email,
                    AccountRole = null // Admin has no role
                };
                await SaveUserToStorage();
                return true;
            }

            // Also check hardcoded admin credentials as fallback
            if (email == "admin@funews.com" && password == "admin123")
            {
                Console.WriteLine("Admin login successful via hardcoded credentials");
                _currentUser = new SystemAccount
                {
                    AccountId = 0,
                    AccountName = "Administrator",
                    AccountEmail = email,
                    AccountRole = null // Admin has no role
                };
                await SaveUserToStorage();
                return true;
            }

            // Check staff/lecturer login
            try
            {
                var accounts = await _systemAccountService.GetSystemAccountsAsync();
                var user = accounts?.FirstOrDefault(a => a.AccountEmail == email && a.AccountPassword == password);
                
                if (user != null)
                {
                    _currentUser = user;
                    await SaveUserToStorage();
                    return true;
                }
            }
            catch (Exception)
            {
                // Handle API error
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            _currentUser = null;
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");
        }

        private async Task SaveUserToStorage()
        {
            if (_currentUser != null)
            {
                var userJson = JsonSerializer.Serialize(_currentUser);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", userJson);
            }
        }

        private async Task CheckAuthenticationAsync()
        {
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userJson))
                {
                    _currentUser = JsonSerializer.Deserialize<SystemAccount>(userJson);
                }
            }
            catch (Exception)
            {
                // Handle error
            }
        }

        public bool CanAccessCategories() => IsAdmin || IsStaff;
        public bool CanAccessTags() => IsAdmin;
        public bool CanAccessNewsArticles() => IsAdmin || IsStaff;
        public bool CanAccessAllSystemAccounts() => IsAdmin;
        public bool CanAccessOwnAccount() => IsAuthenticated;
        public bool CanEditAccount(short accountId) => IsAdmin || (IsStaff && _currentUser?.AccountId == accountId);
    }
}