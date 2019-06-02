using Budget.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Budget.WebSite.Services
{
    public class AccountService: IAccountService
    {
        private string baseUrl = "https://localhost:44361/api/accounts/";
        private readonly IHttpClientFactory _clientFactory;
        public AccountService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Account>> GetAccountsAsync(Boolean includeSubAccounts)
        {
            string reqUrl = baseUrl + "?includeSubAccounts=" + includeSubAccounts ;

            var request = new HttpRequestMessage(HttpMethod.Get, reqUrl);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadAsAsync<List<Account>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Account> GetAccountAsync(int id, bool includeSubAccounts)
        {
            string reqUrl = baseUrl + id +"/?includeSubAccounts=" + includeSubAccounts;

            var request = new HttpRequestMessage(HttpMethod.Get, reqUrl);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadAsAsync<Account>();
                return result;
            }
            else
            {
                return null;
            }
        }
        

        public async Task<Uri> CreateAccountAsync(AccountForEditingDto account)
        {
            string reqUrl = baseUrl;

            //var request = new HttpRequestMessage(HttpMethod.Post, reqUrl);
            //request.Headers.Add("ContentType", "application/json");

            var client = _clientFactory.CreateClient();


            HttpResponseMessage response = await client.PostAsJsonAsync(reqUrl, account);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }

        
        public async Task UpdateAccountAsync(int accountId, AccountForUpdatingDto account)
        {
            string reqUrl = baseUrl + accountId;
            
            var client = _clientFactory.CreateClient();
            
            HttpResponseMessage response = await client.PutAsJsonAsync(reqUrl, account);

            response.EnsureSuccessStatusCode();
            
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            string reqUrl = baseUrl + accountId + "?deleteSubTree=true";

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.DeleteAsync(reqUrl);

            response.EnsureSuccessStatusCode();

        }
        
    }
}
