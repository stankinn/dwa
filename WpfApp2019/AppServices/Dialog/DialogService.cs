using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2019.AppServices.Dialog
{
    public class DialogService : IDialogService
    {
        public T OpenDialog<T>(DialogViewModelBase<T> vm) {
        
            IDialogWindow window = new DialogWindow();
            window.DataContext = vm;

            window.ShowDialog();

            return vm.DialogResult;
        }

    }
}
