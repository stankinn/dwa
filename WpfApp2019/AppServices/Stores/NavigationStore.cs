using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2019.Stores
{
    internal class NavigationStore
    {

        private NavigationStore()
        { }
        private static readonly NavigationStore _instance = new NavigationStore();

        internal static NavigationStore Instance { get { return _instance; } }

        private object _currViewModel;

        public object CurrViewModel
        {
            get => _currViewModel;
            set
            {
                _currViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        public event Action CurrentViewModelChanged;
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


    }
}
