using System;
using MyMauiApp.Data.Network;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.Data;

public class ApiRequestRepository
{
    private readonly ApiRestService _apiRestService;
    
    /// <summary>
    /// Constructor that receives ApiRestService through dependency injection
    /// </summary>
    /// <param name="apiRestService">The API service instance provided by the DI container</param>
    public ApiRequestRepository(ApiRestService apiRestService)
    {
        _apiRestService = apiRestService;
    }
    
    public async Task<ApiRestResponse> GetDataAsync(string page, string limit)
    {
        return await _apiRestService.GetAsync(page, limit);
    }
}
