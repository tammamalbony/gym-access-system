using System;
using System.Windows;
using System.Windows.Controls;

namespace Gym.Client.Pages;

public partial class SignUpPage : Page
{
    private readonly ApiClient _api;
    private readonly Action _onSuccess;
    private readonly Action _goLogin;
    public SignUpPage(ApiClient api, Action onSuccess, Action goLogin)
    {
        InitializeComponent();
        _api = api;
        _onSuccess = onSuccess;
        _goLogin = goLogin;
    }

    private async void SignUp_Click(object sender, RoutedEventArgs e)
    {
        ErrorText.Text = string.Empty;
        if (PwdBox.Password != ConfirmBox.Password)
        {
            ErrorText.Text = "Passwords do not match";
            return;
        }
        var result = await _api.SignUpAsync(UserBox.Text, EmailBox.Text, PwdBox.Password);
        if (result.Success)
        {
            _onSuccess?.Invoke();
        }
        else
        {
            ErrorText.Text = result.Error ?? "Signup failed";
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        _goLogin?.Invoke();
    }
}
