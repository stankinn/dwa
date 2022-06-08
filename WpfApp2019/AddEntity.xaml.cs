using System.Windows;
using System.Windows.Controls;
using System.IO;
using TemplateTestCase;
using RazorEngine;
using RazorEngine.Templating;


namespace WpfApp2019
{
    /// <summary>
    /// Interaktionslogik für AddEntity.xaml
    /// </summary>
    public partial class AddEntity : Page
    {
        public AddEntity()
        {
            InitializeComponent();
        }

        private void Button_Add_Entity(object sender, RoutedEventArgs e)
        {

            string templateText = File.ReadAllText("..\\..\\..\\TextFile1-NormalTemplate.txt");
            var model = new Entity("");
            model.Name = EntityName.Text;
            model.Attributes.Add(new EntityAttribute() { Name = AttributeNames.Text, Type = AttributeType.Text, DataType = AttributeDataType.Text });
            System.Diagnostics.Debug.WriteLine(model.Attributes);
            var resultText = Engine.Razor.RunCompile(templateText, "TextNormal", null, model);
            File.WriteAllTextAsync("..\\..\\..\\WriteText.txt", resultText);
            System.Diagnostics.Debug.WriteLine(resultText);

        }

      /*  private void Button_Go_Back(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            MainPage mainPage = new MainPage();
            this.NavigationService.Navigate(mainPage);
        }*/
    }
}
