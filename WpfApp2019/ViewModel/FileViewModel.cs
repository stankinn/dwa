using System;
using System.Collections.ObjectModel;
using WpfApp2019.Model;
using Path = System.IO.Path;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.ComponentModel;
using WpfApp2019.View;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;

namespace WpfApp2019.ViewModel
{
    internal class FileViewModel
    {
        public FileViewModel()
        {
            Files = new ObservableCollection<ObjectAttributes>();
            Items = new ObservableCollection<Item>();
            FilePathText = new PathText();

            LoadObjects();

            FilePathText.FPath = "Change me";
        }
        public ObservableCollection<ObjectAttributes> Files { get; set; }

        public ObservableCollection<Item> Items { get; set; }

        public PathText FilePathText { get; set; }

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
                //Path wird gesetzt

                string sPath = folderDialog.SelectedPath;

                FilePathText = new PathText
                {
                    FPath = sPath
                };
                Trace.WriteLine("click: " + FilePathText.FPath);

            }
        }

        public void LoadObjects()
        {
            string sPath = "";

            if (FilePathText != null)
            {
                sPath = FilePathText.FPath;
                Trace.WriteLine("pp: " + sPath);
            }

            try
            {
                var files = Directory.EnumerateFileSystemEntries(sPath);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                ObservableCollection<Item> treeItems = new ObservableCollection<Item>();
                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d),
                        Type = GetDataType(d),
                        ModificationTime = Directory.GetLastWriteTime(d),
                        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                        Description = GetFileDescription(d),
                        FilePath = Path.GetFullPath(d)
                    });
                    treeItems.Add(new Item
                    {
                        Title = Path.GetFileName(d)
                    });
                }
                Files = items;
                Items = treeItems;
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private string GetDataType(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return Path.GetExtension(file);
            }
            else
            {
                return "Dateiordner";
            }
        }

        private string GetFileDescription(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return FileVersionInfo.GetVersionInfo(file).FileDescription;
            }
            else
            {
                return "";
            }
        }
    }
}
