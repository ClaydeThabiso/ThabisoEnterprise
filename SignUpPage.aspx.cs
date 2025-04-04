using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thabiso_Enterprice
{
    public partial class SignUpPage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropdown();
            }
        }

        private void PopulateDropdown()
        {
            DropdownList1.Items.Clear();
            DropdownList1.Items.Add(new ListItem("Select Gender", "")); 
            DropdownList1.Items.Add(new ListItem("Male", "Male"));
            DropdownList1.Items.Add(new ListItem("Female", "Female"));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            Response.Write("<script>alert('Button clicked');</script>");

            if (string.IsNullOrEmpty(DropdownList1.SelectedValue) || DropdownList1.SelectedValue == "")
            {
                Response.Write("<script>alert('Please select a gender');</script>");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                   


                    using (SqlCommand cmd = new SqlCommand("INSERT INTO UsersInfo ([First_Name], [Last_Name], [ID], [Gender], " +
                        "[Phone_Number], [Postal_Address], [City], [Email], [Password]) VALUES " +
                        "(@First_Name, @Last_Name, @ID, @Gender, @Phone_Number, @Postal_Address, @City, @Email, @Password)", con))
                    {
                        cmd.Parameters.AddWithValue("@First_Name", Textbox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Last_Name", Textbox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", Textbox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone_Number", Textbox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@Gender", DropdownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@Postal_Address", Textbox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", Textbox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", Textbox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", Textbox8.Text.Trim());
                       

                        cmd.ExecuteNonQuery();

                        Response.Write("<script>alert('Sign Up successful!');</script>");
                        Response.Redirect("ProfilePage.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }


    }
}