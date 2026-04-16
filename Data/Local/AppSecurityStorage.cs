using System;

namespace MyMauiApp.Data.Local;

public class AppSecurityStorage
{
    // save a string value in secure storage
    public async Task SaveStringAsync(string key, string value)
    {
        await SecureStorage.SetAsync(key, value);
    }

    // get a string value from secure storage
    public async Task<string> GetStringAsync(string key, string defaultValue = "")
    {
        try
        {
            return await SecureStorage.GetAsync(key) ?? defaultValue;
        }
        catch (Exception)
        {
            // Handle exceptions such as when the device doesn't support secure storage
            return defaultValue;
        }
    }

    // remove a value from secure storage
    public void Remove(string key)
    {
        SecureStorage.Remove(key);
    }

    // clear all values from secure storage
    public void Clear()
    {
        SecureStorage.RemoveAll();
    }
}
