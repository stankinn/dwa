using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static WpfApp2019.ViewModel.FileViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WpfApp2019.Model
{
    public class FileModel
    {

    }
    public class ObjectAttributes : ObservableObject
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
    }

    public class Item : ObservableObject
    {
        private string _Title;
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Item> _SubItems = new ObservableCollection<Item>();
        public ObservableCollection<Item> SubItems
        {
            get
            {
                return this._SubItems;
            }
            set
            {
                if (this._SubItems != value)
                {
                    this._SubItems = value;
                    OnPropertyChanged();
                }
            }
        }
    }

}
