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
    public partial class AdimnPage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTotalClients();
                CalculateTotalDepositsAndWithdrawals();
            }
        }

        private void GetTotalClients()
        {
      
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(*) FROM UsersInfo";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    int totalClients = (int)cmd.ExecuteScalar();
                    lblTotalClients.Text = totalClients.ToString();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
        private void CalculateTotalDepositsAndWithdrawals()
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                // SQL query for total deposits (assuming 'Type' is a column for deposit/withdrawal)
                string depositQuery = "SELECT SUM(Amount) FROM Transactions WHERE Type = 'Deposit'";
                SqlCommand cmdDeposit = new SqlCommand(depositQuery, conn);
                var totalDeposits = cmdDeposit.ExecuteScalar();

                // SQL query for total withdrawals
                string withdrawalQuery = "SELECT SUM(Amount) FROM Transactions WHERE Type = 'Withdrawal'";
                SqlCommand cmdWithdrawal = new SqlCommand(withdrawalQuery, conn);
                var totalWithdrawals = cmdWithdrawal.ExecuteScalar();

                // Update the labels with total amounts
                lblTotalDeposits.Text = "R" + (totalDeposits != DBNull.Value ? Convert.ToDecimal(totalDeposits).ToString("0.00") : "0.00");
                lblTotalWithdrawals.Text = "R" + (totalWithdrawals != DBNull.Value ? Convert.ToDecimal(totalWithdrawals).ToString("0.00") : "0.00");
            }
        }
    }
}