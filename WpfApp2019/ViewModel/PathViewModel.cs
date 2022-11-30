using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.Model;
using static WpfApp2019.ViewModel.AddEntityViewModel;
using WpfApp2019.View;


namespace WpfApp2019.ViewModel
{
    internal class PathViewModel : ObservableObject, INavigationAware
    {

        private IContainer container;
        private PathText _filePathText;
        private IRegionManager regionManager;

        IEventAggregator _ea;

        public PathViewModel()
        {
            this.container = ApplicationService.Instance.Container;
            this.regionManager = ApplicationService.Instance.RegionManager;
            _ea = ApplicationService.Instance.EventAggregator;
            Trace.WriteLine(regionManager);
            regionManager.RegisterViewWithRegion("PathViewRegion", typeof(PathView));
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

        private ICommand _addEntityViewCommand;
        public ICommand AddEntityViewCommand
        {
            get
            {
                if (_addEntityViewCommand == null)
                {
                    _addEntityViewCommand = new RelayCommand(
                        param => this.AddEntityView()
                    );
                }
                return _addEntityViewCommand;
            }

        }

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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }


        #region Executes
        /// <summary>
        /// Command when ViewB button clicked
        /// </summary>
        public void AddEntityView()
        {
            Trace.WriteLine("Change View");

         

            regionManager.RequestNavigate("PathViewRegion", "AddEntityViewRegion");

           
        }

        #endregion
    }
}
