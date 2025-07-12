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

    private void Members_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new MembersPage(_api);
    }

    private void Plans_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new PlansPage(_api);
    }
}
