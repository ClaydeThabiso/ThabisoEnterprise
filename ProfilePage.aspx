<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="Thabiso_Enterprice.ProfilePage" %>

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
        <a href="DashBordProfile.aspx">My Profile</a>
        <a href="Transactions.aspx">Transactions</a>
        <a href="AccountTypes.aspx">Add Account</a>
        <a href="UserLogin.aspx">Logout</a>
    </div>

    <div class="content">
        <div class="container py-5 mt-5">
            <h2>Welcome,
                <asp:Label ID="LabelName" runat="server"></asp:Label></h2>
            <p>Your banking dashboard at a glance.</p>

            <div class="row mt-4">
                <div class="col-md-4">
                    <div class="card card-custom p-3">
                        <h5>Account Balance</h5>
                        <asp:Label runat="server" ID="Label1" Text="R0,00"></asp:Label>
                        <br>
                        <div class="p-3 text-success-emphasis bg-success-subtle border border-success-subtle rounded-3">
                            <asp:LinkButton runat="server" ID="View" Text="View Balances" Font-Underline="false" OnClick="View_Click"></asp:LinkButton>
                        </div>
                </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3">
                        <h5>Recent Transactions</h5>
                        <asp:Label ID="lblTransaction1" runat="server" CssClass="d-block mb-2" BorderColor="ScrollBar"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card card-custom p-3">
                        <h5>View Past Transactions</h5>
                        <div class="p-3 text-success-emphasis bg-success-subtle border border-success-subtle rounded-3">
                      <asp:LinkButton runat="server" ID="ViewTrans" Text="View Transactions" Font-Underline="false" OnClick="ViewTrans_Click"></asp:LinkButton>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
