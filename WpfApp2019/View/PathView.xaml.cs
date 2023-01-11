
using System.Windows.Controls;
using WpfApp2019.ViewModel;
using WpfApp2019.Database;
using System.Collections.Generic;

namespace WpfApp2019.View
{
    /// <summary>
    /// Interaktionslogik für PathView.xaml
    /// </summary>
    public partial class PathView : UserControl
    {

        public PathView()
        {
            InitializeComponent();
        }

        private void TreeView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            //databaseConnection.GetTableContent("Products");
            databaseConnection.GetDataType("Products");
        }
    }
}
