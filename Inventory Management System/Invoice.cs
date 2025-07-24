using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Invoice : UserControl
    {
        public Invoice()
        {
            InitializeComponent();
            LoadInvoices();
        }
        public class InvoiceModel
        {
            public DateTime Date { get; set; }
            public int No { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string invoiceType{ get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public string Category { get; set; }
            public double Amount { get; set; }
            public double Discount { get; set; }
            public double Total { get; set; }

        }
        public List<InvoiceModel> GetProductsFromDatabase()
        {
            List<InvoiceModel> invoices = new List<InvoiceModel>();
            string connectionString = @"ConnectionString";
            string query = @"SELECT * From Invoices";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InvoiceModel invoice = new InvoiceModel
                        {
                            No = reader.GetInt32(0),
                            ID = reader.GetString(1),
                            Name = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            Category = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty,
                            UnitPrice = reader.GetDouble(5),
                            Amount= reader.GetDouble(6),
                            Discount = reader.GetDouble(7),
                            Total = reader.GetDouble(8),
                            Date = reader.GetDateTime(9),
                            invoiceType = reader.GetString(10)

                        };

                        invoices.Add(invoice);
                    }
                }
                connection.Close();
            }
            // Add actual database fetching logic here
            return invoices;
        }
         private void LoadInvoices()
        {
            flowLayoutPanel1.Controls.Clear();
            Invoiceword invoicewordCont = new Invoiceword();
            flowLayoutPanel1.Controls.Add(invoicewordCont);

            List<InvoiceModel> invoices = GetProductsFromDatabase();
             

             foreach (var invoice in invoices)
             {
                 invoicecard invoiceControl = new invoicecard();
                invoiceControl.invoiceNo = invoice.No.ToString();
                invoiceControl.productID = invoice.ID;
                invoiceControl.productName = invoice.Name;
                invoiceControl.quantity = invoice.Quantity.ToString();
                invoiceControl.category = invoice.Category;
                invoiceControl.price = invoice.UnitPrice.ToString();
                invoiceControl.amount = invoice.Amount.ToString();
                invoiceControl.Discount = invoice.Discount.ToString();
                invoiceControl.Total = invoice.Total.ToString();
                DateTime datewithoutTime = invoice.Date.Date;
                invoiceControl.Date = datewithoutTime.ToString("yyyy-MM-dd");
                invoiceControl.invoiceType = invoice.invoiceType;

                 flowLayoutPanel1.Controls.Add(invoiceControl);
             }
        }

        private void AddInvoiceSell()
        {
            string Quantity = textBox2.Text;
            string discount = textBox5.Text;
            string invoiceType = "Sale";
            string invoiceId = textBox1.Text;
            string Date = textBox4.Text;
            DateTime converted = DateTime.Parse(Date);
            string prodname = textBox3.Text;
            string Id = "";
            string Name = "";
            int prodQuantity = 0;
            double Price = 0;
            string Category = "";

            string connectionString = @"ConnectionString";
            string query = @"SELECT p.productID, p.productName, p.quantity, p.price, c.category
                           FROM Products p
                           LEFT JOIN Categories c ON p.categoryId = c.categoryId
                           Where p.productName = @prodName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@prodName", prodname);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            Id = reader.GetString(0);
                            Name = reader.GetString(1);
                            prodQuantity = reader.GetInt32(2);
                            Price = reader.GetDouble(3);
                            Category = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                       
                    }
                }
                connection.Close();
            }
            double convertedDiscount = Convert.ToDouble(discount);
            double convertedQuantity = Convert.ToDouble(Quantity);
            double convertedPrice = Convert.ToDouble(Price);
            double comvertedprodQuantity = Convert.ToDouble(prodQuantity);
            double amount = convertedQuantity* convertedPrice;
            double total = amount -(amount*(convertedDiscount/100));
            double quanchange = comvertedprodQuantity - convertedQuantity;
            
            string query1 = @"INSERT INTO Invoices VALUES(@invoiceNo,@productID,@productName,@quantity,@caregory,@unitPrice,@Amount,@Discount,@Total,@Date,@invoiceType)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query1, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoiceId);
                command.Parameters.AddWithValue("@productID", Id);
                command.Parameters.AddWithValue("@productName", Name);
                command.Parameters.AddWithValue("@quantity", Quantity);
                command.Parameters.AddWithValue("@caregory", Category);
                command.Parameters.AddWithValue("@unitPrice", Price);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Discount", discount);
                command.Parameters.AddWithValue("@Date", converted);
                command.Parameters.AddWithValue("@invoiceType", invoiceType);
                command.Parameters.AddWithValue("@Total", total);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            string query2 = @"UPDATE Products SET quantity = @Quantitypro WHERE productName = @productName";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@Quantitypro", quanchange);
                command.Parameters.AddWithValue("@productName", Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            LoadInvoices();

        }
        private void AddInvoiceReturn()
        {
            string Quantity = textBox2.Text;
            string discount = textBox5.Text;
            string invoiceType = "Return";
            string invoiceId = textBox1.Text;
            string Date = textBox4.Text;
            DateTime converted = DateTime.Parse(Date);
            string prodname = textBox3.Text;
            string Id = "";
            string Name = "";
            int prodQuantity = 0;
            double Price = 0;
            string Category = "";

            string connectionString = @"ConnectionString";
            string query = @"SELECT p.productID, p.productName, p.quantity, p.price, c.category
                           FROM Products p
                           LEFT JOIN Categories c ON p.categoryId = c.categoryId
                           Where p.productName = @prodName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@prodName", prodname);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Id = reader.GetString(0);
                        Name = reader.GetString(1);
                        prodQuantity = reader.GetInt32(2);
                        Price = reader.GetDouble(3);
                        Category = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;

                    }
                }
                connection.Close();
            }
            double convertedDiscount = Convert.ToDouble(discount);
            double convertedQuantity = Convert.ToDouble(Quantity);
            double convertedPrice = Convert.ToDouble(Price);
            double comvertedprodQuantity = Convert.ToDouble(prodQuantity);
            double amount = convertedQuantity * convertedPrice;
            double total = amount - (amount * (convertedDiscount / 100));
            double quanchange = comvertedprodQuantity + convertedQuantity;

            string query1 = @"INSERT INTO Invoices VALUES(@invoiceNo,@productID,@productName,@quantity,@caregory,@unitPrice,@Amount,@Discount,@Total,@Date,@invoiceType)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query1, connection);
                command.Parameters.AddWithValue("@invoiceNo", invoiceId);
                command.Parameters.AddWithValue("@productID", Id);
                command.Parameters.AddWithValue("@productName", Name);
                command.Parameters.AddWithValue("@quantity", Quantity);
                command.Parameters.AddWithValue("@caregory", Category);
                command.Parameters.AddWithValue("@unitPrice", Price);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Discount", discount);
                command.Parameters.AddWithValue("@Date", converted);
                command.Parameters.AddWithValue("@invoiceType", invoiceType);
                command.Parameters.AddWithValue("@Total", total);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            string query2 = @"UPDATE Products SET quantity = @Quantitypro WHERE productName = @productName";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@Quantitypro", quanchange);
                command.Parameters.AddWithValue("@productName", Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            LoadInvoices();

        }


        private void Invoice_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddInvoiceSell();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddInvoiceReturn();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}   
