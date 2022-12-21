using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfApp2019.TreeView
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class TreeViewStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<TreeViewItem> GetLogicalDrives()
        {
            // Get every logical drive on the machine
            return Directory.GetLogicalDrives().Select(drive => new TreeViewItem { FullPath = drive, Type = TreeViewItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<TreeViewItem> GetDirectoryContents(string fullPath)
        {
            // Create empty list
            var items = new List<TreeViewItem>();

            #region Get Folders
            
            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new TreeViewItem { FullPath = dir, Type = TreeViewItemType.Folder }));
            }
            catch { }

            #endregion

            #region Get Files

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new TreeViewItem { FullPath = file, Type = TreeViewItemType.File }));
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // If we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            // Return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
