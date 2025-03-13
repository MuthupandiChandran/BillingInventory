using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;
using WindowsFormsApp1.DLL;

namespace WindowsFormsApp1.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }
        DeaCustDAL dcDL = new DeaCustDAL();
        productDAL pDL = new productDAL();
        userDAL uDL = new userDAL();
        transactionDAL tDL = new transactionDAL();
        transactionDetailDAL tdetailsDL = new transactionDetailDAL();
        DataTable transactionDT = new DataTable();
        
        private void lblBillDate_Click(object sender, EventArgs e)
        {

        }

        private void lblProductTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            string Type = frmUserDashboard.transactionType;
            lblTop.Text = Type;
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if(keyword=="")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            DeaCustBLL dc = dcDL.SearchDealerCustomerForTransaction(keyword);

            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;
            if(keyword =="")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }
            productBLL p = pDL.GetProductsForTransaction(keyword);
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString(); //Instead p.qty.ToString();
            txtRate.Text = p.rate.ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);
            decimal Total = Rate * Qty;
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;
            if (productName=="")
            {
                MessageBox.Show("Select the Product first.Try Again");
            }
            else
            {
                transactionDT.Rows.Add(productName, Rate, Qty, Total);
                dgvAddedProducts.DataSource = transactionDT;
                txtSubTotal.Text = subTotal.ToString();
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0.00";
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string value = txtDiscount.Text;
            if(value=="")
            {
                MessageBox.Show("Please Add Discount First");
            }
            else
            {
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);
                decimal grandTotal = ((100 - discount) / 100) * subTotal;
                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                //Deisplay the error message to calcuate discount
                MessageBox.Show("Calculate the discount and set the Grand Total First.");
            }
            else
            {
                //Calculate VAT
                //Getting the VAT Percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT = ((100 + vat) / 100) * previousGT;

                //Displaying new grand total with vat
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDL.GetDeaCustIDFromName(deaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text), 2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the Username of Logged in user
            string username = frmLogin.loggeedIn;
            userBLL u = uDL.GetIdfromUserName(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;

            //Lets Create a Boolean Variable and set its value to false
            bool success = false;

            //Actual Code to Insert Transaction And Transaction Details
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;
                //Create a boolean value and insert transaction 
                bool w = tDL.Insert_Transaction(transaction, out transactionID);

                //Use for loop to insert Transaction Details
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productBLL p = pDL.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()), 2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x = false;
                    if (transactionType == "Purchase")
                    {
                        //Increase the Product
                        x = pDL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if (transactionType == "Sales")
                    {
                        //Decrease the Product Quntiyt
                        x = pDL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdetailsDL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }

                if (success == true)
                {
                    //Transaction Complete
                    scope.Complete();

                    //Code to Print Bill
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n ANYSTORE PVT. LTD. \r\n\r\n";
                    printer.SubTitle = "Kathmandu, Nepal \r\n Phone: 01-045XXXX \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: " + txtDiscount.Text + "% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: " + txtGrandTotal.Text + "\r\n\r\n" + "Thank you for doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);

                    MessageBox.Show("Transaction Completed Sucessfully");
                    //Celar the Data Grid View and Clear all the TExtboxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";
                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
        }
    }
}
