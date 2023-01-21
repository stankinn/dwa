﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }


        private ICommand _openFiles;
        public ICommand OpenFiles
        {
            get
            {
                if (_openFiles == null)
                {
                    _openFiles = new RelayCommand(
                        param => this.SearchFiles()
                    );
                }
                return _openFiles;
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
                        param => this.OnOpenDialog()
                    );
                }
                return _openDialogCommand;
            }

        }

        public void OnOpenDialog()
        {
            var dialog = new DBDialogViewModel();
            var result = _dialogService.OpenDialog(dialog);
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

        private ICommand _navigateBack;
        public ICommand NavigateBack
        {
            get
            {
                if (_navigateBack == null)
                {
                    _navigateBack = new RelayCommand(
                        param => this.GoBack()
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
                    Trace.WriteLine("Path PropertyChanged");
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
            Trace.WriteLine("updated: " + FilePathText.FPath);
        }

        private bool even = true;
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

                // change TreeView Root - Item Folder
                //if (even)
                //{
                    //var items = new List<TreeViewItem>();

                    //items.Add(new TreeViewItem { FullPath = FilePathText.FPath, Type = TreeViewItemType.Folder });

                    //Items = new ObservableCollection<TreeViewItemViewModel>(
                    //items.Select(folder => new TreeViewItemViewModel(folder.FullPath, folder.Type)));

                    //TreeViewItemViewModel tvivm = new TreeViewItemViewModel(FilePathText.FPath, TreeViewItemType.Folder);
                    //tvivm.ChangeVisibility(false);
                //        even = false;
                //}




                // change TreeView Root-Item Database
                //else
                //    {
                DatabaseConnection dbc = new DatabaseConnection();
                List<string> tables = dbc.GetTableNames();

                var items = new List<TreeViewItem>();

                for (int i = 0; i < tables.Count; i++)
                {
                    items.Add(new TreeViewItem { FullPath = tables[i], Type = TreeViewItemType.Table });
                }

                Items = new ObservableCollection<TreeViewItemViewModel>(
                items.Select(table => new TreeViewItemViewModel(table.FullPath, TreeViewItemType.Table)));

                TreeViewItemViewModel tvivm = new TreeViewItemViewModel(FilePathText.FPath, TreeViewItemType.Table);
                tvivm.ChangeVisibility(true);
                //        even = true;
                //    }

            }
        }

    }
}
