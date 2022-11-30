using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Ioc;
using WpfApp2019.View;

namespace WpfApp2019
{
    internal sealed class ApplicationService : IModule
    {

        private IContainer _container;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        private ApplicationService()
        { }
        private static readonly ApplicationService _instance = new ApplicationService();

        internal static ApplicationService Instance { get { return _instance; } }

       
        internal IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null) { _eventAggregator = new EventAggregator(); }

                return _eventAggregator;
            }
        }
        
        internal IContainer Container
        {
            get
            {
                if (_container == null) { _container = new Container(); }

                return _container;
            }
        }

  
        internal IRegionManager RegionManager
        {
            get
            {
                if (_regionManager == null) { _regionManager = new RegionManager(); }

                return _regionManager;
            }
        }



        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Setup Event listeners etc...
            _regionManager = containerProvider.Resolve<IRegionManager>();
            //We get from the container an instance of ViewB.
            var entityView = containerProvider.Resolve<AddEntityView>();
            var pathView = containerProvider.Resolve<PathView>();

            //We get from the region manager our target region.
            IRegion entityRegion = _regionManager.Regions["AddEntityViewRegion"];
            IRegion pathRegion = _regionManager.Regions["PathViewRegion"];

            //We inject the view into the region.
            entityRegion.Add(entityView);
            pathRegion.Add(pathView);
        }

        /// <summary>
        /// Register your views for this Module
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AddEntityView>("AddEntityViewRegion");
            containerRegistry.RegisterForNavigation<PathView>("PathViewRegion");

        }
    }
}
