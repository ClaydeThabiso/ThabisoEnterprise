using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thabiso_Enterprice
{
    public partial class ClientsPage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClients();
            }
        }

        private void LoadClients(string searchQuery = "")
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT  First_Name, Last_Name,ID, Gender, Phone_Number, Postal_Address, City, Email FROM UsersInfo";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " WHERE First_Name LIKE @Search OR Last_Name LIKE @Search";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvClients.DataSource = dt;
                    gvClients.DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            LoadClients(searchText);
        }

        protected void GvClients_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteClient")
            {
                string clientId = e.CommandArgument.ToString(); // Get the ID from CommandArgument
                DeleteClient(clientId); // Call method to delete the client
            }
        }

        private void DeleteClient(string clientId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    // Start a transaction to ensure both deletions are done atomically
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            // First DELETE from Accounts table
                            string deleteAccountsQuery = "DELETE FROM Accounts WHERE Email = @Email";
                            using (SqlCommand cmd = new SqlCommand(deleteAccountsQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Email", clientId);
                                cmd.ExecuteNonQuery(); // Execute delete from Accounts
                            }

                            // Second DELETE from UsersInfo table
                            string deleteUsersQuery = "DELETE FROM UsersInfo WHERE Email = @Email";
                            using (SqlCommand cmd = new SqlCommand(deleteUsersQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Email", clientId);
                                cmd.ExecuteNonQuery(); // Execute delete from UsersInfo
                            }

                            // Commit the transaction if both deletes are successful
                            transaction.Commit();
                            LoadClients(); // Refresh the GridView after deletion
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if any error occurs
                            transaction.Rollback();
                            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Database Error: " + ex.Message + "');</script>");
            }
        }

    }
}
