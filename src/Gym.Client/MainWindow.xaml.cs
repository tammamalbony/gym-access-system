using System.Windows;
using Gym.Client.Pages;

namespace Gym.Client;

public partial class MainWindow : Window
{
    private readonly ApiClient _api = new("http://localhost:5000/");

    public MainWindow()
    {
        InitializeComponent();
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
}
