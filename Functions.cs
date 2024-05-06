using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Management;

namespace Managment_System
{
     class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private DataTable dt;
        private string ConStr;
        private SqlDataAdapter Sda;

        public int SetData (string Query)
        {
            int Cnt;
            if(Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            Cmd.CommandText = Query;
            Cnt = Cmd.ExecuteNonQuery();
            Con.Close();
            return Cnt;
        }
        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            Sda = new SqlDataAdapter(Query,ConStr);
            Sda.Fill(dt);
            return dt;
        }

        public Functions()
        {
            ConStr = @"Data Source=HARBIN\SQLEXPRESS;Initial Catalog=SystemManagmentDb;Integrated Security=True;TrustServerCertificate=True";
            Con = new SqlConnection(ConStr);
            Cmd = new SqlCommand();
            Cmd.Connection = Con;
        }
    }


}
 