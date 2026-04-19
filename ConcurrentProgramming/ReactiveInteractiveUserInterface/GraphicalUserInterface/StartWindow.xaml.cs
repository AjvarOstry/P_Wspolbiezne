using System.Windows;
using TP.ConcurrentProgramming.Presentation.ViewModel;

namespace TP.ConcurrentProgramming.PresentationView
{
  /// <summary>
  /// View implementation
  /// </summary>
  public partial class StartWindow : Window
  {
    public StartWindow()
    {
      
      InitializeComponent();
      //MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
      // double screenWidth = SystemParameters.PrimaryScreenWidth;
      // double screenHeight = SystemParameters.PrimaryScreenHeight;
      
    }

    private void Start_Simulation(object sender, RoutedEventArgs e)
    {
      //MessageBox.Show("Start simulation");
      
      int numberOfBalls = (int)BallsSlider.Value;
      MainWindow mainWindow = new MainWindow(numberOfBalls);
      Application.Current.MainWindow = mainWindow;
      mainWindow.Show();

      Close();
    }
  }
}