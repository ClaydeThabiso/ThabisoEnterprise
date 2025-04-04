<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AccountTypes.aspx.cs" Inherits="Thabiso_Enterprice.AccountTypes" %>
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
        <a href="Transactions.aspx">Transactions</a>
        <a href="#">Add Account</a>
        <a href="UserLogin.aspx">Logout</a>
    </div>

   <section class="container py-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card p-4 shadow">
                <h2 class="text-center mb-4">Create a New Account</h2>

                <div class="mb-3">
                    <label class="form-label">Account Type:</label>
                    <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Select Account Type" Value="" />
                        <asp:ListItem Text="Savings Account" Value="Savings" />
                        <asp:ListItem Text="Cheque Account" Value="Cheque" />
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label">Initial Deposit (Optional):</label>
                    <asp:TextBox ID="txtInitialDeposit" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="text-center">
                    <asp:Button ID="btnCreateAccount" runat="server" CssClass="btn btn-success w-100" Text="Create Account" OnClick="btnCreateAccount_Click" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Display User Accounts -->
    <h3 class="mt-5">My Accounts</h3>
    <asp:GridView ID="gvAccounts" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" AllowPaging="True" PageSize="5">
        <Columns>
            <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" />
            <asp:BoundField DataField="AccountType" HeaderText="Account Type" />
            <asp:BoundField DataField="Balance" HeaderText="Balance (R)" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
</section>


</asp:Content>
