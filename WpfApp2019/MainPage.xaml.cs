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
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using System.Windows.Forms;

namespace WpfApp2019
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                        
                        Files data = new(Path.GetFileName(d));
                        FileList.Items.Add(data.Name);

                    }
                }
                catch (System.Exception excpt)
                {
                    Console.WriteLine(excpt.Message);
                }

                // System.Diagnostics.Debug.WriteLine(sPath);

                //Properties.Settings.Default.Reload();
            }
        }

        private void Button_Navigate_Add(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            AddEntity addEntityPage = new AddEntity();
            this.NavigationService.Navigate(addEntityPage);
        }

    }

    public class Files
    {
        protected string name;
        protected string type;
        protected DateTime modifiedTime;
        protected string owner;
        protected string description;


        Files(string name, string type, DateTime modifiedTime, string owner, string description)
        {
            this.name = name;
            this.type = type;
            this.modifiedTime = modifiedTime;
            this.owner = owner;
            this.description = description;
        }
        Files(string name)
        {
            this.name = name;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime ModificationTime { get; set; }
        public string Owner { get; set; }
        public string OwnerDescription { get; set; }
    }
}
