using AccountManager.Models;

namespace AccountManager.Services
{
    public interface IDataManagerService
    {
        List<Account> LoadAccounts();
        Account? GetAccountById(int id);
        public void UpdateAccount(Account updatedAccount);
        public bool validateAccount(string userName, string password);
    }
}
