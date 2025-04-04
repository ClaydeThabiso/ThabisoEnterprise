<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WithdrawalList.aspx.cs" Inherits="Thabiso_Enterprice.WithdrawalList" %>
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
        <a href="AdimnPage.aspx">Dashboard</a>
        <a href="ClientsPage.aspx">Clients</a>
        <a href="#">Withdraw List</a>
        <a href="DepositList.aspx">Deposit List</a>
        <a href="HomePage.aspx">Logout</a>
    </div>
     <div class="content">
        <div class="table-responsive">
            <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvTransactions_PageIndexChanging">
                <columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="ID" />
                    <asp:BoundField DataField="Email" HeaderText="UserEmail" />
                    <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount (R)" DataFormatString="{0:C}" />
                </columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
