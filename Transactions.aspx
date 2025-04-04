<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="Thabiso_Enterprice.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .sidebar {
            width: 250px;
            background: #343a40;
            color: white;
            padding: 20px;
            position: fixed;
            height: 50%;
        }

        .sidebar a {
            color: white;
            text-decoration: none;
            display: block;
            padding: 10px;
            margin: 10px 0;
            border-radius: 5px;
        }

        .sidebar a:hover {
            background: #495057;
        }

        .content {
            margin-left: 270px;
            padding: 20px;
            background: #f8f9fa;
            flex: 1;
        }

        .card-custom {
            border-left: 5px solid #28a745;
        }
    </style>

    <div class="sidebar">
        <a href="ProfilePage.aspx">Dashboard</a>
        <a href="DashBordProfile.aspx">My Profile</a>
        <a href="#">Transactions</a>
        <a href="AccountTypes.aspx">Add Account</a>
        <a href="UserLogin.aspx">Logout</a>
    </div>

    <div class="content">
        <h2>New Transaction</h2>

        <div class="mb-3">
            <label>Select Account:</label>
            <asp:DropDownList ID="ddlAccountNumber" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNumber_SelectedIndexChanged">
                <asp:ListItem Text="Select Account" Value="" />
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label>Amount:</label>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label>Transaction Type:</label>
            <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                <asp:ListItem Text="Deposit" Value="Deposit" />
                <asp:ListItem Text="Withdrawal" Value="Withdrawal" />
                <asp:ListItem Text="Internal Transfer" Value="Internal Transfer" />
                <asp:ListItem Text="External Transfer" Value="External Transfer" />
            </asp:DropDownList>
        </div>

        <!-- Only show destination account if Transfer is selected -->
        <div class="mb-3" id="transferSection" runat="server" visible="false">
            <label>Destination Account:</label>
            <asp:DropDownList ID="ddlDestinationAccount" runat="server" CssClass="form-select">
                <asp:ListItem Text="Select Destination Account" Value="" />
            </asp:DropDownList>
        </div>
        <div class="mb-3" id="recipientNameSection" runat="server" visible="false">
            <label>Recipient Name:</label>
            <asp:TextBox ID="txtRecipientName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3" id="recipientAccountSection" runat="server" visible="false">
            <label>Recipient Account Number:</label>
            <asp:TextBox ID="txtRecipientAccount" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!-- Recipient Bank Name (For External Transfers) -->
        <div class="mb-3" id="recipientBankSection" runat="server" visible="false">
            <label>Recipient Bank:</label>
            <asp:TextBox ID="txtRecipientBank" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnSubmitTransaction" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitTransaction_Click" />

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <section class="container py-5">
            <h2 class="text-center">Transaction History</h2>

            <!-- Balance Display -->
            <div class="alert alert-info text-center">
                Current Balance: <strong>
                    <asp:Label ID="lblBalance" runat="server" Text="R0.00"></asp:Label></strong>
            </div>


            <!-- Transactions Table -->
            <div class="table-responsive">
                <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvTransactions_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="TransactionID" HeaderText="ID" />
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount (R)" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>
        </section>
    </div>
</asp:Content>
