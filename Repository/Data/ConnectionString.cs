using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class ConnectionString
    {
        public  string servername = "";
        public void setServername(string name)
        {
            servername = name;
        }
        public  string getServername()
        {
            return servername;
        }

        public  string database = "";
        public void setDatabase(string data)
        {
            database = data;
        }
        public  string getDatabase()
        {
            return database;
        }
        public  string conString = "";
        
        public  string getConnectionString() {
            conString = "@DaData Source = " + getServername() + "; Initial Catalog = " + getDatabase() + "; Integrated Security = True; Timeout = 30";
            return conString;
        }


    }
}
