using System;
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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCategories();
        }

        Functions Con;

        private void ShowCategories()
        {
            string Query = "select * from CategoryTbl";
            CategoriesList.DataSource = Con.GetData(Query);
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Query = "insert into CategoryTbl values('{0}')";
                    Query = string.Format(Query, Name);
                    Con.SetData(Query); // Execute insert query to add new category
                    ShowCategories(); // Refresh DataGridView with updated data
                    MessageBox.Show("Category Added");
                    NameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Query = "UPDATE CategoryTbl SET CatName = '{0}' WHERE CatCode = {1}";
                    Query = string.Format(Query, Name, Key);
                    Con.SetData(Query); // Execute the UPDATE query
                    ShowCategories(); // Refresh DataGridView with updated data
                    MessageBox.Show("Category Updated!!!");
                    NameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        int Key = 0;
        private void CategoriesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = CategoriesList.SelectedRows[0].Cells[1].ToString();

            if(NameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CategoriesList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    // Get the selected category code
                    int catCode = Key;

                    // Construct the DELETE query
                    string Query = "DELETE FROM CategoryTbl WHERE CatCode = {0}";
                    Query = string.Format(Query, catCode);

                    // Execute the DELETE query
                    Con.SetData(Query);

                    // Refresh the DataGridView to reflect the changes
                    ShowCategories();

                    MessageBox.Show("Category Deleted!!!");
                    NameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

    }
}