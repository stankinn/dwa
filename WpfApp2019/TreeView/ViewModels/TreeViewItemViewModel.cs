using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace WpfApp2019.TreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class TreeViewItemViewModel : TreeViewBase
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public TreeViewItemType Type { get; set; }

        public string ImageName => Type == TreeViewItemType.Drive ? "drive" : (Type == TreeViewItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type == TreeViewItemType.Drive ? this.FullPath : TreeViewStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != TreeViewItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
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

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public TreeViewItemViewModel(string fullPath, TreeViewItemType type)
        {
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

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<TreeViewItemViewModel>();

            // Show the expand arrow if we are not a file
            if (this.Type != TreeViewItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // We cannot expand a file
            if (this.Type == TreeViewItemType.File)
                return;

            // Find all children
            var children = TreeViewStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<TreeViewItemViewModel>(
                                children.Select(content => new TreeViewItemViewModel(content.FullPath, content.Type)));

            Trace.WriteLine(this.Children.Count + " Children");
        }
    }
}
