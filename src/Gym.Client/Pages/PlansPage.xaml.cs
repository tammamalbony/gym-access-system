using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Client.Models;

using Gym.Client.Dialogs;
namespace Gym.Client.Pages;

public partial class PlansPage : Page
{
    private readonly ApiClient _api;
    public ObservableCollection<PlanDto> Items { get; } = new();

    public PlansPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        Grid.ItemsSource = Items;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Items.Clear();
        var data = await _api.GetPlansAsync() ?? new List<PlanDto>();
        foreach (var m in data) Items.Add(m);
    }

    private async void Add_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new PlanEditWindow(null);
        if (dlg.ShowDialog() == true)
        {
            var added = await _api.AddPlanAsync(dlg.Model);
            if (added is not null) Items.Add(added);
        }
    }

    private async void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not PlanDto dto) return;
        var dlg = new PlanEditWindow(dto);
        if (dlg.ShowDialog() == true)
        {
            var updated = await _api.UpdatePlanAsync(dlg.Model);
            if (updated is not null)
            {
                var index = Items.IndexOf(dto);
                Items[index] = updated;
            }
        }
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not PlanDto dto) return;
        if (MessageBox.Show($"Delete {dto.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            await _api.DeletePlanAsync(dto.Id);
            Items.Remove(dto);
        }
    }
}
