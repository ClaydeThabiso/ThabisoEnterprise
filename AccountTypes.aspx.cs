using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thabiso_Enterprice
{
    public partial class AccountTypes : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserAccounts();
            }
        }

        private void LoadUserAccounts()
        {
            string userEmail = Session["UserEmail"] as string;

            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("UserLogin.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT AccountNumber, AccountType, Balance FROM Accounts WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvAccounts.DataSource = dt;
                    gvAccounts.DataBind();
                }
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"] as string;

            if (string.IsNullOrEmpty(userEmail))
            {
                lblMessage.Text = "Session expired. Please log in again.";
                return;
            }

            string accountType = ddlAccountType.SelectedValue;
            string initialDeposit = txtInitialDeposit.Text;

            if (string.IsNullOrEmpty(accountType))
            {
                lblMessage.Text = "Please select an account type.";
                return;
            }

            decimal depositAmount = 0;
            if (!string.IsNullOrEmpty(initialDeposit) && decimal.TryParse(initialDeposit, out decimal result))
            {
                depositAmount = result;
            }

            string accountNumber = GenerateUniqueAccountNumber();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "INSERT INTO Accounts (Email, AccountNumber, AccountType, Balance) VALUES (@Email, @AccountNumber, @AccountType, @Balance)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    cmd.Parameters.AddWithValue("@AccountType", accountType);
                    cmd.Parameters.AddWithValue("@Balance", depositAmount);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Account successfully created!";
            LoadUserAccounts();
            Response.Redirect(Request.RawUrl, false);// Refresh UI
        }

        private string GenerateUniqueAccountNumber()
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                string accountNumber;
                bool exists;

                do
                {
                    accountNumber = "ACC" + new Random().Next(100000, 999999);
                    string checkQuery = "SELECT COUNT(*) FROM Accounts WHERE AccountNumber = @AccountNumber";

                    using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        exists = (int)cmd.ExecuteScalar() > 0;
                    }
                } while (exists);

                return accountNumber;
            }
        }
    }
}
