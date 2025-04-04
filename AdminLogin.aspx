<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="Thabiso_Enterprice.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="container py-5">
        <div class="row justify-content-center">
            <div class="col-md-4">
                <div class="card p-4 shadow">
                    <h2 class="text-center">Admin Login</h2>
                  
                        <div class="mb-3">
                            <label class="form-label">Email</label>
                            <asp:Textbox CssClass="form-control" placeholder="Enter Email here" ID="Ema" runat="server" required="yes" TextMode="Email"></asp:Textbox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Password</label>
                            <asp:Textbox CssClass="form-control" placeholder="Enter Password here" ID="Pass" runat="server" required="yes" TextMode="Password"></asp:Textbox>
                        </div>
                        <asp:Button  runat="server" ID="button1" class="btn btn-primary w-100"  Text="Login" OnClick="button1_Click"></asp:Button>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
