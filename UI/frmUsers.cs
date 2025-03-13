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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
            this.txtUserId.Enabled = false;
        }
        userBLL bl = new userBLL();
        userDAL dl = new userDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string loggedUser = frmLogin.loggeedIn;
            bl.first_name = txtFirstName.Text;
            bl.last_name = txtLastName.Text;
            bl.email = txtEmail.Text;
            bl.username = txtUserName.Text;
            bl.password = txtPassword.Text;
            bl.contact = txtContact.Text;
            bl.address = txtAddress.Text;
            bl.gender = cmbGender.Text;
            bl.user_type = cmbUserType.Text;
            userBLL bll = dl.GetIdfromUserName(loggedUser);
            bl.added_date = DateTime.Now;
            bl.added_by = (added_by)bll.id;

            bool success = dl.Insert(bl);
            if(success== true)
            {
                MessageBox.Show("User Successfully Created");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Add New User");
            }
            DataTable dt = dl.Select();
            dgvUsers.DataSource = dt;
            
            
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dl.Select();
            dgvUsers.DataSource = dt;
            
        }
        private void clear()
        {
            txtUserId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the Index of Particular row
            int rowIndex = e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bl.id = Convert.ToInt32(txtUserId.Text);
            bl.first_name = txtFirstName.Text;
            bl.last_name = txtLastName.Text;
            bl.email = txtEmail.Text;
            bl.username = txtUserName.Text;
            bl.password = txtPassword.Text;
            bl.contact = txtContact.Text;
            bl.address = txtAddress.Text;
            bl.gender = cmbGender.Text;
            bl.user_type = cmbUserType.Text;
            bl.added_date = DateTime.Now;
            bl.added_by = added_by.User;

            bool success = dl.Update(bl);
            if(success == true)
            {
                MessageBox.Show("User Details Successfully Updated");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Update the User Details");
            }
            DataTable dt = dl.Select();
            dgvUsers.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bl.id = Convert.ToInt32(txtUserId.Text);
            bool success = dl.Delete(bl);
            if(success == true)
            {
                MessageBox.Show("User Successfully Deleted");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete the User");
            }
            DataTable dt = dl.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword != null)
            {
                DataTable dt = dl.Search(keyword);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = dl.Select();
                dgvUsers.DataSource = dt;
            }


        }
    }
}
