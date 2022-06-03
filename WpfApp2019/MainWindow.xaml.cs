using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;
using TemplateTestCase;
using RazorEngine;
using RazorEngine.Templating;


namespace WpfApp2019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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

                //Properties.Settings.Default.Reload();
            }
        }
        private void Button_Test_Click(object sender, RoutedEventArgs e)
        {

            string templateText = File.ReadAllText("..\\..\\..\\TextFile1-NormalTemplate.txt");
            var model = new Entity("Bert");
            model.Name = EntityName.Text;
            model.Attributes.Add(new EntityAttribute() { Name = AttributeNames.Text, Type = AttributeType.Text, DataType = AttributeDataType.Text });
            System.Diagnostics.Debug.WriteLine(model.Attributes);
            var resultText = Engine.Razor.RunCompile(templateText, "TextNormal", null, model);
            File.WriteAllTextAsync("..\\..\\..\\WriteText.txt", resultText);
            //Console.Write(resultText);
            System.Diagnostics.Debug.WriteLine(resultText);

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
