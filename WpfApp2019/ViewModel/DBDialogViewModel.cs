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

namespace WpfApp2019.ViewModel
{
    internal class DBDialogViewModel : DialogViewModelBase<DialogResults>
    {
        private ServerNameModel _server;
        private DatabaseModel _database;

        private ICommand _okCommand;
        private ICommand _cancelCommand;

        public DBDialogViewModel()
        {
            _server = new ServerNameModel();
            _database = new DatabaseModel();
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
                pvm.OpenDatabase(sqlString);

                CloseDialogWithResult(window, DialogResults.OK);


            } catch { 
                Trace.WriteLine("Connection couldn't be opened");
            }

        }
        private void CancelButton(IDialogWindow window)
        {
           
            CloseDialogWithResult(window, DialogResults.Cancel);

        }

    }
}
