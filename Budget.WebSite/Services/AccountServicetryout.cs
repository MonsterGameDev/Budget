using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Budget.WebSite.Models;
using Newtonsoft.Json;

namespace Budget.WebSite.Services
{
    public class AccountService : IAccountService
    {
        private HttpClient _client;
        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Account> GetAccountAsync(string path)
        {
            Account account= null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                account = await response.Content.ReadAsAsync<Account>();
            }
            return account;

            
            //private async Task<T> GetAsync<T>(Uri requestUrl)
            //{

            //    var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            //    response.EnsureSuccessStatusCode();
            //    var data = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<T>(data);
            //}
        }


        // public Task<List<Account>> GetAccountsAsync(bool includeSubAccounts)
        // {
            //public async Task<IEnumerable<string>> GetRepos()
            //{
            //    var response = await _httpClient.GetAsync("aspnet/repos");

            //    response.EnsureSuccessStatusCode();

            //    var result = await response.Content
            //        .ReadAsAsync<IEnumerable<string>>();

            //    return result;
            //}
        // }









        public Task CreateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccountAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        
       

        public Task UpdateAccountAsync(int accountId, Account account)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAccountsAsync(bool includeSubAccounts)
        {
            throw new NotImplementedException();
        }
    }
}
