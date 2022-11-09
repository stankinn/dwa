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

namespace WpfApp2019.ViewModel
{
    public class FileViewModel
    {
        public FileViewModel()
        {
            LoadObjects();
        }

        public class PathText : INotifyPropertyChanged
        {

            string fPath;

            public string FPath
            {
                get
                {
                    return fPath;
                }
                set
                {
                    if (fPath != value)
                    {
                        fPath = value;
                        RaisePropertyChanged("FPath");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }

            }
        }

        public class ObjectAttributes : INotifyPropertyChanged
        {
            private string name;
            private string type;
            private DateTime modificationTime;
            private string owner;
            private string description;
            private string path;

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (name != value)
                    {
                        name = value;
                        RaisePropertyChanged("Name");
                    }
                }
            }
            public string Type
            {
                get
                {
                    return type;
                }
                set
                {
                    if (type != value)
                    {
                        type = value;
                        RaisePropertyChanged("Type");
                    }
                }
            }

            public DateTime ModificationTime
            {
                get
                {
                    return modificationTime;
                }
                set
                {
                    if (modificationTime != value)
                    {
                        modificationTime = value;
                        RaisePropertyChanged("ModificationTime");
                    }
                }
            }

            public string Owner
            {
                get
                {
                    return owner;
                }
                set
                {
                    if (owner != value)
                    {
                        owner = value;
                        RaisePropertyChanged("Owner");
                    }
                }
            }

            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    if (description != value)
                    {
                        description = value;
                        RaisePropertyChanged("Description");
                    }
                }
            }

            public string FilePath
            {
                get
                {
                    return path;
                }
                set
                {
                    if (path != value)
                    {
                        path = value;
                        RaisePropertyChanged("FilePath");
                    }
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }

            }
        }

        public void SearchFiles()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Path wird gesetzt

                string sPath = folderDialog.SelectedPath;

                PathText pt = new PathText();
                pt.FPath = sPath;

                Trace.WriteLine("click: " + pt.FPath);

            }
        }

        public void LoadObjects()
        {
            string sPath = "";
            PathText pt = new PathText();

            Trace.WriteLine("LOADING...");

            if (pt != null)
            {
                sPath = pt.FPath;
                Trace.WriteLine("pp: " + sPath);
            }
            

            try
            {
                var files = Directory.EnumerateFileSystemEntries(sPath);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
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
                }
                //FileList.ItemsSource = items;
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
