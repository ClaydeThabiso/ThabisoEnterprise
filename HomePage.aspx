<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Thabiso_Enterprice.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--The header--%>
     <header class="bg-primary text-white text-center py-5">
        <h1>Welcome to Thabiso Enterprise</h1>
        <p class="lead">Your Trusted Digital Bank - Secure. Reliable. Innovative.</p>
    </header>

    <%--The introduction/about --%>
    <section id="about" class="container py-5">
        <div class="row">
            <div class="col-md-6">
                <h2>About Us</h2>
                <p>Thabiso Enterprise is a next-generation digital bank, offering seamless online banking solutions. Whether you need a secure way to save, transfer funds, or invest, we’ve got you covered.</p>
                <a href="#features" class="btn btn-primary">Explore Features</a>
            </div>
            <div class="col-md-6">
                <img src="img/p1.jfif"" class="img-fluid rounded" alt="Banking Image">
            </div>
        </div>
    </section>
    <%--The features--%>
    <section id="features" class="bg-light py-5">
        <div class="container">
            <h2 class="text-center">Our Features</h2>
            <div class="row text-center mt-4">
                <div class="col-md-4">
                    <i class="bi bi-shield display-4 text-primary"></i>
                    <h4>Secure Transactions</h4>
                    <p>We use advanced encryption to keep your money safe.</p>
                </div>
                <div class="col-md-4">
                    <i class="bi bi-clock-history display-4 text-primary"></i>
                    <h4>24/7 Online Banking</h4>
                    <p>Manage your account anytime, anywhere.</p>
                </div>
                <div class="col-md-4">
                    <i class="bi bi-cash-stack display-4 text-primary"></i>
                    <h4>Easy Fund Transfers</h4>
                    <p>Send and receive money instantly.</p>
                </div>
            </div>
        </div>
    </section>

    <section class="text-center py-5">
        <h2>Join Thabiso Enterprise Today!</h2>
        <p>Experience secure and convenient banking like never before.</p>
        <asp:LinkButton class="btn btn-success btn-lg" ID="LinkButton2" runat="server"  href="SignUpPage.aspx">Sign Up Now</asp:LinkButton>
    </section>
</asp:Content>
