<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMgm.aspx.cs" Inherits="Notes.UserMgm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row text-right">
                    <div class="col">
                        <asp:LinkButton ID="btnNew" CssClass="btn btn-success bi-plus-circle" runat="server" Text="新增" OnClick="btnNew_Click" Visible="false" />
                        <asp:LinkButton ID="btnSave" CssClass="btn btn-primary bi-check-lg" runat="server" Text="儲存" OnClick="btnSave_Click" Visible="false" />
                        <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger bi-trash" runat="server" Text="刪除" OnClientClick="confirmMsg()" Visible="false" />
                        <asp:LinkButton ID="btnReset" CssClass="btn btn-warning bi-arrow-counterclockwise" runat="server" Text="取消" OnClick="btnReset_Click" Visible="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:Panel ID="Pop" runat="server" Visible="false" Width="100%">
                            <div class="container">
                                <asp:HiddenField ID="hd_id" runat="server" />
                                <div class="row">
                                    <div class="col">
                                        <label for="username">帳號：</label>
                                        <asp:TextBox ID="username" runat="server"></asp:TextBox>
                                        <label for="txtName">密碼：</label>
                                        <asp:TextBox ID="password" runat="server"></asp:TextBox>
                                        <label for="lastName">姓：</label>
                                        <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
                                        <label for="firstName">名：</label>
                                        <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="id,username" DataSourceID="SqlDataSource1" Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Height="30" RowStyle-Height="30" OnSelectedIndexChanging="gv_SelectedIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="username" HeaderText="帳號" ReadOnly="True" SortExpression="username" />
                                <asp:BoundField DataField="password" HeaderText="密碼" SortExpression="password" />
                                <asp:BoundField DataField="lastName" HeaderText="姓" SortExpression="lastName" />
                                <asp:BoundField DataField="firstName" HeaderText="名" SortExpression="firstName" />
                                <asp:CommandField ShowSelectButton="True" SelectText="編輯" HeaderStyle-Width="5%" ControlStyle-ForeColor="Red" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [Users] Where IsDeleted = 0"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function confirmMsg() {
            if (confirm("確定要刪除嗎?")) {
                var id = $('#<%=hd_id.ClientID %>').val();
                $.ajax({
                    type: "get",
                    url: "Controller/UserController.ashx",
                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                    dataType: "json",
                    data: { id: id },
                    success: function (response) {
                        console.log(response);
                        alert('刪除成功!');
                        window.location.href = "UserMgm.aspx";
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert('刪除失敗!');
                    }
                });
            }
        }
    </script>
</asp:Content>
