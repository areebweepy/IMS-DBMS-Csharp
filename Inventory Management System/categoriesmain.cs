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
using static Inventory_Management_System.productsmain;

namespace Inventory_Management_System
{
    public partial class categoriesmain : UserControl
    {
        public categoriesmain()
        {
            InitializeComponent();
            LoadCategories();
        }
        public class CategoryModel
        {
            public string catId { get; set; }
            public string catName { get; set; }
        }

        public List<CategoryModel> GetCategoriesFromDatabase()
        {
            // Implement your database fetching logic here
            // This is a placeholder method that should return a list of product models
            List<CategoryModel> categories = new List<CategoryModel>();

            string connectionString = @"ConnectionString";
            string query = "SELECT categoryID, category FROM Categories";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CategoryModel category = new CategoryModel
                        {
                            catId = reader.GetString(0),
                            catName = reader.GetString(1),
                            
                        };

                        categories.Add(category);
                    }
                }
                connection.Close();
            }

            // Add actual database fetching logic here
            return categories;
        }

        private void LoadCategories()
        {
            flowLayoutPanel1.Controls.Clear();
            List<CategoryModel> categories = GetCategoriesFromDatabase();
            label2.Text = $"Categories ({categories.Count})";

            foreach (var category in categories)
            {
                categories categoryControl = new categories();
                categoryControl.CategoryID = category.catId;
                categoryControl.Categoryname = category.catName;
                categoryControl.CategoryClicked += new EventHandler<CategoryEventArgs>(CategoryControl_CategoryClicked);
                flowLayoutPanel1.Controls.Add(categoryControl);
            }
        }
        public class ProductModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public string Category { get; set; }
            public string ImagePath { get; set; }
        }

        public List<ProductModel> GetProductsByCategory(string categoryId)
        {
            List<ProductModel> products = new List<ProductModel>();
            string connectionString = @"ConnectionString";
            string query = @"SELECT p.productID, p.productName, p.quantity, p.price, c.category, p.imagePath
                           FROM Products p
                           INNER JOIN Categories c ON p.categoryId = c.categoryId
                           WHERE c.categoryId = @CategoryId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
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
            return products;
        }
        private void LoadProducts(string categoid)
        {
            // Clear previous cards in the second flowLayoutPanel
            flowLayoutPanel2.Controls.Clear();

            List<ProductModel> products = GetProductsByCategory(categoid);
            

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

                flowLayoutPanel2.Controls.Add(productControl);

                
            }
        }

        private void AddCategory(string addcatid, string addcatname)
        {
            string connectionString = @"ConnectionString";
            string query = @"INSERT INTO CATEGORIES VALUES(@catID,@catName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@catID", addcatid);
                command.Parameters.AddWithValue("@catName", addcatname);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            LoadCategories();
        }
        private void ProductControl_ProductClicked(object sender, ProductEventArgs e)
        {
            // Make the prodinfo panel visible
            panel2.Visible = true;

            // Populate the prodinfo panel with product details
            textBox2.Text = e.ProductID;
            textBox1.Text = e.ProductName;
            textBox6.Text = e.ProductQuantity.ToString();
            textBox5.Text = e.ProductPrice.ToString();
            textBox9.Text = e.ProductCategory;
            pictureBox2.Image = e.ProductImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            // Assuming you have other controls for quantity and price, set them as well
            // e.g., txtQuantity.Text = e.Quantity.ToString();
            // e.g., txtPrice.Text = e.Price.ToString();
        }
        private void CategoryControl_CategoryClicked(object sender, CategoryEventArgs e)
        {
            
            string categid =e.CategoryID;
            LoadProducts(categid);
           
        }

        private void categories1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
           prodinfo.Visible = false;
           textBox7.Text = string.Empty;
           textBox8.Text = string.Empty;
        }

        private void newcatsub_Click(object sender, EventArgs e)
        {
            string addcatid = textBox8.Text;
            string addcatname = textBox7.Text;
            AddCategory(addcatid, addcatname);
            prodinfo.Visible = false;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
        }

        private void newcategory_Click(object sender, EventArgs e)
        {
            prodinfo.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadCategories();
            flowLayoutPanel2.Controls.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
