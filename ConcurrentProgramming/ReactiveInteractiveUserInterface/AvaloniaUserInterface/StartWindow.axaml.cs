using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TP.ConcurrentProgramming.Presentation.ViewModel; // Upewnij się, że ten namespace jest poprawny

namespace TP.ConcurrentProgramming.AvaloniaUI
{
    public partial class StartWindow : Window
    {    
        private readonly StartWindowViewModel _viewModel;

        public StartWindow()
        {
            InitializeComponent();
            
            _viewModel = new StartWindowViewModel();
            // datacontext
            DataContext = _viewModel;
        }

        private void Start_Simulation(object sender, RoutedEventArgs e)
        {
            // synchronizacja z viewmodelem
            int numberOfBalls = _viewModel.SelectedNumberOfBalls;
           
			// glowne okno
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