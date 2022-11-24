using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.Model;

namespace WpfApp2019.ViewModel
{
    internal class AddEntityViewModel:ObservableObject
    {
        public AddEntityViewModel()
        {

        }

        private ICommand _addEntityCommand;
        public ICommand AddEntityCommand
        {
            get
            {
                if (_addEntityCommand == null)
                {
                    _addEntityCommand = new RelayCommand(
                        param => this.AddEntity()
                    );
                }
                return _addEntityCommand;
            }

        }
        public void AddEntity()
        {

        }

    }
}
