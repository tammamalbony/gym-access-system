using System.Windows.Controls;
using Gym.Client.ViewModels;
namespace Gym.Client.Pages;

public partial class MembersPage : Page
{
    private readonly MembersViewModel _vm;

    public MembersPage(ApiClient api)
    {
        InitializeComponent();
        _vm = new MembersViewModel(api);
        DataContext = _vm;
        Grid.ItemsSource = _vm.Items;
        Loaded += async (_, _) => await _vm.Refresh();
    }
}
