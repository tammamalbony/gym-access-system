using System;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Client.Pages;

public partial class LoginPage : Page
{
    private readonly ApiClient _api;
    private readonly Action _onSuccess;
    public LoginPage(ApiClient api, Action onSuccess)
    {
        InitializeComponent();
        _api = api;
        _onSuccess = onSuccess;
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        ErrorText.Visibility = Visibility.Collapsed;
        bool ok = await _api.LoginAsync(UserBox.Text, PwdBox.Password);
        if (ok)
        {
            _onSuccess?.Invoke();
        }
        else
        {
            ErrorText.Text = "Login failed";
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
