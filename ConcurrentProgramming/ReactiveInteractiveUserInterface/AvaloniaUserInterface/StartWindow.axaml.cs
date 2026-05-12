using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TP.ConcurrentProgramming.Presentation.ViewModel;

namespace TP.ConcurrentProgramming.AvaloniaUI
{
    public partial class StartWindow : Window
    {
    public int MinBalls { get; } = 1;
    public int MaxBalls { get; } = 200;

    public StartWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
        private void Start_Simulation(object sender, RoutedEventArgs e)
        {
            int numberOfBalls = (int)BallsSlider.Value;
            MainWindow mainWindow = new MainWindow(numberOfBalls);
            
            if (Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = mainWindow;
            }
            mainWindow.Show();
            Close();
        }
    }
}