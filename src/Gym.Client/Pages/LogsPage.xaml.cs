using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Core.Dtos;

namespace Gym.Client.Pages;

public partial class LogsPage : Page
{
    private readonly ApiClient _api;
    public ObservableCollection<AccessLogDto> Items { get; } = new();

    public LogsPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        Grid.ItemsSource = Items;
        Loaded += Refresh_Click;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Items.Clear();
        var list = await _api.GetLogsAsync() ?? new();
        foreach (var l in list) Items.Add(l);
    }
}
