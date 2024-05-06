using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Managment_System
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCostumers();
        }
        Functions Con;

        private void ShowCostumers()
        {
            string Query = "select * from CustomerTbl";
            CustomersList.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(NameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "insert into CustomerTbl values('{0}','{1}','{2}')";
                    Query = string.Format(Query,Name,Gender,Phone);
                    Con.SetData(Query);
                    ShowCostumers();
                    MessageBox.Show("Costumer Added!!!");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }
                catch(Exception Ex) 
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void CustomersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = CustomersList.SelectedRows[0].Cells[1].Value.ToString();
            GenderCb.Text = CustomersList.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = CustomersList.SelectedRows[0].Cells[3].Value.ToString();
            if(NameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersList.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if(NameTb.Text == "" || GenderCb.SelectedIndex == -1 || PhoneTb.Text == "") 
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "update CustomerTbl set Item = '{0}', Gender = '{1}', Phone = '{2}' WHERE CustCode = {3}";
                    Query = string.Format(Query,Name,Gender,Phone,Key);
                    Con.SetData(Query);
                    ShowCostumers();
                    MessageBox.Show("Customer Updated!!!");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || GenderCb.SelectedItem == null || PhoneTb.Text == "")


            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenderCb.SelectedItem.ToString();
                    string Phone = PhoneTb.Text;
                    string Query = "Delete from CustomerTbl where CustCode = {0}";
                    Query = string.Format(Query, Key);
                    Con.SetData(Query);
                    ShowCostumers();
                    MessageBox.Show("Custuomer Deleted!!!");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.SelectedIndex = -1;
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
