using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static WpfApp2019.ViewModel.FileViewModel;
using System.Collections.ObjectModel;

namespace WpfApp2019.Model
{
    public class FileModel
    {
        public ObservableCollection<ObjectAttributes> Files{ get ; set; }
    }

}
