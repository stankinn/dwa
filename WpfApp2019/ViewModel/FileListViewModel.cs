using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using WpfApp2019.Model;

namespace WpfApp2019.ViewModel
{
    internal class FileListViewModel : ObservableObject, IViewModel
    {
        public FileListViewModel( )
        {
            ApplicationService.Instance.EventAggregator.GetEvent<PathChangedEvent>().Subscribe(LoadObjects);
            //LoadObjects();
        }


        public ObservableCollection<Item> Items { get; set; }

        //Änderung/Aktualisierung der Attribute
        private ObjectAttributes _object = new ObjectAttributes();
        public ObjectAttributes Object
        {

            get => _object;
            set
            {
                if (_object != value)
                {
                    _object = value;
                    OnPropertyChanged();
                    Trace.WriteLine("value changed");
                }
            }
        }

        //Änderung/Aktualisierung der ganzen Collection
        private ObservableCollection<ObjectAttributes> _files = new ObservableCollection<ObjectAttributes>();
        public ObservableCollection<ObjectAttributes> Files
        {
            get => _files;
            set
            {
                if (_files != value)
                {
                    _files = value;
                    OnPropertyChanged();
                    Trace.WriteLine("value changed");
                }
            }
        }


        public void LoadObjects(PathText path)
        {
            string sPath = "";
            //Trace.WriteLine("LOADING OBJECTS");

            if (path != null)
            {
                sPath = path.FPath;

            }
            Trace.WriteLine("pp: " + sPath);
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
