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
    public partial class invoicecard : UserControl
    {
        public invoicecard()
        {
            InitializeComponent();
        }
        public string invoiceNo
        {
            get { return label19.Text; }
            set { label19.Text = value; }
        }
        public string invoiceType
        {
            get { return label21.Text; }
            set { label21.Text = value; }
        }
        public string Date
        {
            get { return label20.Text; }
            set { label20.Text = value; }
        }
        public string productID
        {
            get { return label7.Text; }
            set { label7.Text = value; }
        }
        public string productName
        {
            get { return label8.Text; }
            set { label8.Text = value; }
        }
        public string quantity
        {
            get { return label9.Text; }
            set { label9.Text = value; }
        }
        public string category
        {
            get { return label10.Text; }
            set { label10.Text = value; }
        }
        public string price
        {
            get { return label11.Text; }
            set { label11.Text = value; }
        }
        public string amount
        {
            get { return label12.Text; }
            set { label12.Text = value; }
        }
        public string Discount
        {
            get { return label14.Text; }
            set { label14.Text = value; }
        }
        public string Total
        {
            get { return label16.Text; }
            set { label16.Text = value; }
        }


        private void invoicecard_Load(object sender, EventArgs e)
        {

        }
    }
}
