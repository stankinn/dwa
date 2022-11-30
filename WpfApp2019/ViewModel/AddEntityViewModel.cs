using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp2019.Model;
using WpfApp2019.View;

namespace WpfApp2019.ViewModel
{
    public class AddEntityViewModel : INavigationAware
    {
        private IContainer container;
        private IRegionManager regionManager;
        private IRegionNavigationService navigationService;

        public AddEntityViewModel()
        {
            this.container = ApplicationService.Instance.Container;
            this.regionManager = ApplicationService.Instance.RegionManager;
            regionManager.RegisterViewWithRegion("AddEntityViewRegion", typeof(AddEntityView));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Do stuff here

            //For navigation back
            navigationService = navigationContext.NavigationService;
        }

        #region Executes
        /// <summary>
        /// Command when ViewB button clicked
        /// </summary>
        public void Execute_ViewBCommand()
        {
            regionManager.RequestNavigate("AddEntityViewRegion", new Uri("PathView", UriKind.Relative));
        }

        #endregion
    }
}
