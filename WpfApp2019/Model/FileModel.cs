using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static WpfApp2019.ViewModel.FileViewModel;
using System.Collections.ObjectModel;

namespace WpfApp2019.Model
{
    public class FileModel
    {
       
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

    public class Item : INotifyPropertyChanged
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
                    this.OnPropertyChanged("Title");
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
                    this.OnPropertyChanged("SubItems");
                }
            }
        }

        #region INotifyPropertyChanged Members

        protected void OnPropertyChanged(string propertyName)
        {
            var evt = this.PropertyChanged;
            if (evt != null)
                evt(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
