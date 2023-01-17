using System.Diagnostics;
using WpfApp2019.AppServices;
using WpfApp2019.Stores;

namespace WpfApp2019.ViewModel
{
    internal class MainWindowViewModel: ViewModelBase
    {

        private readonly NavigationStore _navigationStore = NavigationStore.Instance;

        public object ViewModel => _navigationStore.CurrViewModel;
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            Trace.WriteLine("MainWindow Start: " + ViewModel);
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentVMChanged;
        }

        private void OnCurrentVMChanged()
        {
           OnPropertyChanged(nameof(ViewModel));
        }

        // public FileViewModel FileViewModel { get; set; }
    }
}
