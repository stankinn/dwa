using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;
using System.IO;
using ListView = System.Windows.Controls.ListView;
using Path = System.IO.Path;

namespace DWA_UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                FileList.Items.Clear();
                string sPath = folderDialog.SelectedPath;

                FilePath.Text = sPath;

               
                try
                {
                    var files = Directory.EnumerateFileSystemEntries(sPath);
                    foreach (var d in files)
                    {
                        FileList.Items.Add(Path.GetFileName(d));
                    }
                }
                catch (System.Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                }

               // System.Diagnostics.Debug.WriteLine(sPath);

                Properties.Settings.Default.Reload();
            }
        }

    }

    class Files
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime ModificationTime { get; set; }
        public string Owner { get; set; }
        public string OwnerDescription { get; set; }
    }

}
