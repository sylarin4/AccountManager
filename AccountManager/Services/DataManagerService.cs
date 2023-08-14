using Newtonsoft.Json;
using AccountManager.Models;
using System.Xml.Serialization;
using System.Xml;

namespace AccountManager.Services
{
    public class DataManagerService : IDataManagerService
    {
        private string _dataFilePath;
        private List<Account>? _loadedAccounts;
        public DataManagerService(String dataFilePath = "Data/data.json")
        {
            _dataFilePath = dataFilePath;
            _loadedAccounts = null;
        }

        public List<Account> LoadAccounts()
        {
            if (_loadedAccounts == null)
            {
                List<Account>? accounts = new List<Account>();

                try
                {
                    string jsonData = File.ReadAllText(_dataFilePath);

                    accounts = JsonConvert.DeserializeObject<List<Account>>(jsonData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"The \"{_dataFilePath}\" file was not found.");
                }
                catch (JsonException)
                {
                    Console.WriteLine("The structure of data.json content is not valid.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while reading the data: " + ex.Message);
                }
                _loadedAccounts = accounts ?? new List<Account>();
            }

            return _loadedAccounts;
        }

        public Account? GetAccountById(int id)
        {
            List<Account> accounts = LoadAccounts();
            Account? account = accounts.FirstOrDefault(a => a.Id == id);
            return account;
        }

        public void UpdateAccount(Account updatedAccount)
        {
            List<Account> accounts = LoadAccounts();
            Account? existingAccount = accounts.FirstOrDefault(a => a.Id == updatedAccount.Id);
            if (existingAccount != null)
            {
                existingAccount.Username = updatedAccount.Username;
                existingAccount.Firstname = updatedAccount.Firstname;
                existingAccount.Lastname = updatedAccount.Lastname;
                existingAccount.Birthplace = updatedAccount.Birthplace;
                existingAccount.Residence = updatedAccount.Residence;
                existingAccount.Password = updatedAccount.Password;
            }

            SaveAccounts(accounts);
            _loadedAccounts = null;
        }

        private void SaveAccounts(List<Account> accounts)
        {
            string jsonData = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            using (var writer = new StreamWriter(_dataFilePath))
            {
                writer.Write(jsonData);
            }
        }

        public bool validateAccount(string userName, string password)
        {
            List<Account> accounts = LoadAccounts();
            return accounts.Any(account => account.Username == userName && account.Password == password);
        }
    }
}
