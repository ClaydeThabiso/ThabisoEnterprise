<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DashBordProfile.aspx.cs" Inherits="Thabiso_Enterprice.DashBordProfile" %>
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
        <a href="#">My Profile</a>
        <a href="Transactions.aspx">Transactions</a>
         <a href="AccountTypes.aspx">Add Account</a>
        <a href="UserLogin.aspx">Logout</a>
    </div>

    <section class="container py-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card p-4 shadow">
                    <h2 class="text-center">User Profile</h2>
                        <div class="row">
                            <div class="mb-3 col-md-4">
                                <label class="form-label">First Name</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter First Name" ID="Textbox1" runat="server" required="yes" TextMode="SingleLine" ReadOnly="True"></asp:Textbox>
                            </div>
                            <div class="mb-3 col-md-4">
                                <label class="form-label">Last Name</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter Last Name" ID="Textbox2" runat="server" required="yes" TextMode="SingleLine" ReadOnly="True"></asp:Textbox>
                            </div>
                            <div class="mb-3 col-md-4">
                                <label class="form-label">ID Number</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter ID number here" ID="Textbox3" runat="server" required="yes" TextMode="Phone" ReadOnly="True"></asp:Textbox>
                            </div>
                        </div>
                        <div class="row">
                            <asp:DropdownList runat="server"  class="form-select" ID="DropdownList1" Enabled="false" >
                                <asp:ListItem Text="Select Gender" Value="" />
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                            </asp:DropdownList>
                        </div>

                        <div class="row">
                            <div class="mb-3 col-md-4">
                                <label class="form-label">Phone Number</label>
                               <asp:Textbox CssClass="form-control" placeholder="Enter Phone number here" ID="Textbox4" runat="server" required="yes" TextMode="Phone"></asp:Textbox>
                            </div>
                            <div class="mb-3 col-md-4">
                                <label class="form-label">Postal Address</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter Postal Address here" ID="Textbox5" runat="server" required="yes" TextMode="MultiLine" ReadOnly="True"></asp:Textbox>
                            </div>
                            <div class="mb-3 col-md-4">
                                <label class="form-label">City</label>
                               <asp:Textbox CssClass="form-control" placeholder="Enter City here" ID="Textbox6" runat="server" required="yes" TextMode="SingleLine" ReadOnly="True"></asp:Textbox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="mb-3 col-md-4">
                                <label class="form-label">Email</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter Email here" ID="Textbox7" runat="server" required="yes" TextMode="Email"></asp:Textbox>
                            </div>
                            <div class="mb-3 col-md-4">
                                <label class="form-label">Password</label>
                                <asp:Textbox CssClass="form-control" placeholder="Enter Password here" ID="Textbox8" runat="server" required="yes" TextMode="Password"></asp:Textbox>
                            </div>
                        </div>
                     <asp:Button class="btn btn-success btn-lg" ID="UpdateBtn" runat="server" Text="Update Profile" OnClick="UpdateBtn_Click"></asp:Button>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
