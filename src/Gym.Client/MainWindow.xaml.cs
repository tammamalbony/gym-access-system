using System.Windows;
using Gym.Client.Pages;

namespace Gym.Client;

public partial class MainWindow : Window
{
    private readonly ApiClient _api = new("http://localhost:5000/");
    private bool _loggedIn;

    public MainWindow()
    {
        InitializeComponent();
        SetLoggedIn(false);
        ShowLogin();
    }


    private void SetLoggedIn(bool value)
    {
        _loggedIn = value;
        LoginBtn.Content = value ? "Logout" : "Login";
        DashboardBtn.IsEnabled = value;
        MembersBtn.IsEnabled = value;
        PlansBtn.IsEnabled = value;
        UsersBtn.IsEnabled = value;
        LogsBtn.IsEnabled = value;
        RemindersBtn.IsEnabled = value;
        AlertsBtn.IsEnabled = value;
    }

    private void OnLoggedIn()
    {
        SetLoggedIn(true);
        MainFrame.Content = new DashboardPage(_api);
    }

    private void ShowLogin()
    {
        MainFrame.Content = new LoginPage(_api, OnLoggedIn, ShowSignup);
    }

    private void ShowSignup()
    {
        MainFrame.Content = new SignUpPage(_api, OnLoggedIn, ShowLogin);
    }


    private void Dashboard_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new DashboardPage(_api);
    }

    private void Members_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new MembersPage(_api);
    }

    private void Plans_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new PlansPage(_api);
    }

    private void Users_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new UsersPage(_api);
    }

    private void Logs_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new LogsPage(_api);
    }

    private void Reminders_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new RemindersPage(_api);
    }

    private void Alerts_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new AlertsPage(_api);
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        if (_loggedIn)
        {
            // log out and return to login page
            _api.Logout();
            SetLoggedIn(false);
            ShowLogin();
        }
        else
        {
            ShowLogin();
        }
    }

}
