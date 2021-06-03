<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkLog.aspx.cs" Inherits="Notes.WorkLog" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Modal -->
    <div class="modal fade" id="questionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">問題範例</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <i class="bi-x" role="img" aria-label="fill"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <input type="hidden" id="t_id" />
                            <textarea id="tbody" style="width: 100%"></textarea>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3 text-right">
                            <button type="button" id="fill" class="btn btn-info btn-sm" value="帶入" data-dismiss="modal" />
                            <i class="bi-arrow-90deg-down" role="img" aria-label="fill">帶入</i>
                        </div>
                        <div class="col-3 text-right">
                            <button type="button" id="save" class="btn btn-primary btn-sm" value="儲存" />
                            <i class="bi-check-lg" role="img" aria-label="save">儲存</i>
                        </div>
                        <div class="col-3 text-center">
                            <button type="button" id="delete" class="btn btn-warning btn-sm" value="刪除" />
                            <i class="bi-trash" role="img" aria-label="delete">刪除</i>
                        </div>
                        <div class="col-3 text-left">
                            <button type="button" id="reset" class="btn btn-secondary btn-sm" value="取消" data-dismiss="modal" />
                            <i class="bi-arrow-counterclockwise" role="img" aria-label="reset">取消</i>
                        </div>
                    </div>
                    <table id="template" class="table table-striped table-editable">
                        <thead class="thead-dark">
                            <tr>
                                <th class="text-center d-none">編號</th>
                                <th class="col"">內容</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row text-right">
                    <div class="col">
                        <asp:LinkButton ID="btnNew" CssClass="btn btn-success bi-plus-circle" runat="server" Text="新增" OnClick="btnNew_Click" />
                        <asp:LinkButton ID="btnSave" CssClass="btn btn-primary bi-check-lg" runat="server" Text="儲存" OnClick="btnSave_Click" Visible="false" />
                        <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger bi-trash" runat="server" Text="刪除" OnClientClick="confirmMsg()" Visible="false" />
                        <asp:LinkButton ID="btnReset" CssClass="btn btn-warning bi-arrow-counterclockwise" runat="server" Text="取消" OnClick="btnReset_Click" Visible="false" />
                        <asp:LinkButton ID="btnPrint" CssClass="btn btn-info bi-printer" runat="server" Text="列印" OnClick="btnPrint_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:Panel ID="PopWorkLog" runat="server" Visible="false" Width="100%">
                            <div class="container">
                                <asp:HiddenField ID="hd_id" runat="server" />
                                
                                <div class="row">
                                    <div class="col">
                                        <label for="ddlOrg">單位：</label>
                                        <asp:DropDownList ID="ddlOrg" runat="server" DataSourceID="SqlDataSource2" DataTextField="OrgName" DataValueField="Id"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [Org]"></asp:SqlDataSource>
                                        <label for="txtName">名稱：</label>
                                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <label for="txtName">問題：</label>
                                        <button type="button" id="qtemplate" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#questionModal">
                                          <span class="bi-layout-text-sidebar-reverse" role="img" aria-label="qtemplate"> 範本</span>
                                        </button>
                                    </div>
                                    <div class="col-10">
                                        <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:CheckBox ID="chkQuestion" runat="server" />儲存問題範本
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <label for="txtName">處理情形：</label>
                                        <asp:DropDownList ID="ddlDealwith" runat="server" DataSourceID="SqlDataSource3" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [ValueSet] Where Catalog = 1"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-12">
                                        <asp:TextBox ID="txtDealwith" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <label for="txtName">備註：</label>
                                        <asp:DropDownList ID="ddlRemark" runat="server" DataSourceID="SqlDataSource4" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT * FROM [ValueSet] Where Catalog = 2"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="目前尚無資料" ShowHeaderWhenEmpty="True" Width="100%" OnSelectedIndexChanging="gw_SelectedIndexChanging" OnRowCreated="gv_RowCreated" HeaderStyle-Height="30" RowStyle-Height="30">
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
                                <asp:BoundField DataField="OrgId" HeaderText="OrgId" SortExpression="OrgId" />
                                <asp:BoundField DataField="OrgName" HeaderText="單位" SortExpression="OrgName" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="名稱" SortExpression="Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
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
                                <asp:BoundField DataField="RemarkId" HeaderText="RemarkId" SortExpression="RemarkId" />
                                <asp:BoundField DataField="Remark" HeaderText="備註" SortExpression="Remark">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateDate" HeaderText="建立日期" SortExpression="CreateDate" DataFormatString="{0:d}">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" SortExpression="UpdateDate" Visible="false" />
                                <asp:CommandField ShowSelectButton="True" SelectText="編輯" HeaderStyle-Width="5%" ControlStyle-ForeColor="Red">
                                    <ControlStyle ForeColor="Red"></ControlStyle>

                                    <HeaderStyle Width="5%"></HeaderStyle>
                                </asp:CommandField>
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
    <div class="progress">
      <div runat="server" id="progressBar" class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
    </div>
    <script type="text/javascript">
        function confirmMsg() {
            if (confirm("確定要刪除嗎?")) {
                var id = $('#<%=hd_id.ClientID %>').val();
                $.ajax({
                    type: "get",
                    url: "Controller/WorkLogController.ashx",
                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                    dataType: "json",
                    data: { id: id },
                    success: function (response) {
                        console.log(response);
                        alert('刪除成功!');
                        window.location.href = "WorkLog.aspx";
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert('刪除失敗!');
                    }
                });
            }
        }
        $('#template').bootstrapTable({
            url: "Controller/PageController.ashx",
            cache: false,//是否使用快取
            method: "post",
            //toolbar: "#toolbar",
            showRefresh: false,//重新整理按鈕
            sortStable: true,//是否支援排序
            contentType: "application/x-www-form-urlencoded",//post請求必須要有，否則後臺接受不到引數
            pagination: false,//是否顯示分頁
            sidePagination: "server",//設定在服務端還是客戶端分頁
            pageNumber: 1,//首頁頁碼，預設為1
            pageSize: 10,//頁面資料條數,預設為10
            search: false,//是否有搜尋框
            clickToSelect: true,//點選是否選中行
            queryParamsType: "",
            queryParams: function (params) {
                return {
                    pageSize: params.pageSize,//pageSize
                    pageNumber: params.pageNumber,//偏移量
                    sortName: params.sortName,//以哪個欄位排序
                    sortOrder: params.sortOrder,//降序還是升序,asc,desc
                    searchText: params.searchText//搜尋關鍵字
                };
            },
            onClickRow: function (row, $element, field) {
                $element.addClass('bg-info').siblings().removeClass('bg-info');
                $('#t_id').val(row.Id);
                $('#tbody').val(row.Tbody);
            },
            formatLoadingMessage: function () {
                return "請稍後，正在載入....";
            },
            formatNoMatches: function () {
                return "暫無匹配資料.";
            },
            columns: [{
                field: 'Id',
                sortable: true,
            }, {
                field: 'Tbody',
            }]
        })

        $('#reset').click(function () {
            $('#tbody').val('');
            $('#template tr').removeClass('bg-info');
        });

        $('#save').click(function () {
            console.log($('#t_id').val());
            console.log($('#tbody').val());
            $.ajax({
                type: "get",
                url: "Controller/TemplateController.ashx",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                data: { Id: $('#t_id').val(), tbody: $('#tbody').val(), IsDeleted: false },
                success: function (response) {
                    console.log(response);
                    alert('儲存成功!');
                    $('#template').bootstrapTable('refresh');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('儲存失敗!');
                    console.log(xhr.fail);
                }
            });

        })

        $('#delete').click(function () {
            console.log($('#t_id').val());
            $.ajax({
                type: "get",
                url: "Controller/TemplateController.ashx",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                data: { Id: $('#t_id').val(), tbody: $('#tbody').val(), IsDeleted: true },
                success: function (response) {
                    console.log(response);
                    alert('刪除成功!');
                    $('#template').bootstrapTable('refresh');
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('刪除失敗!');
                    console.log(xhr.fail);
                }
            });
        });

        $('#fill').click(function () {
            $('#<%=txtQuestion.ClientID%>').text($('#tbody').val());
            $('#tbody').val('');
            $('#template tr').removeClass('bg-info');
        });

        function download(file) {
                if (window.navigator.msSaveBlob) {
                    var oPop = window.open(file, "", "width=1, height=1, top=5000, left=5000");
                    for (; oPop.document.readyState != "complete";) {
                        if (oPop.document.readyState == "complete") break;
                    }
                    oPop.document.execCommand("SaveAs");
                    oPop.close();
                }
                else {
                    var a = document.createElement("a");
                    a.href = file;
                    a.setAttribute("download", file);
                    a.click();
                }
        }
    </script>
</asp:Content>
