using Repositories;

namespace Services;

public class SystemAccountService: ISystemAccountService
{
    private readonly ISystemAccountRepository iSystemAccountRepository;
    public SystemAccountService()
    {
        iSystemAccountRepository = new SystemAccountRepository();
    }
    public void AddAccount(BusinessObjects.SystemAccount sa) => iSystemAccountRepository.AddAccount(sa);
    public void UpdateAccount(int id, BusinessObjects.SystemAccount sa) => iSystemAccountRepository.UpdateAccount(id, sa);
    public void DeleteAccount(int id) => iSystemAccountRepository.DeleteAccount(id);
    public BusinessObjects.SystemAccount GetAccountByID(int id) => iSystemAccountRepository.GetAccountByID(id);
    public List<BusinessObjects.SystemAccount> GetAccounts() => iSystemAccountRepository.GetAccounts();

}