using Avalonia.Controls;
using TP.ConcurrentProgramming.Presentation.ViewModel;
using System;

namespace TP.ConcurrentProgramming.AvaloniaUI;

public partial class MainWindow : Window
{
    public MainWindow(int numberOfBalls)
    {
        InitializeComponent();
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.Start(numberOfBalls);
        }
        else
        {
            MainWindowViewModel newViewModel = new MainWindowViewModel();
            DataContext = newViewModel;
            newViewModel.Start(numberOfBalls);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            viewModel.Dispose();
        }
        base.OnClosed(e);
    }
}

