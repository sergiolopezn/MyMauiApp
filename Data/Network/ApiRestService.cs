using System.Text.Json;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.Data.Network;

public class ApiRestService
{
    HttpClient _httpClient;
    JsonSerializerOptions _jsonSerializerOptions;
    string _baseAddress = "https://api.artic.edu/api/v1"; // Your API base address
    public ApiRestService()
    {
        _httpClient = new HttpClient();
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<ApiRestResponse> GetAsync(string page, string limit)
    {
        var result = new ApiRestResponse();
        try
        {
            var endpoint = $"{_baseAddress}/artworks?page={page}&limit={limit}";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Request {endpoint} API Response: {json}");
            result = JsonSerializer.Deserialize<ApiRestResponse>(json, _jsonSerializerOptions) ?? new ApiRestResponse();
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

