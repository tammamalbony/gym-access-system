using System;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Client.Pages;

public partial class LoginPage : Page
{
    private readonly ApiClient _api;
    private readonly Action _onSuccess;
    private readonly Action _goSignup;
    public LoginPage(ApiClient api, Action onSuccess, Action goSignup)
    {
        InitializeComponent();
        _api = api;
        _onSuccess = onSuccess;
        _goSignup = goSignup;
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        ErrorText.Text = string.Empty;
        var result = await _api.LoginAsync(UserBox.Text, PwdBox.Password);
        if (result.Success)
        {
            _onSuccess?.Invoke();
        }
        else
        {
            ErrorText.Text = result.Error ?? "Login failed";
        }
    }

    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        _goSignup?.Invoke();
    }
}
