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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        loginBLL bl = new loginBLL();
        loginDAL dl = new loginDAL();
        public static String loggeedIn;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bl.username = txtUsername.Text.Trim();
            bl.password = txtPassword.Text.Trim();
            bl.user_type = cmbUserType.Text.Trim();
            bool success = dl.loginCheck(bl);
            if(success == true)
            {
                MessageBox.Show("Login Successful.");
                loggeedIn = bl.username;
                switch(bl.user_type)
                {
                    case "Admin":
                        {
                            frmAdminDashboard dashboard = new frmAdminDashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                        break;
                    case "User":
                        {
                            frmUserDashboard dashboard = new frmUserDashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                        break;

                    default:
                        {
                            MessageBox.Show("Invalid User Type.");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login Failed.Try Again");
            }


        }
    }
}
