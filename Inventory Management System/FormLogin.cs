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
    public partial class FormLogin : Sample
    {
        public FormLogin()
        {
            InitializeComponent();
            txtuser.KeyDown += new KeyEventHandler(txtpass_KeyDown);
            txtuser.KeyPress += new KeyPressEventHandler(txtpass_KeyPress);
            txtpass.KeyDown += new KeyEventHandler(txtpass_KeyDown);
            txtpass.KeyPress += new KeyPressEventHandler(txtpass_KeyPress);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lgnbtn_Click(object sender, EventArgs e)
        {
            string username = txtuser.Text;
            string password = txtpass.Text;

            if (ValidateLogin(username, password))
            {
                // Hide the login form
                this.Hide();

                // Show the main form
                frmMain mainForm = new frmMain();
                mainForm.ShowDialog();

                // Close the login form after the main form is closed
                this.Close();
            }
            else
            {

                invalidcreds.Visible = true;
                pictureBox3.Visible = true;
            }
            
        }
        private bool ValidateLogin(string username, string password)
        {
            string connectionString = @"Server=ACER-NITRO-5;Database=inventory;Integrated Security=True;";
            string query = "SELECT Password FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                try
                {
                    
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string storedPassword = result.ToString();
                        return password.Equals(storedPassword);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                connection.Close();
            }
                

            return false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtpass.PasswordChar = checkBox1.Checked ? '\0' : '*';
                 
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Suppress the beep sound
                e.SuppressKeyPress = true;
                // Trigger the login button click event
                lgnbtn.PerformClick();
            }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Suppress the key press in KeyPress event
                e.Handled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
