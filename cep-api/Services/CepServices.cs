using cep_api.Model;
using System.Threading;
using System.Text.Json;
using System.Net.Http;

namespace cep_api.Services
{
    public class CepServices
    {
        private readonly HttpClient _httpClient;

        public CepServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://brasilapi.com.br/api/");
        }

        public async Task<CepModel> GetCepModelAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"cep/v1/{cep}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var fullData = JsonSerializer.Deserialize<CepModel>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (fullData != null)
                {
                    return new CepModel
                    {
                        Cep = fullData.Cep,
                        State = fullData.State,
                        City = fullData.City
                    };
                }
            }

            return null;
                
        }
    }
}
