<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignUpPage.aspx.cs" Inherits="Thabiso_Enterprice.SignUpPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Sign Up Section -->
    <section class="container py-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card p-4 shadow">
                    <h2 class="text-center">Sign Up</h2>
                    <div class="row">
                        <div class="mb-3 col-md-4">
                            <label class="form-label">First Name</label>
                            <asp:TextBox ID="Textbox1" runat="server" CssClass="form-control" placeholder="Enter First Name" required="yes" TextMode="SingleLine"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-md-4">
                            <label class="form-label">Last Name</label>
                            <asp:TextBox ID="Textbox2" runat="server" CssClass="form-control" placeholder="Enter Last Name" required="yes" TextMode="SingleLine"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-md-4">
                            <label class="form-label">ID Number</label>
                            <asp:TextBox ID="Textbox3" runat="server" CssClass="form-control" placeholder="Enter ID Number" required="yes" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <asp:DropDownList runat="server" class="form-select" ID="DropdownList1">
                            <asp:ListItem Text="Select Gender" Value="" />
                            <asp:ListItem Text="Male" Value="Male" />
                            <asp:ListItem Text="Female" Value="Female" />
                        </asp:DropDownList>
                    </div>

                    <div class="row">
                        <div class="mb-3 col-md-4">
                            <label class="form-label">Phone Number</label>
                            <asp:TextBox ID="Textbox4" runat="server" CssClass="form-control" placeholder="Enter Phone Number"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-md-4">
                            <label class="form-label">Postal Address</label>
                            <asp:TextBox CssClass="form-control" placeholder="Enter Postal Address here" ID="Textbox5" runat="server" required="yes" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-md-4">
                            <label class="form-label">City</label>
                            <asp:TextBox CssClass="form-control" placeholder="Enter City here" ID="Textbox6" runat="server" required="yes" TextMode="SingleLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="mb-3 col-md-4">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="Textbox7" runat="server" CssClass="form-control" placeholder="Enter Email" TextMode="Email" required="yes"></asp:TextBox>
                        </div>
                        <div class="mb-3 col-md-4">
                            <label class="form-label">Password</label>
                            <asp:TextBox ID="Textbox8" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" required="yes"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button class="btn btn-success btn-lg" ID="Button3" runat="server"
                        Text="Register" OnClick="Button3_Click" />


                </div>
            </div>
        </div>
    </section>

</asp:Content>
