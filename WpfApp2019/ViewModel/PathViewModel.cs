using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.AppServices;
using WpfApp2019.Database;
using WpfApp2019.Model;
using WpfApp2019.Stores;
using WpfApp2019.TreeView;
using DialogResult = System.Windows.Forms.DialogResult;
using DialogService = WpfApp2019.AppServices.Dialog.DialogService;
using IDialogService = WpfApp2019.AppServices.Dialog.IDialogService;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp2019.ViewModel
{
    internal class PathViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        IEventAggregator _ea;
        NavigationStore _navigationStore;
        public PathViewModel()
        {
            _dialogService = new DialogService();
            _ea = ApplicationService.Instance.EventAggregator;
            ApplicationService.Instance.EventAggregator.GetEvent<DbConnectionChangedEvent>().Subscribe(ChangeEnabled);
            OpenEnabled = true;
        }

        private string _search;

        public string Search
        {

            get => _search;

            set
            {
                if (_search != value)
                {
                    _search = value;
                    OnPropertyChanged();
                    _ea.GetEvent<SearchChangedEvent>().Publish(new SearchParameters { SearchInput = _search, Path = FilePathText });
                }
            }

        }

        private ICommand _openFiles;
        public ICommand OpenFiles
        {
            get
            {
                if (_openFiles == null)
                {
                    _openFiles = new RelayCommand(
                        param => SearchFiles()
                    );
                }
                return _openFiles;
            }

        }

        private Boolean _openEnabled;
        public Boolean OpenEnabled
        {
            get => _openEnabled; 
            set
            {
                if (_openEnabled != value)
                {
                    _openEnabled = value;
                    OnPropertyChanged();
                    Trace.WriteLine("PropertyChanged: " + value);
                }
            }
        }

        private ICommand _openDbTables;
        public ICommand OpenDbTables
        {
            get
            {
                if (_openDbTables == null)
                {
                    _openDbTables = new RelayCommand(
                        param => OpenTables()
                    );
                }
                return _openDbTables;
            }

        }

        private ICommand _openDialogCommand;
        public ICommand OpenDialogCommand
        {
            get
            {
                if (_openDialogCommand == null)
                {
                    _openDialogCommand = new RelayCommand(
                        param => OnOpenDialog()
                    );
                }
                return _openDialogCommand;
            }

        }

        public void OnOpenDialog()
        {
            var dialog = new DBDialogViewModel();
            _dialogService.OpenDialog(dialog);
        }


        private ICommand _navigate;
        public ICommand Navigate
        {
            get
            {
                if (_navigate == null)
                {
                    _navigate = new RelayCommand(
                        param => GoToAddEntity()
                    );
                }
                return _navigate;
            }

        }

        private ICommand _navigateBack;
        public ICommand NavigateBack
        {
            get
            {
                if (_navigateBack == null)
                {
                    _navigateBack = new RelayCommand(
                        param => GoBack()
                    );
                }
                return _navigateBack;
            }

        }

        public void GoBack()
        {

            if (FilePathText != null)
            {
                string oldPath = FilePathText.FPath;
                int index = oldPath.LastIndexOf('\\');
                if (index >= 0)
                {
                    oldPath = oldPath.Substring(0, index);
                }

                changePath(oldPath);
            }

        }

        public void GoToAddEntity()
        {
            if (App.Current.Properties["SqlConnectionString"] != null)
            {
                _navigationStore = NavigationStore.Instance;
                _navigationStore.CurrViewModel = new AddEntityViewModel();
            }
            else
                MessageBox.Show("Please connect to a database first.");

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
                    Trace.WriteLine("Path PropertyChanged: " + value.FPath);
                }
            }
        }

        private ObservableCollection<TreeViewItemViewModel> _items;
        public ObservableCollection<TreeViewItemViewModel> Items
        {

            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();

                }
            }
        }

        public void changePath(string sPath)
        {
            FilePathText = new PathText
            {
                FPath = sPath
            };

            _ea.GetEvent<PathChangedEvent>().Publish(FilePathText);
            OnPropertyChanged(nameof(FilePathText));
        }
        public void SearchFiles()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.Desktop
            };

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string sPath = folderDialog.SelectedPath;

                //FilePathText = new PathText
                //{
                //    FPath = sPath
                //};
                changePath(sPath);
                //_ea.GetEvent<PathChangedEvent>().Publish(FilePathText);


                // change TreeView Root - Item Folder
                var items = new List<TreeViewItem>();

                items.Add(new TreeViewItem { FullPath = FilePathText.FPath, Type = TreeViewItemType.Folder });

                Items = new ObservableCollection<TreeViewItemViewModel>(
                items.Select(folder => new TreeViewItemViewModel(folder.FullPath, folder.Type)));


                TreeViewItemViewModel tvivm = new TreeViewItemViewModel(FilePathText.FPath, TreeViewItemType.Folder);
                tvivm.ChangeVisibility(false);

            }
        }

        public void OpenTables()
        {
            if (App.Current.Properties["SqlConnectionString"] != null)
            {
                var items = new List<TreeViewItem>();
                DatabaseConnection dbc = new DatabaseConnection();
                List<string> tables = dbc.GetTableNames();


                for (int i = 0; i < tables.Count; i++)
                {
                    items.Add(new TreeViewItem { FullPath = tables[i], Type = TreeViewItemType.Table });
                }

                Items = new ObservableCollection<TreeViewItemViewModel>(
                items.Select(folder => new TreeViewItemViewModel(folder.FullPath, folder.Type)));

                TreeViewItemViewModel tvivm = new TreeViewItemViewModel("", TreeViewItemType.Table);
                tvivm.ChangeVisibility(true);
            }
            else
            {
                MessageBox.Show("Please connect to a database first.");
            }
        }

        public void ChangeEnabled(DbConnection con)
        {
            Trace.WriteLine("HERE" + con.Connected);
            OpenEnabled = con.Connected;
        }
    }
}
