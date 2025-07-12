using System.Windows;
using System.Windows.Controls;

namespace Gym.Client.Pages;

public partial class LoginPage : Page
{
    private readonly ApiClient _api;
    public LoginPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        ErrorText.Visibility = Visibility.Collapsed;
        bool ok = await _api.LoginAsync(UserBox.Text, PwdBox.Password);
        if (ok)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Content = new DashboardPage(_api);
        }
        else
        {
            ErrorText.Text = "Login failed";
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
