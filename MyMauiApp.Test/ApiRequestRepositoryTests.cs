using NSubstitute;
using MyMauiApp.Data;
using MyMauiApp.Data.Interfaces;
using MyMauiApp.Data.Local.Entities;
using MyMauiApp.ViewModel.Entity;
using NSubstitute.ExceptionExtensions;

namespace MyMauiApp.Test;

public class ApiRequestRepositoryTests
{
    private readonly IApiRestService _apiRestServiceMock;
    private readonly IAnimalDao _animalDaoMock;
    private readonly IAppPreferences _appPreferencesMock;
    private readonly IAppSecurityStorage _appSecurityStorageMock;
    private readonly ApiRequestRepository _repository;

    public ApiRequestRepositoryTests()
    {
        _apiRestServiceMock = Substitute.For<IApiRestService>();
        _animalDaoMock = Substitute.For<IAnimalDao>();
        _appPreferencesMock = Substitute.For<IAppPreferences>();
        _appSecurityStorageMock = Substitute.For<IAppSecurityStorage>();
        _repository = new ApiRequestRepository(
            _apiRestServiceMock,
            _animalDaoMock,
            _appPreferencesMock,
            _appSecurityStorageMock);
    }

    [Fact]
    public async Task GetDataAsync_WhenApiReturnsData_ShouldUpdateLocalDatabaseAndReturnData()
    {
        // Arrange
        var apiData = new List<AnimalsData>
        {
            new AnimalsData { image_link = "link1", origin = "origin1", name = "name1" }
        };
        var localData = new List<AnimalEntity>
        {
            new AnimalEntity { Id = 1, imageLink = "link1", origin = "origin1", name = "name1" }
        };

        _apiRestServiceMock.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(apiData);
        _animalDaoMock.GetAll().Returns(localData);

        // Act
        var result = await _repository.GetDataAsync("1", "10");

        // Assert
        _animalDaoMock.Received(1).DeleteAll();
        _animalDaoMock.Received(1).InsertAll(Arg.Any<List<AnimalEntity>>());
        _appPreferencesMock.Received(1).SaveString("last_fetch_time", Arg.Any<string>());
        await _appSecurityStorageMock.Received(1).SaveStringAsync("last_fetch_time_secure", Arg.Any<string>());
        Assert.Single(result);
        Assert.Equal("name1", result[0].name);
    }

    [Fact]
    public async Task GetDataAsync_WhenApiReturnsNoData_ShouldReturnLocalData()
    {
        // Arrange
        var localData = new List<AnimalEntity>
        {
            new AnimalEntity { Id = 1, imageLink = "link1", origin = "origin1", name = "name1" }
        };

        _apiRestServiceMock.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(new List<AnimalsData>());
        _animalDaoMock.GetAll().Returns(localData);

        // Act
        var result = await _repository.GetDataAsync("1", "10");

        // Assert
        _animalDaoMock.DidNotReceive().DeleteAll();
        _animalDaoMock.DidNotReceive().InsertAll(Arg.Any<List<AnimalEntity>>());
        _appPreferencesMock.DidNotReceive().SaveString(Arg.Any<string>(), Arg.Any<string>());
        await _appSecurityStorageMock.DidNotReceive().SaveStringAsync(Arg.Any<string>(), Arg.Any<string>());
        Assert.Single(result);
        Assert.Equal("name1", result[0].name);
    }
    
    [Fact]
    public async Task GetDataAsync_WhenApiCallFails_ShouldReturnLocalData()
    {
        // Arrange
        var localData = new List<AnimalEntity>
        {
            new AnimalEntity { Id = 1, imageLink = "link1", origin = "origin1", name = "name1" }
        };

        _apiRestServiceMock.GetAsync(Arg.Any<string>(), Arg.Any<string>()).ThrowsAsync(new Exception("API call failed"));
        _animalDaoMock.GetAll().Returns(localData);

        // Act
        var result = await _repository.GetDataAsync("1", "10");

        // Assert
        _animalDaoMock.DidNotReceive().DeleteAll();
        _animalDaoMock.DidNotReceive().InsertAll(Arg.Any<List<AnimalEntity>>());
        _appPreferencesMock.DidNotReceive().SaveString(Arg.Any<string>(), Arg.Any<string>());
        await _appSecurityStorageMock.DidNotReceive().SaveStringAsync(Arg.Any<string>(), Arg.Any<string>());
        Assert.Single(result);
        Assert.Equal("name1", result[0].name);
    }

    [Fact]
    public async Task GetDataAsync_WhenBothApiAndLocalDataAreEmpty_ShouldReturnEmptyList()
    {
        // Arrange
        _apiRestServiceMock.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(new List<AnimalsData>());
        _animalDaoMock.GetAll().Returns(new List<AnimalEntity>());

        // Act
        var result = await _repository.GetDataAsync("1", "10");

        // Assert
        _animalDaoMock.DidNotReceive().DeleteAll();
        _animalDaoMock.DidNotReceive().InsertAll(Arg.Any<List<AnimalEntity>>());
        _appPreferencesMock.DidNotReceive().SaveString(Arg.Any<string>(), Arg.Any<string>());
        await _appSecurityStorageMock.DidNotReceive().SaveStringAsync(Arg.Any<string>(), Arg.Any<string>());
        Assert.Empty(result);
    }
}
