using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class ProductsCard : UserControl
    {
        public ProductsCard()
        {
            InitializeComponent();
            this.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            this.MouseHover += new EventHandler(ProductsCard_MouseHover);
            this.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
            pictureBox1.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            pictureBox1.MouseHover += new EventHandler(ProductsCard_MouseHover);
            pictureBox1.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
            label1.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            label1.MouseHover += new EventHandler(ProductsCard_MouseHover);
            label1.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
            label2.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            label2.MouseHover += new EventHandler(ProductsCard_MouseHover);
            label2.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
            comboBox1.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            comboBox1.MouseHover += new EventHandler(ProductsCard_MouseHover);
            comboBox1.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
            this.BackColorChanged += Form1_BackColorChanged;
            this.Click += new EventHandler(Product_Click);
            pictureBox1.Click += new EventHandler(Product_Click);
            label1.Click += new EventHandler(Product_Click);
            label2.Click += new EventHandler(Product_Click);
        }
        private void Form1_BackColorChanged(object sender, EventArgs e)
        {
            // Update ComboBox background color to match form's back color
            comboBox1.BackColor = this.BackColor;
        }
        public event EventHandler<ProductEventArgs> ProductClicked;
        private void Product_Click(object sender, EventArgs e)
        {
            if (ProductClicked != null)
            {
                ProductClicked(this, new ProductEventArgs { ProductID = this.ProductID, ProductName = this.Productname, ProductImage = this.ProductImage, ProductCategory = this.ProductCategory, ProductPrice = this.ProductPrice, ProductQuantity = this.ProductQuantity });
            }
        }
        public int ProductQuantity;
        public double ProductPrice;
        public string ProductCategory;
        public Image ProductImage
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        public string ProductID
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public string Productname
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }


        private void Products_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ProductsCard_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(232, 238, 243);
        }

        private void ProductsCard_MouseHover(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(232, 238, 243);
        }

        private void ProductsCard_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAction = comboBox1.SelectedItem.ToString();

            switch (selectedAction)
            {
                case "Delete":
                    // Call a method to delete from the database
                    DeleteFromDatabase();
                    break;
                default:
                    break;
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Delete");

            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DrawItem += new DrawItemEventHandler(comboBox1_DrawItem);


        }
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();

                string itemText = comboBox1.Items[e.Index].ToString();
                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(itemText, e.Font, brush, e.Bounds);
                }

                e.DrawFocusRectangle();
            }
        }

        private void DeleteFromDatabase()
        {
            string productId = this.ProductID;

            string connectionString = @"ConnectionString";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();                               

                    string deleteCategoryQuery = "DELETE FROM Products WHERE productID = @ProductId";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteCategoryQuery, connection))
                    {
                        cmdDelete.Parameters.AddWithValue("@ProductId", productId);
                        cmdDelete.ExecuteNonQuery();



                    }
                
                connection.Close();
            }



        }

    }
}
public class ProductEventArgs : EventArgs
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public Image ProductImage { get; set; }
    public int ProductQuantity { get; set; }
    public double ProductPrice { get; set; }
    public string ProductCategory { get; set; }
}
