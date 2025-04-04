using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thabiso_Enterprice
{
    public partial class DashBordProfile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                LoadUserProfile();
            }
        }

        private void LoadUserProfile()
        {
            string userEmail = Session["UserEmail"] as string; 

            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("UserLogin.aspx"); // Redirect to login if not authenticated
                return;
            }

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT First_Name, Last_Name, ID, Gender, Phone_Number, Postal_Address, City, Email, Password FROM UsersInfo WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Textbox1.Text = reader["First_Name"].ToString();
                        Textbox2.Text = reader["Last_Name"].ToString();
                        Textbox3.Text = reader["ID"].ToString();
                        DropdownList1.SelectedValue = reader["Gender"].ToString();
                        Textbox4.Text = reader["Phone_Number"].ToString();
                        Textbox5.Text = reader["Postal_Address"].ToString();
                        Textbox6.Text = reader["City"].ToString();
                        Textbox7.Text = reader["Email"].ToString();
                        Textbox8.Text = reader["Password"].ToString();
                    }
                    conn.Close();
                }
            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"] as string;

            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("UserLogin.aspx");
                return;
            }

            string phone = Textbox4.Text.Trim();
            string password = Textbox8.Text.Trim();

            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('Phone number and password cannot be empty.');</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string updateQuery = "UPDATE UsersInfo SET Phone_Number = @Phone_Number, Password = @Password WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Phone_Number", phone);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Profile updated successfully!');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error updating profile. Please try again.');</script>");
                    }
                }
            }
        }
    }
}