using Prism.Events;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.Model;
using WpfApp2019.Stores;

namespace WpfApp2019.ViewModel
{
    internal class PathViewModel : ObservableObject, IViewModel
    {
    
        IEventAggregator _ea;
        NavigationStore _navigationStore;
        public PathViewModel()
        {
            _ea = ApplicationService.Instance.EventAggregator;
        }

        private ICommand _changeTextCommand;
        public ICommand ChangeTextCommand
        {
            get
            {
                if (_changeTextCommand == null)
                {
                    _changeTextCommand = new RelayCommand(
                        param => this.SearchFiles()
                    );
                }
                return _changeTextCommand;
            }

        }

        private ICommand _navigate;
        public ICommand Navigate
        {
            get
            {
                if (_navigate == null)
                {
                    _navigate = new RelayCommand(
                        param => this.GoToAddEntity()
                    );
                }
                return _navigate;
            }

        }

        public void GoToAddEntity()
        {
            _navigationStore = NavigationStore.Instance;
            
            _navigationStore.CurrViewModel = new AddEntityViewModel();


        }

        private PathText _filePathText;
        public PathText FilePathText
        {

            get => _filePathText;
            set
            {
                if (_filePathText != value)
                {
                    _filePathText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SearchFiles()
        {
            Trace.WriteLine("clicked!");
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.Desktop
            };

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string sPath = folderDialog.SelectedPath;

                FilePathText = new PathText
                {
                    FPath = sPath
                };

                _ea.GetEvent<PathChangedEvent>().Publish(FilePathText);
                Trace.WriteLine("click: " + FilePathText.FPath);

            }
        }


       


    }
}
