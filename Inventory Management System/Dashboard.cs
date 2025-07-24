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
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            GetProductCount();
            GetCategoryCount();
            GetInvoiceCount();
        }
        private void GetProductCount()
        {
            int count = 0;
            string connectionString = @"Server=ACER-NITRO-5;Database=inventory;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) AS total_products FROM Products";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            label1.Text = count.ToString();
            
        }
        private void GetCategoryCount()
        {
            int count = 0;
            string connectionString = @"Server=ACER-NITRO-5;Database=inventory;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) AS total_products FROM Categories";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            label2.Text = count.ToString();

        }
        private void GetInvoiceCount()
        {
            int count = 0;
            string connectionString = @"Server=ACER-NITRO-5;Database=inventory;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) AS total_products FROM Invoices";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            label6.Text = count.ToString();

        }
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetProductCount();
            GetCategoryCount();
            GetInvoiceCount();
        }
    }
}
