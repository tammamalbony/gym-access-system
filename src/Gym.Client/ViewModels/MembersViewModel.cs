using System.Collections.ObjectModel;
using System.Windows.Input;
using Gym.Core.Dtos;
using Gym.Core;
using Gym.Client.Dialogs;

namespace Gym.Client.ViewModels;

public class MembersViewModel : BaseViewModel
{
    private readonly ApiClient _api;

    public ObservableCollection<MemberDto> Items { get; } = new();

    public ICommand RefreshCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }

    public MembersViewModel(ApiClient api)
    {
        _api = api;
        RefreshCommand = new RelayCommand(async _ => await Refresh());
        AddCommand = new RelayCommand(async _ => await Add());
        EditCommand = new RelayCommand(async sel => await Edit(sel as MemberDto));
        DeleteCommand = new RelayCommand(async sel => await Delete(sel as MemberDto));
    }

    public async Task Refresh()
    {
        Items.Clear();
        var data = await _api.GetMembersAsync() ?? new List<MemberDto>();
        foreach (var m in data) Items.Add(m);
    }

    private async Task Add()
    {
        var dlg = new MemberEditWindow(null);
        if (dlg.ShowDialog() == true)
        {
            var added = await _api.AddMemberAsync(dlg.Model);
            if (added is not null) Items.Add(added);
        }
    }

    private async Task Edit(MemberDto? dto)
    {
        if (dto is null) return;
        var dlg = new MemberEditWindow(dto);
        if (dlg.ShowDialog() == true)
        {
            var updated = await _api.UpdateMemberAsync(dlg.Model);
            if (updated is not null)
            {
                var index = Items.IndexOf(dto);
                if (index >= 0) Items[index] = updated;
            }
        }
    }

    private async Task Delete(MemberDto? dto)
    {
        if (dto is null) return;
        if (System.Windows.MessageBox.Show($"Delete {dto.FirstName}?", "Confirm", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
        {
            await _api.DeleteMemberAsync(dto.Id);
            Items.Remove(dto);
        }
    }
}
