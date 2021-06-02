<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkLogMgm.aspx.cs" Inherits="Notes.WorkLogMgm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row text-right">
                    <div class="col">
                        <label for="ddlUser">操作員：</label>
                        <asp:DropDownList ID="ddlUser" runat="server" DataSourceID="SqlDataSource3" DataTextField="firstName" DataValueField="Id"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
                        <label for="ddlOrg">　|　單位：</label>
                        <asp:DropDownList ID="ddlOrg" runat="server" DataSourceID="SqlDataSource2" DataTextField="OrgName" DataValueField="Id"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [Org]"></asp:SqlDataSource>
                        <label for="ddlOrg">　|　日期：</label>
                        <asp:TextBox ID="txtSdate" runat="server" TextMode="Date">日期起</asp:TextBox>~
                        <asp:TextBox ID="txtEdate" runat="server" TextMode="Date">日期迄</asp:TextBox>
                        <asp:Button ID="btnQuery" CssClass="btn btn-info" runat="server" Text="查詢" OnClick="btnQuery_Click" />
                        <asp:Button ID="btnReset" CssClass="btn btn-warning" runat="server" Text="取消" OnClick="btnReset_Click" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="目前尚無資料" ShowHeaderWhenEmpty="True" Width="100%" OnRowCreated="gv_RowCreated" HeaderStyle-Height="30" RowStyle-Height="30" PageSize="20">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="序　號" HeaderStyle-Width="4%">
                                    <ItemTemplate>
                                        <%#Container.DisplayIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
                                <asp:BoundField DataField="UserName" HeaderText="操作員" SortExpression="UserName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrgId" HeaderText="OrgId" SortExpression="OrgId" />
                                <asp:BoundField DataField="OrgName" HeaderText="單位" SortExpression="OrgName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="名稱" SortExpression="Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Question" HeaderText="問題" SortExpression="Question">
                                    <HeaderStyle Width="25" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DealWithId" HeaderText="DealWithId" SortExpression="DealWithId" />
                                <asp:BoundField DataField="DealWithName" HeaderText="處理" SortExpression="DealWithName" HeaderStyle-Width="5%">
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DealWith" HeaderText="處理情形" SortExpression="DealWith">
                                    <HeaderStyle Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="備註" SortExpression="Remark">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateDate" HeaderText="建立日期" SortExpression="CreateDate" DataFormatString="{0:d}">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" SortExpression="UpdateDate" Visible="false" />
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
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [WorkLog]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
