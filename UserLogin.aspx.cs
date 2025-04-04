using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Thabiso_Enterprice
{
    public partial class UserLogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM UsersInfo WHERE Email=@Email AND Password=@Password", con);
                    cmd.Parameters.AddWithValue("@Email", Textbox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", TextBook2.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Session["userEmail"] = dr["Email"].ToString(); // Store email in session
                            Response.Redirect("ProfilePage.aspx"); // Redirect to profile
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid login details');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}
