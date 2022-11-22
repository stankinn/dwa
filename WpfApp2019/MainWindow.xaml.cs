using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp2019.ViewModel;

namespace WpfApp2019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.Loaded += new RoutedEventHandler(FileViewControl_Loaded);
        }

        //private void FileViewControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    FileViewModel fileViewModelObject = new FileViewModel();
        //    Trace.WriteLine("WINDOW path: " + fileViewModelObject);
        //    fileViewModelObject.LoadObjects();

        //    FileViewControl.DataContext = fileViewModelObject;
        //    if (fileViewModelObject.FilePathText != null)
        //    {
        //        Trace.WriteLine("WINDOW path: " + fileViewModelObject.FilePathText.FPath);
        //    }

        //}

        

    }
}
