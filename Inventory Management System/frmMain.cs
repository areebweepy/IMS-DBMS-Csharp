using Inventory_Management_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class frmMain : Sample
    {
        public frmMain()
        {
            InitializeComponent();
            
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                maximize.Image = Resources.icons8_restore_down_15;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                maximize.Image = Resources.icons8_maximize_button_15;
            }
        }

        private void products_Click(object sender, EventArgs e)
        {
            productsmain1.Visible = true;
            categoriesmain1.Visible = false;
            invoice1.Visible = false;
            dashboard1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Hide the login form
            this.Hide();

            // Show the main form
            FormLogin mainForm = new FormLogin();
            mainForm.ShowDialog();

            // Close the login form after the main form is closed
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            categoriesmain1.Visible=true; 
            productsmain1.Visible=false;
            invoice1.Visible = false;
            dashboard1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            invoice1.Visible = true;
            categoriesmain1.Visible = false;
            productsmain1.Visible = false;
            dashboard1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboard1.Visible=true;
            invoice1.Visible = false;
            categoriesmain1.Visible = false;
            productsmain1.Visible = false;
        }
    }
}
