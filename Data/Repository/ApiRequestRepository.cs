using System;
using System.Linq;
using MyMauiApp.Data.Dao;
using MyMauiApp.Data.Local;
using MyMauiApp.Data.Local.Entities;
using MyMauiApp.Data.Network;
using MyMauiApp.Data.Repository.Entities;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.Data;

public class ApiRequestRepository
{
    private readonly ApiRestService _apiRestService;
    private readonly AnimalDao _animalDao;
    private readonly AppPreferences _appPreferences;
    private readonly AppSecurityStorage _appSecurityStorage;
    
    /// <summary>
    /// Constructor that receives ApiRestService and AnimalDao through dependency injection
    /// </summary>
    /// <param name="apiRestService">The API service instance provided by the DI container</param>
    /// <param name="animalDao">The animal DAO instance provided by the DI container</param>
    /// <param name="appPreferences">The app preferences instance provided by the DI container</param>
    public ApiRequestRepository(ApiRestService apiRestService, AnimalDao animalDao, AppPreferences appPreferences, AppSecurityStorage appSecurityStorage)
    {
        _apiRestService = apiRestService;
        _animalDao = animalDao;
        _appPreferences = appPreferences;
        _appSecurityStorage = appSecurityStorage;
    }
    
    // this class is a repository that handles API requests and local database operations for animal data
    // It allows offline access to the last fetched data and keeps track of when the data was last updated
    public async Task<List<AnimalsModel>> GetDataAsync(string page, string limit)
    {
        var result = await _apiRestService.GetAsync(page, limit);

        if (result != null && result.Count > 0)
        {
            _animalDao.DeleteAll();

            var animals = result.Select(item => new AnimalEntity
            {
                imageLink = item.image_link,
                origin = item.origin,
                name = item.name
            }).ToList();

            _animalDao.InsertAll(animals);
            _appPreferences.SaveString("last_fetch_time", DateTime.Now.ToString());
        }

        var localData = _animalDao.GetAll().Select(entity => new AnimalsModel
        {
            Id = entity.Id,
            imageLink = entity.imageLink,
            origin = entity.origin,
            name = entity.name
        }).ToList();
        return localData;
    }

}
