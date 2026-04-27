using System.Collections.Generic;
using System.Threading.Tasks;
using MyMauiApp.Data.Local.Entities;
using MyMauiApp.Data.Repository.Entities;
using MyMauiApp.ViewModel.Entity;

namespace MyMauiApp.Data.Interfaces;

public interface IApiRestService
{
    Task<List<AnimalsData>> GetAsync(string page, string limit);
}

public interface IAnimalDao
{
    int DeleteAll();
    int InsertAll(List<AnimalEntity> animals);
    List<AnimalEntity> GetAll();
}

public interface IAppPreferences
{
    void SaveString(string key, string value);
}

public interface IAppSecurityStorage
{
    Task SaveStringAsync(string key, string value);
}
