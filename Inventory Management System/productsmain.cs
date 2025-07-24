using Inventory_Management_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class productsmain : UserControl
    {
        public productsmain()
        {
            InitializeComponent();
            LoadProducts();
        }
        // ProductModel.cs
        public class ProductModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public string Category { get; set; }
            public string ImagePath { get; set; }
        }

        public List<ProductModel> GetProductsFromDatabase()
        {
            List<ProductModel> products = new List<ProductModel>();
           string connectionString = @"ConnectionString";
            string query = @"SELECT p.productID, p.productName, p.quantity, p.price, c.category, p.imagePath
                           FROM Products p
                           LEFT JOIN Categories c ON p.categoryId = c.categoryId" ;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Quantity = reader.GetInt32(2),
                            Price = reader.GetDouble(3),
                            Category = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty,
                            ImagePath = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty
                        };

                        products.Add(product);
                    }
                }
                connection.Close();
            } 
            // Add actual database fetching logic here
            return products;
        }
        private void LoadProducts()
        {
            flowLayoutPanel1.Controls.Clear();
            List<ProductModel> products = GetProductsFromDatabase();
            label2.Text = $"Products ({products.Count})";

            foreach (var product in products)
            {
                ProductsCard productControl = new ProductsCard();
                productControl.ProductID = product.Id;
                productControl.Productname = product.Name;
                productControl.ProductQuantity = product.Quantity;
                productControl.ProductPrice = product.Price;
                productControl.ProductCategory = product.Category;

                productControl.ProductImage = Image.FromFile(product.ImagePath); // Load image from file
                productControl.ProductClicked += new EventHandler<ProductEventArgs>(ProductControl_ProductClicked);

                flowLayoutPanel1.Controls.Add(productControl);
            }
        }
        public string selectedImagePath = string.Empty;
        private void AddProduct()
        {
            
            string productId = textBox1.Text;
            string productName = textBox2.Text;
            string quantity = textBox4.Text;
            string category = textBox10.Text;
            string Price = textBox3.Text;
            string connectionString = @"ConnectionString";
            string query1 = @"SELECT categoryID FROM Categories WHERE category = @CategoryName";
            string categoryno = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query1, connection);
                command.Parameters.AddWithValue("@CategoryName", category);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categoryno = reader.GetString(0);

                    }
                }
                connection.Close();
            }
            
            string query = @"INSERT INTO Products VALUES(@productID,@productName,@Quantity,@Price,@CategoryID,@imagePath)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@productID", productId);
                command.Parameters.AddWithValue("@productName", productName);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Price", Price);
                command.Parameters.AddWithValue("@CategoryID", categoryno);
                command.Parameters.AddWithValue("@imagePath", selectedImagePath );

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            selectedImagePath = string.Empty;
            LoadProducts();
        }
        private void ProductControl_ProductClicked(object sender, ProductEventArgs e)
        {
            // Make the prodinfo panel visible
            prodinfo.Visible = true;

            // Populate the prodinfo panel with product details
            textBox8.Text = e.ProductID;
            textBox7.Text = e.ProductName;
            textBox6.Text = e.ProductQuantity.ToString();
            textBox5.Text = e.ProductPrice.ToString();
            textBox9.Text = e.ProductCategory;
            pictureBox2.Image = e.ProductImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            // Assuming you have other controls for quantity and price, set them as well
            // e.g., txtQuantity.Text = e.Quantity.ToString();
            // e.g., txtPrice.Text = e.Price.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.icons8_image_100;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox10.Text = string.Empty;
            addprod.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addprod.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            prodinfo.Visible = false;
        }

        private void productsCard1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void productsCard1_Click(object sender, EventArgs e)
        {
            prodinfo.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void prodinfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addprodsub_Click(object sender, EventArgs e)
        {
            AddProduct();
            pictureBox1.Image = Resources.icons8_image_100;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox10.Text = string.Empty;
            addprod.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(selectedImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
