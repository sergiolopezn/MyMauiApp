using System;
using System.IO;
using Microsoft.Maui.Storage;
using MyMauiApp.Data.Local.Entities;
using SQLite;

namespace MyMauiApp.Data.Local;

public class AppDataBase : IDisposable
{
    private readonly SQLiteConnection _conn;
    private readonly int _targetVersion;

    public string DatabasePath { get; }
    public SQLiteConnection Connection => _conn;
    public int DatabaseVersion => _conn.ExecuteScalar<int>("PRAGMA user_version");

    public AppDataBase(string databaseName = "app.db", int targetVersion = 1)
    {
        _targetVersion = targetVersion;
        Directory.CreateDirectory(FileSystem.AppDataDirectory);
        DatabasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);

        _conn = new SQLiteConnection(DatabasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
        _conn.Execute("PRAGMA foreign_keys = ON;");

        Initialize();
    }

    private void Initialize()
    {
        if (DatabaseVersion == 0)
        {
            CreateSchema();
            SetDatabaseVersion(1);
        }

        if (DatabaseVersion < _targetVersion)
        {
            Migrate(DatabaseVersion, _targetVersion);
        }
        else if (DatabaseVersion > _targetVersion)
        {
            throw new InvalidOperationException($"Database version {DatabaseVersion} is newer than supported version {_targetVersion}.");
        }
    }

    private void CreateSchema()
    {
        _conn.CreateTable<User>();
        _conn.CreateTable<AnimalEntity>();
    }

    private void Migrate(int currentVersion, int targetVersion)
    {
        for (var nextVersion = currentVersion + 1; nextVersion <= targetVersion; nextVersion++)
        {
            ApplyMigration(nextVersion);
            SetDatabaseVersion(nextVersion);
        }
    }

    private void ApplyMigration(int version)
    {
        switch (version)
        {
            case 1:
                // Initial schema is created in CreateSchema().
                break;
            case 2:
                // Example migration:
                // _conn.Execute("ALTER TABLE user ADD COLUMN Phone TEXT;");
                break;
            default:
                throw new InvalidOperationException($"No migration defined for version {version}.");
        }
    }

    public void SetDatabaseVersion(int version)
    {
        _conn.Execute($"PRAGMA user_version = {version};");
    }

    public void Close()
    {
        _conn.Close();
    }

    public void Dispose()
    {
        Close();
        _conn.Dispose();
    }
}
