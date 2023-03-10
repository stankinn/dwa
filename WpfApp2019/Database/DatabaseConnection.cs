using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using Repository.Data;

namespace WpfApp2019.Database
{
    internal class DatabaseConnection
    {
        private SqlConnection sqlConn;

        public void OpenConnection(string conString)
        {
            //öffne Verbindung zur Datenbank

            sqlConn = new SqlConnection(conString);
            sqlConn.Open();
            
        }

        public List<string> GetTableNames() 
        {
            string conString = App.Current.Properties["SqlConnectionString"].ToString();
            sqlConn = new SqlConnection(conString);

            //Rückgabe der Tabellennamen ohne Migration History
            if(sqlConn.State != ConnectionState.Open){
               OpenConnection(conString);
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

        public DataTable GetTableContent(String tablename)
        {
            string conString = App.Current.Properties["SqlConnectionString"].ToString();
            sqlConn = new SqlConnection(conString);

            string sqlQuery = null;
            sqlQuery = "SELECT *FROM "+tablename;
            if (sqlConn == null || sqlConn.State != ConnectionState.Open )
            {
                OpenConnection(conString);
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

            return dtData;
        }

        public List<List<string>> GetDataType(string tablename)
        {
            string conString = App.Current.Properties["SqlConnectionString"].ToString();
            sqlConn = new SqlConnection(conString);

            string sqlQuery = null;
            sqlQuery = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tablename+ "'";
            if (sqlConn == null || sqlConn.State != ConnectionState.Open)
            {
                OpenConnection(conString);
            }
            SqlDataAdapter dscmd = new SqlDataAdapter(sqlQuery, sqlConn);
            DataTable dtData = new DataTable();
            dscmd.Fill(dtData);
            string res = string.Join(Environment.NewLine, dtData.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
            //MessageBox.Show(res);

            var lines = res.Split(Environment.NewLine);

            List<string> list = new List<string>();
            foreach (String line in lines)
            {
                list.Add(line);
            }
            List<string> dataTypes = new List<string>();
            List<string> dataNames = new List<string>();
            bool bla = false;
            foreach (string line in list)
            {
                var values = line.Split(';');
                foreach (var item in values)
                {
                    if (!bla)
                    {
                        dataNames.Add(item);
                        bla = true;
                    }
                    else
                    {
                        dataTypes.Add(item);
                        bla=false;
                    }
                }
            }

            List<List<string>> Datatypes_Names = new List<List<string>>();
            Datatypes_Names.Add(dataNames);
            Datatypes_Names.Add(dataTypes);

            //for(int i = 0; i < Datatypes_Names.Count; i++)
            //{
            //    for( int j = 0; j < Datatypes_Names[i].Count; j++)
            //    {
            //        Trace.Write(Datatypes_Names[i][j]);
            //    }

            //}
            
            return Datatypes_Names;
        }

    }
}
