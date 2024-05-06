using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Managment_System
{
    public partial class Billing : Form
    {
        public Billing()
        {
            Con = new Functions();
            InitializeComponent();
            ShowItems();
        }

        Functions Con;
        private void ShowItems()
        {
            string Query = "Select * from ItemTbl";
            ItemList.DataSource = Con.GetData(Query);
        }

        private void Billing_Load(object sender, EventArgs e)
        {

        }

        int Key = 0;
        int Stock;

        private void ItemsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = ItemList.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = ItemList.SelectedRows[0].Cells[3].Value.ToString();
            Stock = Convert.ToInt32(ItemList.SelectedRows[0].Cells[4].Value.ToString());
            if (NameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemList.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            PriceTb.Text = "";
        }

        private void UpdateStock()
        {

            try
            {
                int NewStcok = Stock - Convert.ToInt32(QtyTb.Text);
                string Query = "Update ItemTbl set ItStock = {0} where ItCode = {1}";
                Query = string.Format(Query, NewStcok, Key);
                Con.SetData(Query);
                ShowItems();
                MessageBox.Show("Stock Updated");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        string PMethod = "";
        int n = 0;
        int GrdTotal = 0;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            if (PriceTb.Text == "" || QtyTb.Text == "" || NameTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                int Qte = Convert.ToInt32(QtyTb.Text);
                int total = Convert.ToInt32(PriceTb.Text) * Qte;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ClientBill);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = NameTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = "Eur " + total;
                ClientBill.Rows.Add(newRow);
                n++;
                GrdTotal += total;
                GrdTotalLbl.Text = "Eur " + GrdTotal;
                UpdateStock();
                ShowItems();
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || CustTb.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                try
                {
                    if (MobileRadio.Checked == true)
                    {
                        PMethod = "Mobile";
                    }
                    else if (CardRadio.Checked == true)
                    {
                        PMethod = "Card";
                    }
                    else
                    {
                        PMethod = "Cash";
                    }
                    string Name = NameTb.Text;
                    string Query = "INSERT INTO BillTbl (BDate, Customer, Amount, PaymentMode) VALUES ('{0}', '{1}', {2}, '{3}')";
                    Query = string.Format(Query, DateTime.Today.Date, CustTb.Text, GrdTotal, PMethod);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Bill Added");

                    // Update printable bill string
                    PrintableBill += $"Date: {DateTime.Today.Date}\nCustomer: {CustTb.Text}\nPayment Mode: {PMethod}\n\n";
                    PrintableBill += "Items:\n";
                    foreach (DataGridViewRow row in ClientBill.Rows)
                    {
                        PrintableBill += $"{row.Cells[0].Value}. {row.Cells[1].Value} - {row.Cells[2].Value} x {row.Cells[3].Value} = {row.Cells[4].Value}\n";
                    }
                    PrintableBill += $"\nTotal Amount: Eur {GrdTotal}";

                    // Print the document
                    printDocument1.Print();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private string PrintableBill = ""; // Add this variable at class level to hold the printable bill

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Draw the printable bill on the print page
            e.Graphics.DrawString("Bill", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(10, 10));
            e.Graphics.DrawString(PrintableBill, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 50));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printdialog1 = new PrintDialog();
            printdialog1.Document = printDocument1;

            DialogResult result = printdialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
