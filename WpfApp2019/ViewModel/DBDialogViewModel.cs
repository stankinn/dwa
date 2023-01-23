using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2019.AppServices;
using WpfApp2019.AppServices.Dialog;
using WpfApp2019.Model;
using IDialogWindow = WpfApp2019.AppServices.Dialog.IDialogWindow;
using Repository.Data;
using WpfApp2019.Database;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Windows.Controls;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using System.Data.SqlTypes;
using System.Data;

namespace WpfApp2019.ViewModel
{
    internal class DBDialogViewModel : DialogViewModelBase<DialogResults>
    {
        private ServerNameModel _server;
        private DatabaseModel _database;

        private ICommand _okCommand;
        private ICommand _cancelCommand;

        IEventAggregator _ea;

        public DBDialogViewModel()
        {
            _server = new ServerNameModel();
            _database = new DatabaseModel();
            _ea = ApplicationService.Instance.EventAggregator;
        }
        
        public ServerNameModel Server
        {

            get => _server;
            set
            {
                if (_server != value)
                {
                    _server = value;
                    OnPropertyChanged(nameof(Server));
                }
            }
        }
       
        public DatabaseModel Database
        {

            get => _database;
            set
            {
                if (_database != value)
                {
                    _database = value;
                    OnPropertyChanged(nameof(Database));
                }
            }
        }


        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        param => this.CancelButton(param as IDialogWindow)
                    );
                }
                return _cancelCommand;
            }

        }
        public ICommand OKCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(
                        param => this.OKButton(param as IDialogWindow)
                    );
                }
                return _okCommand;
            }

        }

        private DbConnection _dbCon;
        public DbConnection DbCon
        {
            get => _dbCon;
            set
            {
                if (_dbCon != value)
                {
                    _dbCon = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OKButton(IDialogWindow window)
        {

            ConnectionString conString = new ConnectionString();
            DatabaseConnection con = new DatabaseConnection();
            PathViewModel pvm = new PathViewModel();
            conString.setServername(Server.ServerName);
            conString.setDatabase(Database.DatabaseName);

            try {
                con.OpenConnection(conString.getConnectionString());

                string sqlString = conString.getConnectionString();
                App.Current.Properties["SqlConnectionString"] = sqlString;
                SaveConTxt(sqlString);

                DbCon = new DbConnection { Connected = true };
                _ea.GetEvent<DbConnectionChangedEvent>().Publish(DbCon);

                CloseDialogWithResult(window, DialogResults.OK);


            } catch { 
                Trace.WriteLine("Connection couldn't be opened");
            }

        }
         private void SaveConTxt(string con)
        {
            string strAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var values = strAssemblyPath.Split(@"\bin");
            string fileName = values[0] + @"\Database\ConnectionString.txt";

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] connection = new UTF8Encoding(true).GetBytes(con);
                    fs.Write(connection, 0, connection.Length);
                }

                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void CancelButton(IDialogWindow window)
        {
           
            CloseDialogWithResult(window, DialogResults.Cancel);

        }

    }
}
