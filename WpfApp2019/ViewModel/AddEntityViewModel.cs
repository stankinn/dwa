using System.Windows.Input;
using WpfApp2019.Stores;

namespace WpfApp2019.ViewModel
{
    internal class AddEntityViewModel:ObservableObject, IViewModel
    {

        NavigationStore _navigationStore;

        public AddEntityViewModel()
        {

        }

        private ICommand _addEntityCommand;
        public ICommand AddEntityCommand
        {
            get
            {
                if (_addEntityCommand == null)
                {
                    _addEntityCommand = new RelayCommand(
                        param => this.AddEntity()
                    );
                }
                return _addEntityCommand;
            }

        }
        public void AddEntity()
        {

        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(
                        param => this.GoBack()
                    );
                }
                return _goBackCommand;
            }

        }
        public void GoBack()
        {
            _navigationStore = NavigationStore.Instance;

            _navigationStore.CurrViewModel = new PathViewModel();
        }
        
    }
}
