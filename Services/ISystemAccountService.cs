using BusinessObjects;

namespace Services;

public interface ISystemAccountService
{
    void AddAccount(SystemAccount sa);
    void UpdateAccount(int id, SystemAccount sa);
    void DeleteAccount(int id);
    SystemAccount GetAccountByID(int id);
    List<SystemAccount> GetAccounts();

}