namespace WpfApp2019.TreeView
{
    /// <summary>
    /// Information about a directory item such as a drive, a file or a folder
    /// </summary>
    public class TreeViewItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public TreeViewItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type == TreeViewItemType.Drive ? this.FullPath : TreeViewStructure.GetFileFolderName(this.FullPath); } }
    }

    public enum TreeViewItemType
    {
        /// <summary>
        /// A logical drive
        /// </summary>
        Drive,
        /// <summary>
        /// A phyiscal file
        /// </summary>
        File,
        /// <summary>
        /// A folder
        /// </summary>
        Folder
    }
}
