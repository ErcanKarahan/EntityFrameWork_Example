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

namespace WFA_EntityFrameWork_Odev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities _db = new NorthwindEntities();
        Product modimodifiedProduct;
        void ProductList()
        {
            lstProducts.DataSource = _db.Products.ToList();
            lstProducts.DisplayMember = "ProductName";
            lstProducts.SelectedIndex = -1;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        void Clear()
        {
            txtPrice.Text = null;
            txtName.Text = null;
            cmbCategory.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product p=new Product();
            p.Category = cmbCategory.SelectedItem as Category;
            p.ProductName = txtName.Text;
            p.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            _db.Products.Add(p);
            _db.SaveChanges();
            Clear();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = _db.Categories.ToList();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.SelectedIndex = -1;
        }

        private void lstProducts_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex > -1) 
            {
                modimodifiedProduct=lstProducts.SelectedItem as Product;
                txtName.Text = (lstProducts.SelectedItem as Product).ProductName;
                txtPrice.Text = (lstProducts.SelectedItem as Product).UnitPrice.ToString();
                cmbCategory.SelectedItem = (lstProducts.SelectedItem as Product).Category;

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (modimodifiedProduct is null)
            {
                MessageBox.Show("Plaese select a Item to Update");
                return;
            }

            modimodifiedProduct.ProductName = txtName.Text;
            modimodifiedProduct.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            modimodifiedProduct.Category = cmbCategory.SelectedItem as Category;
            modimodifiedProduct = null;
            _db.SaveChanges();
            ProductList();
            Clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (modimodifiedProduct is null)
            {
                MessageBox.Show("Please select Deleted Itenm");    
                return;
            }
            _db.Products.Remove(modimodifiedProduct);
            modimodifiedProduct = null;
            _db.SaveChanges();
            ProductList();
            Clear();
        }
    }

    
}
