using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using WpfApp2019.Stores;
using WpfApp2019.ViewModel;

namespace WpfApp2019
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = NavigationStore.Instance;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrViewModel = new PathViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

    }
}
