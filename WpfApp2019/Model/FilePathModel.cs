using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static WpfApp2019.ViewModel.FileViewModel;

namespace WpfApp2019.Model
{
    public class FilePathModel
    {
       
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

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            //if (PropertyChanged != null)
            //{
            //    Trace.WriteLine("UPDATEEEE");
            //    PropertyChanged(this, new PropertyChangedEventArgs(property));
            //}

        }
    }

}
