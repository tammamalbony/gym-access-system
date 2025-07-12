using System.Windows;
using Gym.Client.Models;

namespace Gym.Client.Dialogs;

public partial class MemberEditWindow : Window
{
    public MemberDto Model { get; private set; }

    public MemberEditWindow(MemberDto? model)
    {
        InitializeComponent();
        Model = model ?? new MemberDto(0, "", "", "", null, null, null, false, DateTime.Now, DateTime.Now);
        DataContext = Model;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
