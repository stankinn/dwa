using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2019.AppServices;

namespace WpfApp2019.Model
{
    internal class TreeModel
    {

    }
    public class Item : ViewModelBase
    {
        private string _Title;
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Item> _SubItems = new ObservableCollection<Item>();
        public ObservableCollection<Item> SubItems
        {
            get
            {
                return this._SubItems;
            }
            set
            {
                if (this._SubItems != value)
                {
                    this._SubItems = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
