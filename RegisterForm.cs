using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Managment_System
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=HARBIN\\SQLEXPRESS;Initial Catalog=SystemManagmentDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                con.Open();
                string insertQuery = "INSERT INTO Register (Username, Password, Email, PhoneNumber, Country) " +
                                     "VALUES (@username, @password, @email, @phone, @country)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@username", txtUname.Text);
                    cmd.Parameters.AddWithValue("@password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@country", txtCnt.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Register successfully", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
