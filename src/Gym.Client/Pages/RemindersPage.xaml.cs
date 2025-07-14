using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Client.Models;
using MessageBox = System.Windows.MessageBox;

namespace Gym.Client.Pages;

public partial class RemindersPage : Page
{
    private readonly ApiClient _api;
    public ObservableCollection<ExpiringSubDto> Items { get; } = new();

    public RemindersPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        Grid.ItemsSource = Items;
        Loaded += Refresh_Click;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Items.Clear();
        var list = await _api.GetTomorrowExpirationsAsync() ?? new();
        foreach (var i in list) Items.Add(i);
    }

    private async void Send_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not ExpiringSubDto dto) return;
        await _api.SendReminderAsync(dto.SubscriptionId);
        MessageBox.Show("Reminder sent.");
    }
}
