using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;

namespace WindowsFormsApp1.DLL
{
    class userDAL
    {

        static String myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Select data from database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM [Trainee].[dbo].[tbl_users]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Insert Data to database
        public bool Insert(userBLL data)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string query = "INSERT INTO tbl_users(first_name,last_name,email,username,password,contact,address,gender,user_type,added_date,added_by) VALUES(@first_name,@last_name,@email,@username,@password,@contact,@address,@gender,@user_type,@added_date,@added_by)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", data.first_name);
                cmd.Parameters.AddWithValue("@last_name", data.last_name);
                cmd.Parameters.AddWithValue("@email", data.email);
                cmd.Parameters.AddWithValue("@username", data.username);
                cmd.Parameters.AddWithValue("@password", data.password);
                cmd.Parameters.AddWithValue("@contact", data.contact);
                cmd.Parameters.AddWithValue("@address", data.address);
                cmd.Parameters.AddWithValue("@gender", data.gender);
                cmd.Parameters.AddWithValue("@user_Type", data.user_type);
                cmd.Parameters.AddWithValue("@added_date",data.added_date);
                cmd.Parameters.AddWithValue("@added_by", (int)data.added_by);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }




        #endregion
        #region Update Data in Database
        public bool Update(userBLL data)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string query = "UPDATE tbl_users SET first_name =@first_name,last_name=@last_name,email=@email,username=@username,password=@password,contact=@contact,address=@address,gender=@gender,user_type=@user_type,added_date=@added_date,added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", data.first_name);
                cmd.Parameters.AddWithValue("@last_name", data.last_name);
                cmd.Parameters.AddWithValue("@email", data.email);
                cmd.Parameters.AddWithValue("@username", data.username);
                cmd.Parameters.AddWithValue("@password", data.password);
                cmd.Parameters.AddWithValue("@contact", data.contact);
                cmd.Parameters.AddWithValue("@address", data.address);
                cmd.Parameters.AddWithValue("@gender", data.gender);
                cmd.Parameters.AddWithValue("@user_Type", data.user_type);
                cmd.Parameters.AddWithValue("@added_date", data.added_date);
                cmd.Parameters.AddWithValue("@added_by", (int)data.added_by);
                cmd.Parameters.AddWithValue("@id", data.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }


        #endregion
        #region Delete data from Database
        public bool Delete(userBLL data)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string query = "DELETE tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", data.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }



        #endregion

        #region Search users in Database

        public DataTable Search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM [Trainee].[dbo].[tbl_users] WHERE id LIKE '%" + keyword + "%' OR first_name LIKE '%" + keyword + "%' OR last_name LIKE '%" + keyword + "%' OR username LIKE '%" + keyword + "%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        #endregion
        #region Getting User ID from username
        public userBLL GetIdfromUserName(string username)
        {
            userBLL bl = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT id FROM tbl_users WHERE username='"+username+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                conn.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    bl.id = int.Parse(dt.Rows[0]["id"].ToString());
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return bl;
        }



        #endregion
    }
}
