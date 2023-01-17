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
            //ApplicationService.Instance.EventAggregator.GetEvent<PathChangedEvent>().Subscribe(LoadObjects);
            //LoadTable("themes");
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

        public void LoadTable(string tableName)
        {
            DatabaseConnection dbc = new DatabaseConnection();
            _table = new DataTable();

            List<string> tableHeader = dbc.GetDataType(tableName)[0];

            for (int i = 0; i < tableHeader.Count; i++)
            {
                _table.Columns.Add(tableHeader[i]);
                Trace.WriteLine(tableHeader[i]);
            }

            _table = dbc.GetTableContent(tableName);
            Trace.WriteLine("Table " + tableName + " loaded");
        }
    }
}
