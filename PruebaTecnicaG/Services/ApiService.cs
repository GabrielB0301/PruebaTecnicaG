using PruebaTecnicaG.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PruebaTecnicaG.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string Token = "ae8bad44-7348-11ee-b962-0242ac120002";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
            _httpClient.BaseAddress = new Uri("https://mainserver.ziursoftware.com/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Token);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Documento>> GetDocumentosAsync()
        {
            try
            {
                
                var endpoint = "Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos";

                Console.WriteLine($"Solicitando datos a: {_httpClient.BaseAddress}{endpoint}");

                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error en la API: {response.StatusCode}");
                    return new List<Documento>();
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta recibida: {content}"); 

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var documentos = JsonSerializer.Deserialize<List<Documento>>(content, options);
                Console.WriteLine($"Documentos deserializados: {documentos?.Count ?? 0}");

                return documentos ?? new List<Documento>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al obtener documentos: {ex.Message}");
                return new List<Documento>();
            }
        }
    }
}