//namespace TP.ConcurrentProgramming.Presentation.ViewModel;

using TP.ConcurrentProgramming.Presentation.ViewModel.MVVMLight;

namespace TP.ConcurrentProgramming.Presentation.ViewModel
{
    // Dziedziczymy po bazie, która jest w podfolderze MVVMLight
    public class StartWindowViewModel : ViewModelBase
    {
        public int MinBalls => 1;
        public int MaxBalls => 200;

        private int _selectedNumberOfBalls = 5;
        public int SelectedNumberOfBalls
        {
            get => _selectedNumberOfBalls;
            set
            {
                _selectedNumberOfBalls = value;
                // Metoda z klasy bazowej (MVVMLight)
                RaisePropertyChanged(); 
            }
        }
    }
}