using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TP.ConcurrentProgramming.Presentation.ViewModel;

namespace TP.ConcurrentProgramming.AvaloniaUI
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Start_Simulation(object sender, RoutedEventArgs e)
        {
            int numberOfBalls = (int)BallsSlider.Value;
            MainWindow mainWindow = new MainWindow(numberOfBalls);
            
            if (Application.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = mainWindow;
            }
            mainWindow.Show();
            Close();
        }
    }
}