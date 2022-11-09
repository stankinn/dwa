using System;
using System.Collections.ObjectModel;
using WpfApp2019.Model;
using Path = System.IO.Path;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.ComponentModel;

namespace WpfApp2019.ViewModel
{
    public class FileViewModel
    {
        public FileViewModel()
        {
            LoadObjects();
        }

        public ObservableCollection<ObjectAttributes> Files { get; set; }

        public PathText FilePathText { get; set; } 

        public void SearchFiles()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                //Path wird gesetzt

                string sPath = folderDialog.SelectedPath;

                
                PathText pt = new PathText();
                pt.FPath = sPath;

                FilePathText = pt;

                Trace.WriteLine("click: " + FilePathText.FPath);

            }
        }

        public void LoadObjects()
        {
            string sPath = "";
            //PathText pt = new PathText();

            Trace.WriteLine("LOADING...");

            if (FilePathText != null)
            {
                sPath = FilePathText.FPath;
                Trace.WriteLine("pp: " + sPath);
            }
            

            try
            {
                var files = Directory.EnumerateFileSystemEntries(sPath);
                ObservableCollection<ObjectAttributes> items = new ObservableCollection<ObjectAttributes>();
                foreach (var d in files)
                {
                    var accessControl = new FileInfo(d).GetAccessControl();
                    items.Add(new ObjectAttributes
                    {
                        Name = Path.GetFileName(d),
                        Type = GetDataType(d),
                        ModificationTime = Directory.GetLastWriteTime(d),
                        Owner = accessControl.GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                        Description = GetFileDescription(d),
                        FilePath = Path.GetFullPath(d)
                    });
                }
                Files = items;
                //FileList.ItemsSource = items;
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private string GetDataType(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return Path.GetExtension(file);
            }
            else
            {
                return "Dateiordner";
            }
        }

        private string GetFileDescription(string file)
        {
            if (Path.GetExtension(file) != "")
            {
                return FileVersionInfo.GetVersionInfo(file).FileDescription;
            }
            else
            {
                return "";
            }
        }
    }
}
