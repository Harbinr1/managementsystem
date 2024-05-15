using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Managment_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
                return;
            }

            using (SqlConnection con = new SqlConnection("Data Source=HARBIN\\SQLEXPRESS;Initial Catalog=SystemManagmentDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Register WHERE Username = @username AND Password = @password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", UNameTb.Text);
                    cmd.Parameters.AddWithValue("@password", PasswordTb.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Successful login
                        Items Obj = new Items();
                        Obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Invalid login
                        MessageBox.Show("Wrong Username or Password");
                    }
                }
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registrationForm = new RegisterForm();
            registrationForm.Show();
        }
    }
}
