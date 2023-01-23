using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfApp2019.AppServices;
using WpfApp2019.ViewModel;
using WpfApp2019.Model;
using System.Windows;

namespace WpfApp2019.TreeView
{
    // A view model for each directory item
    public class TreeViewItemViewModel : ViewModelBase
    {
        #region Public Properties

        // The type of this item
        public TreeViewItemType Type { get; set; }

        public string ImageName => Type == TreeViewItemType.Drive ? "drive" : (Type == TreeViewItemType.Table ? "table" : (Type == TreeViewItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed")));

        // The full path to the item
        public string FullPath { get; set; }

        // The name of this directory item
        public string Name { get { return this.Type == TreeViewItemType.Drive ? this.FullPath : TreeViewStructure.GetFileFolderName(this.FullPath); } }

        // A list of all children contained inside this item

        private ObservableCollection<TreeViewItemViewModel> _children = new ObservableCollection<TreeViewItemViewModel>();
        public ObservableCollection<TreeViewItemViewModel> Children
        {

            get => _children;
            set
            {
                if (_children != value)
                {
                    _children = value;
                    OnPropertyChanged();
                }
            }
        }

        private Table _tablename;
        public Table TableName
        {
            get => _tablename;
            set
            {
                if (_tablename != value)
                {
                    _tablename = value;
                    OnPropertyChanged();
                }
            }
        }

        private GridVisible _gridV;
        public GridVisible GridV
        {
            get => _gridV;
            set
            {
                if (_gridV != value)
                {
                    _gridV = value;
                    OnPropertyChanged();
                }
            }
        }

        // Indicates if this item can be expanded
        public bool CanExpand { get { return (this.Type != TreeViewItemType.File || this.Type != TreeViewItemType.Table); } }

        // Indicates if the current item is expanded or not
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                    // Find all children
                    Expand();
                // If the UI tells us to close
                else
                    this.ClearChildren();

                OnPropertyChanged();
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;

                    if (this.Type == TreeViewItemType.Table)
                    {
                        ChangeVisibility(true);
                        TableName = new Table { Name = this.Name };
                        _ea.GetEvent<DirectoryChangedEvent>().Publish(TableName.Name);
                        _ea.GetEvent<TNameChangedEvent>().Publish(TableName);
                    }
                    else
                    {
                        ChangeVisibility(false);
                        _ea.GetEvent<DirectoryChangedEvent>().Publish(this.FullPath);
                    }
                }
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        IEventAggregator _ea;

        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public TreeViewItemViewModel(string fullPath, TreeViewItemType type)
        {
            _ea = ApplicationService.Instance.EventAggregator;
            // Create commands
            this.ExpandCommand = new TreeViewRelayCommand(Expand);
            
            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // Setup the children as needed
            this.ClearChildren();
        }

        #endregion

        #region Helper Methods

        // Removes all children from the list, adding a dummy item to show the expand icon if required
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<TreeViewItemViewModel>();

            // Show the expand arrow if we are not a file
            if (this.Type != TreeViewItemType.File)
                this.Children.Add(null);
        }

        public void ChangeVisibility(bool show)
        {
            GridViewModel gvm = new GridViewModel();
            if (show)
            {
                GridV = new GridVisible { Visible = Visibility.Visible };
                _ea.GetEvent<GVisibilityChangedEvent>().Publish(GridV);
            }
            else
            {
                GridV = new GridVisible { Visible = Visibility.Hidden };
                _ea.GetEvent<GVisibilityChangedEvent>().Publish(GridV);
            }
        }

        #endregion

        //  Expands this directory and finds all children
        private void Expand()
        {
            // We cannot expand a file or table
            if (this.Type == TreeViewItemType.File || this.Type == TreeViewItemType.Table)
                return;

            // Find all children
            var children = TreeViewStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<TreeViewItemViewModel>(
                                children.Select(content => new TreeViewItemViewModel(content.FullPath, content.Type)));
        }
    }
}