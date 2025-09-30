using BusinessObjects;

namespace Repositories;

public class SystemAccountRepository : ISystemAccountRepository
{
    public void AddAccount(SystemAccount sa) => SystemAccountDAO.AddAccount(sa);
    public void UpdateAccount(int id, SystemAccount sa) => SystemAccountDAO.UpdateAccount(id, sa);
    public void DeleteAccount(int id) => SystemAccountDAO.DeleteAccount(id);
    public SystemAccount GetAccountByID(int id) => SystemAccountDAO.GetAccountByID(id);
    public List<SystemAccount> GetAccounts() => SystemAccountDAO.GetAccounts();


}