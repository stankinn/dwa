using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WpfApp2019.Database
{
    internal class DatabaseConnection
    {
        private SqlConnection sqlConn;

        public void OpenConnection()
        {
            //öffne Verbindung zur Datenbank
            
            string conString = null;
            conString = @"Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=True";
            sqlConn = new SqlConnection(conString);
            sqlConn.Open();
            

        }

        public List<string> GetTableNames() 
        {
            //Rückgabe der Tabellennamen ohne Migration History
            if(sqlConn.State != ConnectionState.Open){
               OpenConnection();
            }
            DataTable schema = sqlConn.GetSchema("Tables");
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                TableNames.Add(row[2].ToString());
            }
            if (TableNames[0].Contains("MigrationsHistory"))
            {
                TableNames.RemoveAt(0);
            }
            return TableNames;

        }

        public void GetTableContent(String tablename)
        {
            string sqlQuery = null;
            sqlQuery = "SELECT *FROM "+tablename;
            if (sqlConn == null || sqlConn.State != ConnectionState.Open )
            {
                OpenConnection();
            }
            SqlDataAdapter dscmd = new SqlDataAdapter(sqlQuery, sqlConn);
            DataTable dtData = new DataTable();
            dscmd.Fill(dtData);
            //einfügen in das Datagridview
            // dataGridView1.Datasource = dtData;


            //nur um zu testen ob die Daten aus der Datenbank auch in dtData stehen
            /*
            string res = string.Join(Environment.NewLine, dtData.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
            MessageBox.Show(res);*/
        }

        public List<string> GetDataType(string tablename)
        {
            string sqlQuery = null;
            sqlQuery = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tablename+ "'";
            if (sqlConn == null || sqlConn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            SqlDataAdapter dscmd = new SqlDataAdapter(sqlQuery, sqlConn);
            DataTable dtData = new DataTable();
            dscmd.Fill(dtData);
            string res = string.Join(Environment.NewLine, dtData.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
            MessageBox.Show(res);
            return new List<string>();
        }

    }
}
