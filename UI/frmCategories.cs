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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
            this.txtCategoryID.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        categoriesBLL bl = new categoriesBLL();
        categoriesDAL dl = new categoriesDAL();
        userDAL dal = new userDAL();


        private void btnADD_Click(object sender, EventArgs e)
        {
            bl.title = txtTitle.Text;
            bl.description = txtDescription.Text;
            bl.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggeedIn;
            userBLL bll = dal.GetIdfromUserName(loggedUser);
            bl.added_by = bll.id;
            bool success = dl.Insert(bl);
            if(success == true)
            {
                MessageBox.Show("New Category Inserted Successfully.");
                Clear();
                DataTable dt = dl.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Insert New Category");
            }
        }


        private void Clear()
        {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            DataTable dt = dl.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtCategoryID.Text);
            bl.title = txtTitle.Text;
            bl.description = txtDescription.Text;
            bl.added_date = DateTime.Now;
            string loggedUser = frmLogin.loggeedIn;
            userBLL bll = dal.GetIdfromUserName(loggedUser);
            bl.added_by = bll.id;
            bool success = dl.Update(bl);
            if(success==true)
            {
                MessageBox.Show("Category Updated Successfully");
                Clear();
                DataTable dt = dl.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Update the Category!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtCategoryID.Text);
            bool success = dl.Delete(bl);
            if(success== true)
            {
                MessageBox.Show("Category Deleted Successfully");
                Clear();
                DataTable dt = dl.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Delete the Category");
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if(keywords!=null)
            {
                DataTable dt = dl.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                DataTable dt = dl.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
