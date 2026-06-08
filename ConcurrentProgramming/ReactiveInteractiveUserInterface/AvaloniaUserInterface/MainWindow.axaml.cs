using Avalonia.Controls;
using Avalonia.Threading; // Dodane, aby użyć Dispatchera
using TP.ConcurrentProgramming.Presentation.ViewModel;
using TP.ConcurrentProgramming.Presentation.Model; // Dodane, aby użyć ModelAbstractApi
using System;

namespace TP.ConcurrentProgramming.AvaloniaUI;

public partial class MainWindow : Window
{
    public MainWindow(int numberOfBalls)
    {
        InitializeComponent();
        var model = ModelAbstractApi.CreateModel();
        MainWindowViewModel viewModel;
        
        if (DataContext is MainWindowViewModel existingViewModel)
        {
            viewModel = existingViewModel;
        }
        else
        {
            viewModel = new MainWindowViewModel(model);
            DataContext = viewModel;
        }

        model.MomentumChanged += (val) => 
            Dispatcher.UIThread.InvokeAsync(() => viewModel.MomentumTxt = $"Momentum: {val:F2}");
        
        viewModel.Start(numberOfBalls);
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

