using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;
using WindowsFormsApp1.DLL;

namespace WindowsFormsApp1.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }
        categoriesDAL cdl = new categoriesDAL();
        productBLL bl = new productBLL();
        productDAL dl = new productDAL();
        userDAL udl = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable CatagoriesDT = cdl.Select();
            cmbCategory.DataSource = CatagoriesDT;
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";
            DataTable dt = dl.Select();
            dgvProducts.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            bl.name = txtName.Text;
            bl.category = cmbCategory.Text;
            bl.description = txtDescription.Text;
            bl.rate = decimal.Parse(txtRate.Text);
            bl.qty = 0;
            bl.added_date = DateTime.Now;
            //Getting username of logged in user
            String loggedUsr = frmLogin.loggeedIn;
            userBLL usr = udl.GetIdfromUserName(loggedUsr);

            bl.added_by = usr.id;

            //Create Boolean variable to check if the product is added successfully or not
            bool success = dl.Insert(bl);
            //if the product is added successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Product Inserted Successfully
                MessageBox.Show("Product Added Successfully");
                //Calling the Clear Method
                Clear();
                //Refreshing DAta Grid View
                DataTable dt = dl.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Add New product
                MessageBox.Show("Failed to Add New Product");
            }
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtID.Text);
            bl.name = txtName.Text;
            bl.category = cmbCategory.Text;
            bl.description = txtDescription.Text;
            bl.rate = decimal.Parse(txtRate.Text);
            bl.added_date = DateTime.Now;
            //Getting Username of logged in user for added by
            String loggedUsr = frmLogin.loggeedIn;
            userBLL usr = udl.GetIdfromUserName(loggedUsr);

            bl.added_by = usr.id;

            //Create a boolean variable to check if the product is updated or not
            bool success = dl.Update(bl);
            //If the prouct is updated successfully then the value of success will be true else it will be false
            if (success == true)
            {
                //Product updated Successfully
                MessageBox.Show("Product Successfully Updated");
                Clear();
                //REfresh the Data Grid View
                DataTable dt = dl.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Update Product
                MessageBox.Show("Failed to Update Product");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtID.Text);

            //Create Bool VAriable to Check if the product is deleted or not
            bool success = dl.Delete(bl);

            //If prouct is deleted successfully then the value of success will true else it will be false
            if (success == true)
            {
                //Product Successfuly Deleted
                MessageBox.Show("Product successfully deleted.");
                Clear();
                //Refresh DAta Grid View
                DataTable dt = dl.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete Product
                MessageBox.Show("Failed to Delete Product.");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                //Search the products
                DataTable dt = dl.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Display All the products
                DataTable dt = dl.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
