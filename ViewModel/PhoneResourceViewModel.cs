using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace MyMauiApp.ViewModel;

public partial class PhoneResourceViewModel: ObservableObject
{
    [ObservableProperty]
    private Location _location;

    [ObservableProperty]
    private ObservableCollection<string> _bluetoothDevices = new ObservableCollection<string>();

    [ObservableProperty]
    private bool _isScanning;

    private async Task<bool> EnsureBluetoothPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
        if (status == PermissionStatus.Granted)
            return true;

        if (Permissions.ShouldShowRationale<Permissions.Bluetooth>())
        {
            await Shell.Current.DisplayAlertAsync("Permission Required", "Bluetooth permission is required to scan devices.", "OK");
        }

        status = await Permissions.RequestAsync<Permissions.Bluetooth>();
        return status == PermissionStatus.Granted;
    }

    [RelayCommand]
    async Task ScanBluetoothDevices()
    {
        if (IsScanning)
        {
            await Toast.Make("Bluetooth scan is already running.").Show();
            return;
        }

        if (!await EnsureBluetoothPermissionAsync())
        {
            await Toast.Make("Bluetooth permission denied.").Show();
            return;
        }

        BluetoothDevices.Clear();

        var ble = CrossBluetoothLE.Current;
        var adapter = CrossBluetoothLE.Current.Adapter;

        if (ble.State != BluetoothState.On)
        {
            await Toast.Make("Bluetooth is off. Please enable Bluetooth and try again.").Show();
            return;
        }

        IsScanning = true;

        void DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs args)
        {
            var name = string.IsNullOrWhiteSpace(args.Device.Name) ? "Unknown device" : args.Device.Name;
            var deviceText = $"{name} ({args.Device.Id})";
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (!BluetoothDevices.Contains(deviceText))
                    BluetoothDevices.Add(deviceText);
            });
        }

        adapter.DeviceDiscovered += DeviceDiscovered;

        try
        {
            await Toast.Make("Scanning for Bluetooth devices...").Show();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            // Start scanning with optional filter and cancellation token.
            await adapter.StartScanningForDevicesAsync(new ScanFilterOptions(), null, false, cts.Token);
            await Toast.Make($"Scan finished. {BluetoothDevices.Count} device(s) found.").Show();
        }
        catch (Exception ex)
        {
            await Toast.Make($"Bluetooth scan failed: {ex.Message}").Show();
        }
        finally
        {
            adapter.DeviceDiscovered -= DeviceDiscovered;
            IsScanning = false;
        }
    }

    [RelayCommand]
    async Task TakePhoto()
    {
        await RequestCameraPermission();
        var photo = await MediaPicker.CapturePhotoAsync();
        if (photo == null)
        {
            await Toast.Make("No photo captured.").Show();
            return;
        }

        try
        {
            var stream = await photo.OpenReadAsync();
            var filePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using var fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);
            await Toast.Make("Photo saved.").Show();
        }
        catch (Exception ex)
        {
            await Toast.Make($"Failed to save photo: {ex.Message}").Show();
        }
    }

    [RelayCommand]
    async Task RequestCameraPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status == PermissionStatus.Granted)
        {
            await Toast.Make("Camera permission already granted.").Show();
        }

        if (Permissions.ShouldShowRationale<Permissions.Camera>())
        {
            await Shell.Current.DisplayAlertAsync("Permission Required", "Camera permission is required to access camera features.", "OK");
        }
        
        status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await Toast.Make("Camera permission denied.").Show();
            return;
        }
    }
    
    [RelayCommand]
    async Task RequestLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
        {
            await Toast.Make("Location permission already granted.").Show();
            return;
        }

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            await Shell.Current.DisplayAlertAsync("Permission Required", "Location permission is required to access location features.", "OK");
        }
        
        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        
        if (status == PermissionStatus.Granted)
        {
            await Toast.Make("Location permission already granted.").Show();
        }
    }
    
    [RelayCommand]
    async Task RequestBluetoothPermission()
    {

        var status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
        if (status == PermissionStatus.Granted)
        {
            await Toast.Make("Bluetooth permission already granted.").Show();
        }
        else
        {
            await RequestBluetoothPermissionInternal();
        }
    }

    private static async Task RequestBluetoothPermissionInternal()
    {
        // Permission denied, handle accordingly
        if (Permissions.ShouldShowRationale<Permissions.Bluetooth>())
        {   
            // Show rationale to the user and request permission again
            await Shell.Current.DisplayAlertAsync("Permission Required", "Bluetooth permission is required to access Bluetooth features.", "OK");                await Permissions.RequestAsync<Permissions.Bluetooth>();
        }
        
        var status = await Permissions.RequestAsync<Permissions.Bluetooth>();
        if (status == PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlertAsync(
                title: "Permission Granted", 
                message: "Bluetooth permission granted. You can now access Bluetooth features.", 
                cancel: "OK");

            
        }
    }
    
    [RelayCommand]
    private async Task OpenGallery()
    {
        var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();
        if (permissionStatus != PermissionStatus.Granted)
        {
            permissionStatus = await Permissions.RequestAsync<Permissions.Photos>();
            if (permissionStatus != PermissionStatus.Granted)
            {
                await Toast.Make("Gallery permission denied.").Show();
                return;
            }
        }
        
        var photo = await MediaPicker.PickPhotoAsync();
        if (photo == null)
        {
            await Toast.Make("No photo selected.").Show();
            return;
        }

        try
        {
            var stream = await photo.OpenReadAsync();
            var filePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using var fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);
            await Toast.Make("Photo saved.").Show();
        }
        catch (Exception ex)
        {
            await Toast.Make($"Failed to save photo: {ex.Message}").Show();
        }
    }
    
    [RelayCommand]
    private async Task GetCurrentLocation()
    {
        await RequestLocationPermission();

        try
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));
            if (location != null)
            {
                Location = location;
                await Toast.Make($"Latitude: {Location.Latitude}, Longitude: {Location.Longitude}").Show();
            }
        }
        catch (FeatureNotSupportedException)
        {
            await Toast.Make("Geolocation is not supported on this device.").Show();
        }
        catch (FeatureNotEnabledException)
        {
            await Toast.Make("GPS is not enabled. Please enable it in your device settings.").Show();
        }
        catch (PermissionException)
        {
            await Toast.Make("Location permission is required to get the current location.").Show();
        }
        catch (Exception ex)
        {
            await Toast.Make($"An unexpected error occurred: {ex.Message}").Show();
        }
    }
}