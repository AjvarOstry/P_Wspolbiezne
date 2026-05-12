using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TP.ConcurrentProgramming.Presentation.ViewModel.Test
{
    [TestClass]
    public class StartWindowViewModelUnitTest
    {
        [TestMethod]
        public void BallLimits_ShouldBeConstantAndSecure()
        {
            
            StartWindowViewModel viewModel = new StartWindowViewModel();
            
            int min = viewModel.MinBalls;
            int max = viewModel.MaxBalls;
            
            Assert.IsTrue(1 <= min, "Minimalna liczba kulek niezgodna z logiką biznesową: "  + min);
            Assert.IsTrue(100 >= max, "Maksymalna liczba kulek niezgodna z logiką biznesową: "  + max);
            Assert.IsTrue(1 <= min, "Minimalna liczba kulek niezgodna z logiką biznesową: "  + min);
            Assert.IsTrue(100 >= max, "Maksymalna liczba kulek niezgodna z logiką biznesową: "  + max);
        }

        [TestMethod]
        public void SelectedBalls_InitialValue_ShouldBeWithinLimits()
        {
            StartWindowViewModel viewModel = new StartWindowViewModel();
            
            Assert.IsTrue(viewModel.SelectedNumberOfBalls >= viewModel.MinBalls, "Wartość początkowa jest mniejsza niż minimum.");
            Assert.IsTrue(viewModel.SelectedNumberOfBalls <= viewModel.MaxBalls, "Wartość początkowa jest większa niż maksimum.");
        }
    }
}