<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Notes.Login" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <div class="jumbotron text-center" id="logon">
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="帳號為必填欄位" ControlToValidate="account" ValidationGroup="login" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <p class="text-info">
                <label for="account" class="text-danger">*</label>
                帳號 :
                <asp:TextBox ID="account" runat="server" ValidationGroup="login"></asp:TextBox>
            </p>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密碼為必填欄位" ControlToValidate="password" ValidationGroup="login" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <p class="text-info">
                <label for="password" class="text-danger">*</label>
                密碼 :
            <asp:TextBox TextMode="Password" ID="password" runat="server" ValidationGroup="login"></asp:TextBox>
            </p>
            <asp:Button ID="btnLogin" runat="server" class="btn btn-large btn-primary" Text="登入" OnClick="btnLogin_Click" ValidationGroup="login" />
            <asp:Button ID="btnReset" runat="server" class="btn btn-large btn-info" Text="重置" OnClick="btnReset_Click" />
            <a href="/Register">註冊</a>

        </div>
    </div>
</asp:Content>
