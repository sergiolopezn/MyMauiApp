using System.Collections.Generic;
using System.Threading.Tasks;
using MyMauiApp.Data.Repository.Entities;

namespace MyMauiApp.Data.Interfaces;

public interface IApiRequestRepository
{
    Task<List<AnimalsModel>> GetDataAsync(string page, string limit);
}
