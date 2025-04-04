<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClientsPage.aspx.cs" Inherits="Thabiso_Enterprice.ClientsPage" %>


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
        <a href="#">Clients</a>
        <a href="WithdrawalList.aspx">Withdraw List</a>
        <a href="DepositList.aspx">Deposit List</a>
        <a href="HomePage.aspx">Logout</a>
    </div>
    <div class="content">
        <div class="container py-5 mt-1 mb-5">
            <h2>Clients List</h2>
            <div class="mb-3">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Name"></asp:TextBox>
                <asp:Button runat="server" ID="Search" class="btn btn-success btn-lg" Text="Search" OnClick="Search_Click" />
            </div>
            <asp:GridView ID="gvClients" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowCommand="GvClients_RowCommand">
                <Columns>
                    <asp:BoundField DataField="First_Name" HeaderText="First Name" SortExpression="First_Name" />
                    <asp:BoundField DataField="Last_Name" HeaderText="Last Name" SortExpression="Last_Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="Phone_Number" HeaderText="Phone Number" SortExpression="Phone_Number" />
                    <asp:BoundField DataField="Postal_Address" HeaderText="Postal Address" SortExpression="Postal_Address" />
                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger"
                                CommandName="DeleteClient" CommandArgument='<%# Eval("Email") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
