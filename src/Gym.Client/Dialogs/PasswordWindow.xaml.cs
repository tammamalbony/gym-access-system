using System.Windows;

namespace Gym.Client.Dialogs;

public partial class PasswordWindow : Window
{
    public string? Password { get; private set; }
    public PasswordWindow()
    {
        InitializeComponent();
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Password = PwdBox.Password;
        DialogResult = true;
    }
}
