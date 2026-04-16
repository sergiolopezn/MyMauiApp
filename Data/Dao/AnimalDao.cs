using System;
using MyMauiApp.Data.Local;
using MyMauiApp.Data.Local.Entities;
using SQLite;

namespace MyMauiApp.Data.Dao;

public class AnimalDao
{
    private readonly AppDataBase _database;
    private readonly SQLiteConnection _connection;
    public AnimalDao(AppDataBase database)
    {
        _database = database;
        _connection = database.Connection;
    }

    // create - insert a new animal
    public int Insert(AnimalEntity animal)
    {
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));
        return _connection.Insert(animal);
    }

    // read - get animal by id
    public AnimalEntity GetById(int id)
    {
        return _connection.Table<AnimalEntity>().FirstOrDefault(a => a.Id == id);
    }

    // read - get all animals
    public List<AnimalEntity> GetAll()
    {
        return _connection.Table<AnimalEntity>().ToList();
    }

    // update - update existing animal
    public int Update(AnimalEntity animal)
    {
        if (animal == null)
            throw new ArgumentNullException(nameof(animal));

        if (animal.Id <= 0)
            throw new ArgumentException("Animal ID must be greater than 0 for update operation", nameof(animal.Id));

        return _connection.Update(animal);
    }

    // delete - delete animal by id
    public int Delete(int id)
    {
        var animal = GetById(id);
        if (animal != null)
        {
            return _connection.Delete(animal);
        }
        return 0; // No rows deleted
    }

    // insert all animals
    public int InsertAll(List<AnimalEntity> animals)
    {
        if (animals == null || animals.Count == 0)
            throw new ArgumentException("Animal list cannot be null or empty", nameof(animals));

        return _connection.InsertAll(animals);
    }

    // delete all animals
    public int DeleteAll()
    {
        List<AnimalEntity> allAnimals = GetAll();
        if (allAnimals != null && allAnimals.Count > 0)
        {
            return _connection.DeleteAll<AnimalEntity>();
        }
        return 0; // No rows deleted
    }
}
