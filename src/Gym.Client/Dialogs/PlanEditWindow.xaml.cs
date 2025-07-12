using System.Windows;
using Gym.Client.Models;

namespace Gym.Client.Dialogs;

public partial class PlanEditWindow : Window
{
    public PlanDto Model { get; private set; }

    public PlanEditWindow(PlanDto? model)
    {
        InitializeComponent();
        Model = model ?? new PlanDto(0, "", null, 0, 1, 0, true, DateTime.Now, DateTime.Now);
        DataContext = Model;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
