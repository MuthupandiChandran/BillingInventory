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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        DeaCustBLL bl = new DeaCustBLL();
        DeaCustDAL dl = new DeaCustDAL();
        userDAL user = new userDAL();


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bl.type = cmbDeaCust.Text;
            bl.name = txtName.Text;
            bl.email = txtEmail.Text;
            bl.contact = txtContact.Text;
            bl.address = txtAddress.Text;
            bl.added_date = DateTime.Now;
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            string loggedUsr = frmLogin.loggeedIn;
            userBLL usr = user.GetIdfromUserName(loggedUsr);
            bl.added_by = usr.id;

            //Creating boolean variable to check whether the dealer or cutomer is added or not
            bool success = dl.Insert(bl);

            if (success == true)
            {
                //Dealer or Cutomer inserted successfully 
                MessageBox.Show("Dealer or Customer Added Successfully");
                Clear();
                //Refresh Data Grid View
                DataTable dt = dl.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Insert Dealer or Customer");
            }
        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            DataTable dt = dl.Select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtDeaCustID.Text);
            bl.type = cmbDeaCust.Text;
            bl.name = txtName.Text;
            bl.email = txtEmail.Text;
            bl.contact = txtContact.Text;
            bl.address = txtAddress.Text;
            bl.added_date = DateTime.Now;
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            string loggedUsr = frmLogin.loggeedIn;
            userBLL usr = user.GetIdfromUserName(loggedUsr);
            bl.added_by = usr.id;

            //create boolean variable to check whether the dealer or customer is updated or not
            bool success = dl.Update(bl);

            if (success == true)
            {
                //Dealer and Customer update Successfully
                MessageBox.Show("Dealer or Customer updated Successfully");
                Clear();
                //Refresh the Data Grid View
                DataTable dt = dl.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to udate Dealer or Customer
                MessageBox.Show("Failed to Udpate Dealer or Customer");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bl.id = int.Parse(txtDeaCustID.Text);

            //Create boolean variable to check wheteher the dealer or customer is deleted or not
            bool success = dl.Delete(bl);

            if (success == true)
            {
                //Dealer or Customer Deleted Successfully
                MessageBox.Show("Dealer or Customer Deleted Successfully");
                Clear();
                //Refresh the Data Grid View
                DataTable dt = dl.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Dealer or Customer Failed to Delete
                MessageBox.Show("Failed to Delete Dealer or Customer");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                //Search the Dealer or Customer
                DataTable dt = dl.Search(keyword);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Show all the Dealer or Customer
                DataTable dt = dl.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
