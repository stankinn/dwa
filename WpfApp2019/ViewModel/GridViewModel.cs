using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp2019.AppServices;
using WpfApp2019.Database;
using WpfApp2019.Model;

namespace WpfApp2019.ViewModel
{
    internal class GridViewModel : ViewModelBase
    {
        public GridViewModel()
        {
            ApplicationService.Instance.EventAggregator.GetEvent<TNameChangedEvent>().Subscribe(LoadTable);
            ApplicationService.Instance.EventAggregator.GetEvent<GVisibilityChangedEvent>().Subscribe(LoadVisibility);
            GridVisibility = Visibility.Hidden;
        }

        private Visibility _gridVisibility = new Visibility();
        public Visibility GridVisibility
        {
            get => _gridVisibility;
            set
            {
                if (_gridVisibility != value)
                {
                    _gridVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private DataTable _table = new DataTable();
        public DataTable Table
        { 
            get => _table;
            set
            {
                if (_table != value)
                {
                    _table = value;
                    OnPropertyChanged();
                }
            } 
        }

        public void LoadTable(Table table)
        {
            string tableName = table.Name;
            DatabaseConnection dbc = new DatabaseConnection();
            Table = new DataTable();

            Table = dbc.GetTableContent(tableName);
        }

        public void LoadVisibility(GridVisible gridV)
        {
            GridVisibility = gridV.Visible;
        }
    }
}
