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
    public partial class categories : UserControl
    {
        public event EventHandler<CategoryEventArgs> CategoryClicked;


        public categories()
        {
            InitializeComponent();



            this.MouseEnter += new EventHandler(ProductsCard_MouseEnter);
            this.MouseHover += new EventHandler(ProductsCard_MouseHover);
            this.MouseLeave += new EventHandler(ProductsCard_MouseLeave);
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
            this.Click += new EventHandler(Category_Click);
            label1.Click += new EventHandler(Category_Click);
            label2.Click += new EventHandler(Category_Click);


        }

        private void Form1_BackColorChanged(object sender, EventArgs e)
        {
            // Update ComboBox background color to match form's back color
            comboBox1.BackColor = this.BackColor;
        }
        private void Category_Click(object sender, EventArgs e)
        {
            if (CategoryClicked != null)
            {
                CategoryClicked(this, new CategoryEventArgs { CategoryID = this.CategoryID, Categoryname = this.Categoryname });
            }
        }

        public string CategoryID
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public string Categoryname
        {
            get { return label2.Text; }
            set { label2.Text = value; }
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
        private void categories_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
            string categoryId = this.CategoryID;

            string connectionString = @"Server=ACER-NITRO-5;Database=inventory;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Step 1: Update Products with the given categoryId to set CategoryId to NULL
                string updateProductsQuery = "UPDATE Products SET CategoryId = NULL WHERE CategoryId = @CategoryId";
                using (SqlCommand cmdUpdate = new SqlCommand(updateProductsQuery, connection))
                {
                    cmdUpdate.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmdUpdate.ExecuteNonQuery();

                    // Step 2: Delete the category after updating the related products

                    string deleteCategoryQuery = "DELETE FROM Categories WHERE CategoryId = @CategoryId";
                    using (SqlCommand cmdDelete = new SqlCommand(deleteCategoryQuery, connection))
                    {
                        cmdDelete.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmdDelete.ExecuteNonQuery();



                    }
                }
                connection.Close();
            }



        }
    }
}
    public class CategoryEventArgs : EventArgs
    {
        public string CategoryID { get; set; }
        public string Categoryname { get; set; }
    }

