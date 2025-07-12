using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Client.Models;

namespace Gym.Client.Pages;

public partial class AlertsPage : Page
{
    private readonly ApiClient _api;
    public ObservableCollection<EmailAlertDto> Items { get; } = new();

    public AlertsPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        Grid.ItemsSource = Items;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Items.Clear();
        var list = await _api.GetAlertsAsync() ?? new();
        foreach (var a in list) Items.Add(a);
    }
}
