using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace WpfApp2019.Model
{
    public class FilePathModel
    {
       
    }

    public class DbConnection
    {
        public Boolean Connected { get; set; }
    }
    public class PathText
    {
        public string FPath{ get; set; }
    }
    public class SearchParameters
    {
        public string SearchInput { get; set; }
        public PathText Path { get; set; }

    }
}
