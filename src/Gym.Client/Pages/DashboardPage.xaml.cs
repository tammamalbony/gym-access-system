using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Drawing;
using Gym.Core.Dtos;

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
        Loaded += Refresh_Click;
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

    private async void Latest_Click(object sender, RoutedEventArgs e)
    {
        var log = await _api.GetLatestLogAsync();
        if (log is null) return;
        var sub = await _api.GetActiveSubscriptionAsync(log.MemberId);
        var status = sub is null ? "No active subscription" :
            (sub.EndDate.ToDateTime(TimeOnly.MinValue) < DateTime.UtcNow
                ? "Expired" : "Active");
        System.Windows.Forms.NotifyIcon ni = new();
        ni.Visible = true;
        ni.Icon = System.Drawing.SystemIcons.Information;
        ni.BalloonTipTitle = $"Last access by {log.MemberId}";
        ni.BalloonTipText = status;
        ni.ShowBalloonTip(3000);
        // auto dispose after short delay
        await Task.Delay(4000);
        ni.Dispose();
    }
}
