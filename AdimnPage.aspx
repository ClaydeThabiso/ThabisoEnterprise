<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdimnPage.aspx.cs" Inherits="Thabiso_Enterprice.AdimnPage" %>

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
        <a href="#">Dashboard</a>
        <a href="ClientsPage.aspx">Clients</a>
        <a href="WithdrawalList.aspx">Withdraw List</a>
        <a href="DepositList.aspx">Deposit List</a>
        <a href="HomePage.aspx">Logout</a>
    </div>
    <div class="content">
    <div class="container py-5 mt-5">
            <div class="row mt-4">
                <div class="col-md-4">
                    <div class="card card-custom p-3 text-white bg-primary" >
                        <div class="card-header">Total Clients</div>
                        <div class="card-body">
                            <h4 class="card-title">
                                <asp:Label ID="lblTotalClients" runat="server" Text="0"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3 text-white bg-success">
                        <div class="card-header">Total Deposits</div>
                        <div class="card-body">
                            <h4 class="card-title">
                                <asp:Label ID="lblTotalDeposits" runat="server" Text="R0.00"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3 text-white bg-danger ">
                        <div class="card-header">Total Withdrawals</div>
                        <div class="card-body">
                            <h4 class="card-title">
                                <asp:Label ID="lblTotalWithdrawals" runat="server" Text="R0.00"></asp:Label></h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
