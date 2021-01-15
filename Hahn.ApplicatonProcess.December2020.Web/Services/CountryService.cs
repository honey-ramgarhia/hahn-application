using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Services
{
    public class CountryService
    {
        private readonly HttpClient httpClient;

        public CountryService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://restcountries.eu/rest/v2/");
            this.httpClient = httpClient;
        }

        public async Task<bool> Exists(string countryName)
        {
            var response = await httpClient.GetAsync($"name/{countryName}?fullText=true");
            return response.IsSuccessStatusCode;
        }
    }
}
