using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2019.AppServices.Dialog
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> vm);
    }
}
