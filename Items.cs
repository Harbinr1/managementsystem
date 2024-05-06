using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Managment_System
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            Con = new Functions();
            GetCategories();
            ShowItems();
        }
        Functions Con;

        private void GetCategories()
        {
            string Query = "Select * from CategoryTbl";
            CatCb.ValueMember = Con.GetData(Query).Columns["CatCode"].ToString();
            CatCb.DisplayMember = Con.GetData(Query).Columns["CatName"].ToString();
            CatCb.DataSource = Con.GetData(Query);
        }
      
        int Key = 0;
        private void ShowItems()
        {
            string Query = "Select * from ItemTbl";
            ItemList.DataSource = Con.GetData(Query);
        }

        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ItemList.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
            StockTb.Text = ItemList.SelectedRows[0].Cells[4].Value.ToString();
            ManifacturerTb.Text = ItemList.SelectedRows[0].Cells[5].Value.ToString();
            if(NameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(NameTb.Text == "" || CatCb.Text == "" ||  PriceTb.Text == "" || ManifacturerTb.Text == "" || StockTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                 try
                {
                    string Name = NameTb.Text;
                    string Cat = CatCb.SelectedValue.ToString();
                    string Price = PriceTb.Text;
                    string Stock = StockTb.Text;
                    string Man = ManifacturerTb.Text;
                    string Query = "insert into ItemTbl values('{0}',{1},{2},{3},'{4}')";
                    Query = string.Format(Query, Name, Cat, Price, Stock, Man);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Item Added");
                    NameTb.Text = "";
                    ManifacturerTb.Text = "";
                    CatCb.SelectedIndex = -1;
                    StockTb.Text = "";
                    PriceTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if(NameTb.Text == "" || CatCb.SelectedIndex == -1 || ManifacturerTb.Text == "" || StockTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Cat = CatCb.SelectedValue.ToString();
                    string Price = PriceTb.Text;
                    string Stock = StockTb.Text;
                    string Man = ManifacturerTb.Text;
                    string Query = "Update ItemTbl set ItItem = '{0}', ItCategory = {1}, ItPrice = {2},ItStock = {3},Manufacturer = '{4}' where ItCode = {5}";
                    Query = string.Format(Query,Name,Cat, Price, Stock,Man,Key);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Item Updated");
                    NameTb.Text = "";
                    ManifacturerTb.Text = "";
                    CatCb.SelectedIndex = -1;
                    PriceTb.Text = "";
                    StockTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("No item selected for deletion.");
                return;
            }

            try
            {
                string Query = "DELETE FROM ItemTbl WHERE ItCode = {0}";
                Query = string.Format(Query, Key);
                Con.SetData(Query);
                ShowItems();
                MessageBox.Show("Item deleted successfully.");
                NameTb.Text = "";
                CatCb.SelectedIndex = -1;
                PriceTb.Text = "";
                StockTb.Text = "";
                ManifacturerTb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the item: " + ex.Message);
            }
        }
    }
}
