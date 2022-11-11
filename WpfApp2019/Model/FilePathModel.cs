using System.ComponentModel;
using System.Diagnostics;


namespace WpfApp2019.Model
{
    public class FilePathModel
    {
       
    }
    public class PathText : ObservableObject
    {

        string fPath = "path";

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
                    OnPropertyChanged();
                }
            }
        }
    }

}
