<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Notes.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-2"><div id="tree"></div></div>
            <div class="col-10"><canvas id="myChart" style="max-width: 500px;"></canvas></div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#tree').treeview({
                data: getTree()//節點資料
            });
        })

        function getTree() {
            //節點上的資料遵循如下的格式：
            var tree = [{
                text: "全部", //節點顯示的文字值  string
                icon: "bi-dash", //節點上顯示的圖示，支援bootstrap的圖示  string
                selectedIcon: "bi-check-lg", //節點被選中時顯示的圖示       string
                color: "#FFFFFF", //節點的前景色      string
                backColor: "#CCCCCC", //節點的背景色      string
                href: "", //節點上的超連結
                selectable: true, //標記節點是否可以選擇。false表示節點應該作為擴充套件標題，不會觸發選擇事件。  string
                state: { //描述節點的初始狀態    Object
                    checked: true, //是否選中節點
                    /*disabled: true,*/ //是否禁用節點
                    expanded: true, //是否展開節點
                    selected: true //是否選中節點
                },
                tags: ['標籤資訊1', '標籤資訊2'], //向節點的右側新增附加資訊（類似與boostrap的徽章）    Array of Strings
                nodes: [{
                    text: "資訊室",
                    selectedIcon: "bi-check-lg"
                }, {
                    text: "會計室",
                    selectedIcon: "bi-check-lg"
                }]
            }];

            return tree;
        }

        var ctx = document.getElementById("myChart").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
                datasets: [{
                    label: '件數',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    </script>
</asp:Content>
