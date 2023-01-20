using Prism.Commands;
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

namespace WpfApp2019.ViewModel
{
    internal class DBDialogViewModel : DialogViewModelBase<DialogResults>
    {

        private ICommand _okCommand;
        private ICommand _cancelCommand;

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
            CloseDialogWithResult(window, DialogResults.OK);
            //Trace.WriteLine(DialogResult);
        }
        private void CancelButton(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Cancel);
            //Trace.WriteLine(DialogResult);

        }

    }
}
