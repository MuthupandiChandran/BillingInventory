using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.UI;

namespace WindowsFormsApp1
{
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
            InitializeComponent();
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedUser.Text = frmLogin.loggeedIn;
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers dashboard = new frmUsers();
            dashboard.Show();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategories dashboard = new frmCategories();
            dashboard.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts dashboard = new frmProducts();
            dashboard.Show();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust dashboard = new frmDeaCust();
            dashboard.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransactions dashboard = new frmTransactions();
            dashboard.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory dashboard = new frmInventory();
            dashboard.Show();
        }
    }
}
