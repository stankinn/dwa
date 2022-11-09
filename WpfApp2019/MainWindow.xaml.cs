using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
        }

        private void FileViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            WpfApp2019.ViewModel.FileViewModel fileViewModelObject =
               new WpfApp2019.ViewModel.FileViewModel();
            fileViewModelObject.LoadObjects();

            FileViewControl.DataContext = fileViewModelObject;
        }

    }
}
