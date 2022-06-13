using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Controls;
using System.ComponentModel;
using Path = System.IO.Path;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System;


namespace WpfApp2019
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

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
                //FileList.Items.Clear();
                FileList.ItemsSource = new List<FileAttributes>();

                string sPath = folderDialog.SelectedPath;

                FilePath.Text = sPath;


                try
                {
                    var files = Directory.EnumerateFileSystemEntries(sPath);
                    List<FileAttributes> items = new List<FileAttributes>();

                    foreach (var d in files)
                    {
                        // FileList.Items.Add(Path.GetFileName(d));
                        items.Add(new FileAttributes(){
                            Name = Path.GetFileName(d),
                            Type = Path.GetExtension(d), // oder MimeMapping.MimeUtility.GetMimeMapping(Path.GetFileName(d)) -> NuGet-Paket: MimeMapping
                            ModificationTime = Directory.GetLastWriteTime(d),
                            Owner = "Owner",
                            Description = "Description"});

                    }
                    FileList.ItemsSource = items;
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

        public void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                FileList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            FileList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

        }

    }

    class FileAttributes
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime ModificationTime { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
    }

    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }

}
