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
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }
        public static string transactionType;

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedUser.Text = frmLogin.loggeedIn;
        }

        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust dashboard = new frmDeaCust();
            dashboard.Show();
            this.Hide();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Purchase";
            frmPurchaseAndSales dashboard = new frmPurchaseAndSales();
            dashboard.Show();
            this.Hide();
        }

        private void salesFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Sales";
            frmPurchaseAndSales dashboard = new frmPurchaseAndSales();
            dashboard.Show();
            this.Hide();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory dashboard = new frmInventory();
            dashboard.Show();
        }
    }
}
