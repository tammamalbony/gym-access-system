using System.Windows;
using Gym.Client.Models;

namespace Gym.Client.Dialogs;

public partial class UserEditWindow : Window
{
    public AppUserDto Model { get; private set; }
    public string? Password { get; private set; }

    public UserEditWindow(AppUserDto? model, bool requirePassword)
    {
        InitializeComponent();
        Model = model ?? new AppUserDto(0, "", "DATA_ENTRY", "", true, DateTime.Now);
        DataContext = Model;
        RoleBox.ItemsSource = new[] { "DATA_ENTRY", "ADMIN", "SUPPORT" };
        if (requirePassword) PwdPanel.Visibility = Visibility.Visible;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Password = PwdBox.Password;
        DialogResult = true;
    }
}
