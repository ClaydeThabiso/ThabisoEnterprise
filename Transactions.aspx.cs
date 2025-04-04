using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thabiso_Enterprice
{
    public partial class Transactions : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userEmail = Session["UserEmail"] as string;
                if (string.IsNullOrEmpty(userEmail))
                {
                    lblMessage.Text = "You must be logged in to view your transactions.";
                    return;
                }
                LoadUserAccounts();
                LoadTransactions(userEmail);
                LoadDestinationAccounts();

            }
        }

        private void LoadUserAccounts()
        {
            string userEmail = Session["UserEmail"]?.ToString();
            if (string.IsNullOrEmpty(userEmail)) return;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT AccountNumber, AccountType FROM Accounts WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlAccountNumber.Items.Clear();
                    ddlAccountNumber.Items.Add(new ListItem("Select Account", ""));
                    while (reader.Read())
                    {
                        ddlAccountNumber.Items.Add(new ListItem($"{reader["AccountType"]} ({reader["AccountNumber"]})", reader["AccountNumber"].ToString()));
                    }
                }
            }
        }


        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedValue == "Internal Transfer")
            {
                transferSection.Visible = true;  // Show destination account dropdown
                recipientNameSection.Visible = false;
                recipientAccountSection.Visible = false;
                recipientBankSection.Visible = false;
            }
            else if (ddlTransactionType.SelectedValue == "External Transfer")
            {
                transferSection.Visible = false;  // Show destination account field
                recipientNameSection.Visible = true;
                recipientAccountSection.Visible = true;
                recipientBankSection.Visible = true;
            }
            else
            {
                transferSection.Visible = false;
                recipientNameSection.Visible = false;
                recipientAccountSection.Visible = false;
                recipientBankSection.Visible = false;
            }
        }






        private void LoadDestinationAccounts()
        {
           
            // Check if the session variable "Email" is set
            if (Session["UserEmail"] == null || string.IsNullOrEmpty(Session["UserEmail"].ToString()))
            {
                lblMessage.Text = "Error: Email session variable is not set.";
                return;
            }

            string userEmail = Session["UserEmail"].ToString();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AccountNumber FROM Accounts WHERE Email = @Email", conn);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                SqlDataReader reader = cmd.ExecuteReader();
                ddlDestinationAccount.Items.Clear();
                ddlDestinationAccount.Items.Add(new ListItem("Select Account", ""));

                while (reader.Read())
                {
                    ddlDestinationAccount.Items.Add(new ListItem(reader["AccountNumber"].ToString(), reader["AccountNumber"].ToString()));
                }

                reader.Close();
            }
        }

        protected void btnSubmitTransaction_Click(object sender, EventArgs e)
        {
            string accountNumber = ddlAccountNumber.SelectedValue;
            string transactionType = ddlTransactionType.SelectedValue;
            decimal amount;

            if (string.IsNullOrEmpty(accountNumber) || !decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                lblMessage.Text = "Invalid input. Please check your details.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    if (transactionType == "Deposit")
                    {
                        ExecuteTransaction(conn, transaction, accountNumber, amount, "Deposit");
                    }
                    else if (transactionType == "Withdrawal")
                    {
                        if (CheckSufficientFunds(conn, transaction, accountNumber, amount))
                        {
                            ExecuteTransaction(conn, transaction, accountNumber, -amount, "Withdrawal");
                        }
                        else
                        {
                            lblMessage.Text = "Insufficient funds.";
                            return;
                        }
                    }
                    else if (transactionType == "Internal Transfer")
                    {
                        string destinationAccount = ddlDestinationAccount.SelectedValue;
                        if (string.IsNullOrEmpty(destinationAccount) || destinationAccount.Trim() == accountNumber.Trim())
                        {
                            lblMessage.Text = "Invalid destination account.";
                            return;
                        }


                        if (CheckSufficientFunds(conn, transaction, accountNumber, amount))
                        {
                           
                            ExecuteTransferTransaction(conn, transaction, accountNumber, destinationAccount, amount);
                           
                        }
                        else
                        {
                            lblMessage.Text = "Insufficient funds for transfer.";
                            return;
                        }
                    }
                    else if (transactionType == "External Transfer")
                    {
                        string recipientName = txtRecipientName.Text;
                        string recipientAccount = txtRecipientAccount.Text;
                        string recipientBank = txtRecipientBank.Text;

                        if (string.IsNullOrEmpty(recipientName) || string.IsNullOrEmpty(recipientAccount) || string.IsNullOrEmpty(recipientBank))
                        {
                            lblMessage.Text = "Please enter all recipient details.";
                            return;
                        }

                        if (CheckSufficientFunds(conn, transaction, accountNumber, amount))
                        {
                            ExecuteExternalTransfer(conn, transaction, accountNumber, recipientAccount, recipientName, recipientBank, amount);
                        }
                        else
                        {
                            lblMessage.Text = "Insufficient funds for external transfer.";
                            return;
                        }
                    }

                    transaction.Commit();
                    lblMessage.Text = "Transaction successful!";
                    LoadUserAccounts();
                    Response.Redirect(Request.RawUrl,false);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessage.Text = "Transaction failed: " + ex.Message;
                }
            }
        }


        private void ExecuteTransaction(SqlConnection conn, SqlTransaction transaction, string accountNumber, decimal amount, string type)
        {
            string userEmail = Session["UserEmail"] as string;
            SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountNumber = @AccountNumber", conn, transaction);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("INSERT INTO Transactions (Email, Type, Amount, Date) VALUES (@Email, @Type, @Amount, @Date)", conn, transaction);
            cmd.Parameters.AddWithValue("@Email", userEmail);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
            cmd.ExecuteNonQuery();
        }
        private void ExecuteExternalTransfer(SqlConnection conn, SqlTransaction transaction, string senderAccount, string recipientAccount, string recipientName, string recipientBank, decimal amount)
        {
            ExecuteTransaction(conn, transaction, senderAccount, -amount, "External Transfer");
            SqlCommand cmd = new SqlCommand("INSERT INTO ExternalTransactions (SenderAccount, RecipientAccount, RecipientName, RecipientBank, Amount, Date) VALUES (@SenderAccount, @RecipientAccount, @RecipientName, @RecipientBank, @Amount, @Date)", conn, transaction);
            cmd.Parameters.AddWithValue("@SenderAccount", senderAccount);
            cmd.Parameters.AddWithValue("@RecipientAccount", recipientAccount);
            cmd.Parameters.AddWithValue("@RecipientName", recipientName);
            cmd.Parameters.AddWithValue("@RecipientBank", recipientBank);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
        private void ExecuteTransferTransaction(SqlConnection conn, SqlTransaction transaction, string senderAccount, string receiverAccount, decimal amount)
        {
            // Deduct from sender
            SqlCommand deductCmd = new SqlCommand("UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountNumber = @SenderAccount", conn, transaction);
            deductCmd.Parameters.AddWithValue("@Amount", amount);
            deductCmd.Parameters.AddWithValue("@SenderAccount", senderAccount);
            deductCmd.ExecuteNonQuery();

            // Add to receiver
            SqlCommand creditCmd = new SqlCommand("UPDATE Accounts SET Balance = Balance + @Amount WHERE AccountNumber = @ReceiverAccount", conn, transaction);
            creditCmd.Parameters.AddWithValue("@Amount", amount);
            creditCmd.Parameters.AddWithValue("@ReceiverAccount", receiverAccount);
            creditCmd.ExecuteNonQuery();

            // Record transactions for both sender and receiver
            SqlCommand insertCmd = new SqlCommand("INSERT INTO Transactions (Email, Type, Amount, Date) VALUES (@Email, @Type, @Amount, @Date)", conn, transaction);

            // Sender record
            insertCmd.Parameters.Clear();
            insertCmd.Parameters.AddWithValue("@Email", Session["UserEmail"]);
            //insertCmd.Parameters.AddWithValue("@AccountNumber", senderAccount);
            insertCmd.Parameters.AddWithValue("@Type", "Transfer Sent");
            insertCmd.Parameters.AddWithValue("@Amount", -amount);
            insertCmd.Parameters.AddWithValue("@Date", DateTime.Now);
            insertCmd.ExecuteNonQuery();

            // Receiver record
            insertCmd.Parameters.Clear();
            insertCmd.Parameters.AddWithValue("@Email", Session["UserEmail"]);
           // insertCmd.Parameters.AddWithValue("@AccountNumber", receiverAccount);
            insertCmd.Parameters.AddWithValue("@Type", "Transfer Received");
            insertCmd.Parameters.AddWithValue("@Amount", amount);
            insertCmd.Parameters.AddWithValue("@Date", DateTime.Now);
            insertCmd.ExecuteNonQuery();
        }



        private bool CheckSufficientFunds(SqlConnection conn, SqlTransaction transaction, string accountNumber, decimal amount)
        {
            string userEmail = Session["UserEmail"] as string;
            SqlCommand cmd = new SqlCommand("SELECT Balance FROM Accounts WHERE AccountNumber = @AccountNumber", conn, transaction);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

            object result = cmd.ExecuteScalar();
            if (result != null && decimal.TryParse(result.ToString(), out decimal balance))
            {
                return balance >= amount;
            }

            return false;
        }

        private void LoadAccountBalance(string accountNumber)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT Balance FROM Accounts WHERE AccountNumber = @AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
            
                    if (result != null)
                    {
                        lblBalance.Text = "R" + Convert.ToDecimal(result).ToString("N2");
                    }
                }
            }
        }

        protected void ddlAccountNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an account is selected
            if (!string.IsNullOrEmpty(ddlAccountNumber.SelectedValue))
            {
                string selectedAccount = ddlAccountNumber.SelectedValue;

                // Optional: Load balance for the selected account
                LoadAccountBalance(selectedAccount);
            }
        }
        private void LoadTransactions(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                lblMessage.Text = "Error: User email not found. Please log in again.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT TransactionID, Date, Type, Amount FROM Transactions WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvTransactions.DataSource = dt;
                        gvTransactions.DataBind();
                    }
                    else
                    {
                        gvTransactions.DataSource = null;
                        gvTransactions.DataBind();
                        lblMessage.Text = "No transactions found.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error loading transactions: " + ex.Message;
                }
            }
        }

        protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index for the GridView
            gvTransactions.PageIndex = e.NewPageIndex;

            // Load transactions again after the page index has changed
            string userEmail = Session["UserEmail"] as string;
            if (!string.IsNullOrEmpty(userEmail))
            {
                LoadTransactions(userEmail);
            }
        }

    }
}
