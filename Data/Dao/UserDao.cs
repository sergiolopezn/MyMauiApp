using System;
using System.Collections.Generic;
using System.Linq;
using MyMauiApp.Data.Local;
using MyMauiApp.Data.Local.Entities;
using SQLite;

namespace MyMauiApp.Data.Dao;

public class UserDao
{
    private readonly AppDataBase _database;
    private readonly SQLiteConnection _connection;

    public UserDao(AppDataBase database)
    {
        _database = database;
        _connection = database.Connection;
    }

    // CREATE - Insert a new user
    public int Insert(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.CreatedAt = DateTime.Now;
        return _connection.Insert(user);
    }

    // READ - Get user by ID
    public User GetById(int id)
    {
        return _connection.Table<User>().FirstOrDefault(u => u.Id == id);
    }

    // READ - Get all users
    public List<User> GetAll()
    {
        return _connection.Table<User>().ToList();
    }

    // UPDATE - Update existing user
    public int Update(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (user.Id <= 0)
            throw new ArgumentException("User ID must be greater than 0 for update operation", nameof(user.Id));

        return _connection.Update(user);
    }

    // DELETE - Delete user by ID
    public int Delete(int id)
    {
        var user = GetById(id);
        if (user != null)
        {
            return _connection.Delete(user);
        }
        return 0;
    }

    // DELETE - Delete user object
    public int Delete(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return _connection.Delete(user);
    }

    // UTILITY - Check if user exists by ID
    public bool Exists(int id)
    {
        return _connection.Table<User>().Any(u => u.Id == id);
    }

    // UTILITY - Get user count
    public int Count()
    {
        return _connection.Table<User>().Count();
    }

    // QUERY - Get users by name (partial match)
    public List<User> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<User>();

        return _connection.Table<User>()
            .Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // QUERY - Get users by email
    public User GetByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return _connection.Table<User>()
            .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    // QUERY - Get users created after a specific date
    public List<User> GetUsersCreatedAfter(DateTime date)
    {
        return _connection.Table<User>()
            .Where(u => u.CreatedAt > date)
            .OrderBy(u => u.CreatedAt)
            .ToList();
    }
}
