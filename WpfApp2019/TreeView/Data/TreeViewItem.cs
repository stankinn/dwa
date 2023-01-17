namespace WpfApp2019.TreeView
{
    // Information about a directory item such as a drive, a file or a folder
    public class TreeViewItem
    {
        // The type of this item
        public TreeViewItemType Type { get; set; }

        // The absolute path to this item
        public string FullPath { get; set; }

        // The name of this directory item
        public string Name { get { return this.Type == TreeViewItemType.Drive ? this.FullPath : TreeViewStructure.GetFileFolderName(this.FullPath); } }
    }

    public enum TreeViewItemType
    {
        // A logical drive
        Drive,
        // A phyiscal file
        File,
        // A folder
        Folder,
        // A table
        Table
    }
}
