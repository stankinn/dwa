using System.Windows;
using System.Windows.Controls;
using System.IO;
using TemplateTestCase;
using System.Data.SqlClient;
using RazorEngineCore;

namespace WpfApp2019
{

    public partial class AddEntity : Page
    {
        //die verbindung muss individuell angepasst sein
        //public static SqlConnection con = new SqlConnection("Data Source=DESKTOP-VD7D8FL;Initial Catalog=databaseConnection;Integrated Security=True");

        public AddEntity()
        {
            InitializeComponent();
        }

        private void Button_Add_Entity(object sender, RoutedEventArgs e)
        {
            //con.Open();

            IRazorEngine razorEngine = new RazorEngine();
            string templateText = File.ReadAllText("..\\..\\..\\TextFile1-NormalTemplate.txt");
            var model = new Entity("");
            model.Name = EntityName.Text;
            model.Attributes.Add(new EntityAttribute() { Name = AttributeNames.Text, Type = AttributeType.Text, DataType = AttributeDataType.Text });
            System.Diagnostics.Debug.WriteLine(model.Attributes);
            IRazorEngineCompiledTemplate template = razorEngine.Compile(templateText);
            string resultText = template.Run(model);
            File.WriteAllText("..\\..\\..\\WriteText.txt", resultText);
            System.Diagnostics.Debug.WriteLine(resultText);

            //SqlCommand cmd = new SqlCommand("INSERT INTO USERS(username, email, phone) values ('" + EntityName.Text + "','" + AttributeType.Text + "','" + AttributeDataType.Text + "')", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
        }

        private void Button_Go_Back(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            View.FileView mainPage = new View.FileView();
            this.NavigationService.Navigate(mainPage);
        }
    }
}