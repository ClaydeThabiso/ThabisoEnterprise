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
    public partial class WithdrawalList : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                LoadAllWithdrawals();

            }
        }
        private void LoadAllWithdrawals()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    string query = "SELECT TransactionID, Email, Date,Type, Amount FROM Transactions WHERE Type='Withdrawal'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                gvTransactions.DataSource = dt;
                                gvTransactions.DataBind();
                            }
                            else
                            {
                                // Show message if no deposits exist
                                dt.Rows.Add(dt.NewRow());
                                gvTransactions.DataSource = dt;
                                gvTransactions.DataBind();
                                gvTransactions.Rows[0].Cells.Clear();
                                gvTransactions.Rows[0].Cells.Add(new TableCell { ColumnSpan = 4, Text = "No deposits found.", HorizontalAlign = HorizontalAlign.Center });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactions.PageIndex = e.NewPageIndex;
            LoadAllWithdrawals();
        }
    }
}