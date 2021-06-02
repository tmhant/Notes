<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Notes.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <div class="jumbotron text-center" id="logon">
            <p class="text-info"><label>註冊帳號</label>
                </p>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="帳號為必填欄位" ControlToValidate="account" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <p class="text-info form-group">
                <label for="account" class="text-danger">*</label>
                帳　　號 :
            <asp:TextBox ID="account" runat="server"></asp:TextBox>
            </p>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密碼為必填欄位" ControlToValidate="password" ValidationGroup="register" ForeColor="Red"></asp:RequiredFieldValidator></div>
            <p class="text-info form-group">
                <label for="password" class="text-danger">*</label>
                密　　碼 :
            <asp:TextBox TextMode="Password" ID="password" runat="server"></asp:TextBox>
            </p>
            <div>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="密碼不符請再次確認" ControlToCompare="password" ControlToValidate="repassword" ValidationGroup="register" ForeColor="Red"></asp:CompareValidator></div>
            <p class="text-info form-group">
                <label for="repassword" class="text-danger">*</label>
                確認密碼 :
            <asp:TextBox TextMode="Password" ID="repassword" runat="server"></asp:TextBox>
            </p>
            <p class="text-info form-group">
                <label for="lastName" class="text-danger"></label>
                　　　姓 :
            <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
            </p>
            <p class="text-info form-group">
                <label for="firstName" class="text-danger"></label>
                　　　名 :
            <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
            </p>
            <asp:Button ID="btnRegister" runat="server" class="btn btn-large btn-primary" Text="送出" ValidationGroup="register" OnClick="btnRegister_Click"/>
            <asp:Button ID="btnReset" runat="server" class="btn btn-large btn-info" Text="重置" OnClick="btnReset_Click" />
        </div>
    </div>
</asp:Content>
