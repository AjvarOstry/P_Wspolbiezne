
using TP.ConcurrentProgramming.Presentation.ViewModel.MVVMLight;

namespace TP.ConcurrentProgramming.Presentation.ViewModel
{
    public class StartWindowViewModel : ViewModelBase
    {
        public int MinBalls => 1;
        public int MaxBalls => 100;

        private int _selectedNumberOfBalls = 5;
        public int SelectedNumberOfBalls
        {
            get => _selectedNumberOfBalls;
            set
            {
                _selectedNumberOfBalls = value;
                // Metoda z MVVMLight
                RaisePropertyChanged(); 
            }
        }
    }
}