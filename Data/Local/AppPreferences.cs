using System;
using MyMauiApp.Data.Interfaces;

namespace MyMauiApp.Data.Local;

public class AppPreferences: IAppPreferences
{

    // save a string value in preferences
    public void SaveString(string key, string value)
    {
        Preferences.Set(key, value);
    }

    // get a string value from preferences
    public string GetString(string key, string defaultValue = "")
    {
        return Preferences.Get(key, defaultValue);
    }

    // remove a value from preferences
    public void Remove(string key)
    {
        Preferences.Remove(key);
    }

    // clear all preferences
    public void Clear()
    {
        Preferences.Clear();
    }
}
