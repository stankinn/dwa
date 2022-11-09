using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp2019.Model
{
    public class FilePathModel
    {
    }

    public class PathText : INotifyPropertyChanged
    {

        string fPath;

        public string FPath {
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
}
