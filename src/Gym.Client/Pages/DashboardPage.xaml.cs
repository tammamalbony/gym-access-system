using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Client.Models;

namespace Gym.Client.Pages;

public partial class DashboardPage : Page
{
    private readonly ApiClient _api;

    public DashboardDto Summary { get; set; } = new(0,0,0);
    public ObservableCollection<LateMemberDto> LateItems { get; } = new();
    public ObservableCollection<SubscriptionDto> SubItems { get; } = new();

    public DashboardPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        DataContext = this;
        LateGrid.ItemsSource = LateItems;
        SubGrid.ItemsSource = SubItems;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        var sum = await _api.GetDashboardSummaryAsync();
        if (sum != null) Summary = sum;

        LateItems.Clear();
        foreach (var l in await _api.GetLateMembersAsync() ?? new())
            LateItems.Add(l);

        SubItems.Clear();
        foreach (var s in await _api.GetSubscriptionsAsync() ?? new())
            SubItems.Add(s);

        DataContext = null;
        DataContext = this;
    }
}
