<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="Notes.Template" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-table.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-table.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col">
                <input type="hidden" id="t_id" />
            <textarea id="tbody" style="width:100%"></textarea>
                </div>
        </div>
        <div class="row">
            <div class="col-4 text-right">
                <input type="button" id="save" class="btn btn-primary" value="儲存" />
            </div>
            <div class="col-4 text-center">
                <input type="button" id="delete" class="btn btn-warning" value="刪除" />
            </div>
            <div class="col-4 text-left">
                <input type="button" id="reset" class="btn btn-secondary" value="取消" />
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
</body>
</html>
<script type="text/javascript">
        $('#template').bootstrapTable({
            url: "Controller/PageController.ashx",
            cache: false,//是否使用快取
            method: "post",
            //toolbar: "#toolbar",
            showRefresh: true,//重新整理按鈕
            sortStable: true,//是否支援排序
            contentType: "application/x-www-form-urlencoded",//post請求必須要有，否則後臺接受不到引數
            pagination: false,//是否顯示分頁
            sidePagination: "server",//設定在服務端還是客戶端分頁
            pageNumber: 1,//首頁頁碼，預設為1
            pageSize: 10,//頁面資料條數,預設為10
            search: true,//是否有搜尋框
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
            data: { Id: $('#t_id').val(), tbody: $('#tbody').val(), IsDeleted: false},
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
</script>