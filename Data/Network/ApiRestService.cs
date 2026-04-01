using System.Text.Json;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.Data.Network;

public class ApiRestService
{
    HttpClient _httpClient;
    JsonSerializerOptions _jsonSerializerOptions;
    string _baseAddress = "https://api.api-ninjas.com/v1/cats"; // Your API base address
    public ApiRestService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", "4SZ5WfrPuGTotzkXTdowqD4xr7qrqBPvmRULAP34"); // Replace with your actual API key
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<AnimalsData>> GetAsync(string page, string limit)
    {
        List<AnimalsData> result = new ();
        try
        {
            var endpoint = $"{_baseAddress}?name=a";
            Console.WriteLine($"Request {endpoint}");
            var response = await _httpClient.GetAsync(endpoint);
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response {json}");
            result = JsonSerializer.Deserialize<List<AnimalsData>>(json, _jsonSerializerOptions) ?? new ();
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log them, show a message to the user, etc.)
            Console.WriteLine($"HTTP Request error: {ex.Message}");
            throw new Exception("An error occurred while fetching data from the API.", ex);
        }
        return result;
    }
}

