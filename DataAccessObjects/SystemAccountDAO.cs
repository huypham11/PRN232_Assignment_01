namespace BusinessObjects;

public class SystemAccountDAO
{
    public static List<SystemAccount> GetAccounts()
    {
        var list = new List<SystemAccount>();
        try
        {
            using var context = new FunewsManagementContext();
            list = context.SystemAccounts.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return list;
    }
    //Get account by ID
    public static SystemAccount GetAccountByID(int id)
    {
        SystemAccount account = null;
        try
        {
            using var context = new FunewsManagementContext();
            account = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return account;
    }
    //Add account
    public static void AddAccount(SystemAccount account)
    {
        try
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Add(account);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Update account
    public static void UpdateAccount(int id, SystemAccount account)
    {
        try
        {
            using var context = new FunewsManagementContext();
            var a = context.SystemAccounts.FirstOrDefault(x => x.AccountId == id);
            if (a == null)
            {
                throw new Exception("Account not found.");
            }
            a.AccountName = account.AccountName;
            a.AccountPassword = account.AccountPassword;
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Delete account
    public static void DeleteAccount(int id)
    {
        try
        {
            using var context = new FunewsManagementContext();
            var account = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            context.SystemAccounts.Remove(account);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}