using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Gym.Client.Models;
using Gym.Client.Dialogs;

namespace Gym.Client.Pages;

public partial class UsersPage : Page
{
    private readonly ApiClient _api;
    public ObservableCollection<AppUserDto> Items { get; } = new();

    public UsersPage(ApiClient api)
    {
        InitializeComponent();
        _api = api;
        Grid.ItemsSource = Items;
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Items.Clear();
        var list = await _api.GetUsersAsync() ?? new();
        foreach (var u in list) Items.Add(u);
        ActiveText.Text = Items.Count(u => u.IsEnabled).ToString();
    }

    private async void Add_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new UserEditWindow(null, true);
        if (dlg.ShowDialog() == true)
        {
            var added = await _api.AddUserAsync(dlg.Model, dlg.Password!);
            if (added is not null) Items.Add(added);
        }
    }

    private async void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not AppUserDto dto) return;
        var dlg = new UserEditWindow(dto, false);
        if (dlg.ShowDialog() == true)
        {
            var updated = await _api.UpdateUserAsync(dlg.Model);
            if (updated is not null)
            {
                var idx = Items.IndexOf(dto);
                Items[idx] = updated;
            }
        }
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not AppUserDto dto) return;
        if (MessageBox.Show($"Delete {dto.Username}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            await _api.DeleteUserAsync(dto.Id);
            Items.Remove(dto);
        }
    }

    private async void Toggle_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not AppUserDto dto) return;
        await _api.SetUserEnabledAsync(dto.Id, !dto.IsEnabled);
        dto = dto with { IsEnabled = !dto.IsEnabled };
        var idx = Items.IndexOf((AppUserDto)Grid.SelectedItem);
        Items[idx] = dto;
        ActiveText.Text = Items.Count(u => u.IsEnabled).ToString();
    }

    private async void Pwd_Click(object sender, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not AppUserDto dto) return;
        var dlg = new PasswordWindow();
        if (dlg.ShowDialog() == true && dlg.Password is { Length: >0 })
        {
            await _api.ChangePasswordAsync(dto.Id, dlg.Password!);
        }
    }
}
