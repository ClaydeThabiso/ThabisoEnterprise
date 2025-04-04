using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Thabiso_Enterprice
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userEmail"] != null)
                {
                    string userEmail = Session["userEmail"].ToString();
                    LoadUserProfile(Session["userEmail"].ToString());
                    LoadUserBalance();
                    LoadTransactions(userEmail);
                }
                else
                {
                    Response.Redirect("UserLogin.aspx"); // Redirect to login if session is null
                }
           
                    
            }
        }

        private void LoadUserProfile(string userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT First_Name, Last_Name FROM UsersInfo WHERE Email=@Email", con);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            LabelName.Text = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('User not found');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
        private void LoadUserBalance()
        {
            // Get the user's email from the session
            string userEmail = Session["UserEmail"] as string;

            if (string.IsNullOrEmpty(userEmail))
            {
                Label1.Text = "Error: User is not logged in.";
                return;
            }

            // Fetch the balance from the database
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT Balance FROM Accounts WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        decimal balance = Convert.ToDecimal(result);
                        Label1.Text = "Balance: R" + balance.ToString("N2");  // Display balance with 2 decimal places
                    }
                    else
                    {
                        Label1.Text = "Balance not found.";
                    }
                }
            }
        }

        protected void View_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccountTypes.aspx");
        }
        private void LoadTransactions(string userEmail)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT TOP 2  Date, Type, Amount FROM Transactions WHERE Email = @Email ORDER BY Date DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Check if there are any transactions
                if (dt.Rows.Count > 0)
                {
                    lblTransaction1.Text = 
                                           "Date: " + Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("yyyy-MM-dd") + "<br/>" +
                                           "Type: " + dt.Rows[0]["Type"] + "<br/>" +
                                           "Amount: R" + Convert.ToDecimal(dt.Rows[0]["Amount"]).ToString("0.00");
                }
                else
                {
                    lblTransaction1.Text = "No transactions available.";
                }

            }
        }

        protected void ViewTrans_Click(object sender, EventArgs e)
        {
            Response.Redirect("Transactions.aspx");
        }
    }
}
