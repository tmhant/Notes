<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Notes.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div id="treepanel" class="col-2" style="overflow-y:auto"><div id="tree"></div></div>
            <div id="chartReport" class="col-5"><canvas id="myChart"></canvas></div>
            <div id="pieReport" class="col-5"><canvas id="myPie"></canvas></div>
        </div>
    </div>
    <script type="text/javascript">
        window.onload = monthdata('');
        window.onload = yeardata;
        $(function () {
            $('#tree').treeview({
                data: getTree()//節點資料
            })
            .on('nodeSelected', function (e, node) {
                monthdata(node['text'])
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
                nodes: [{ "text": "資訊室", "selectedIcon": "bi-check-lg" }, { "text": "會計室", "selectedIcon": "bi-check-lg" }, { "text": "統計室", "selectedIcon": "bi-check-lg" }, { "text": "總務科", "selectedIcon": "bi-check-lg" }, { "text": "政風室", "selectedIcon": "bi-check-lg" }, { "text": "人事室", "selectedIcon": "bi-check-lg" }, { "text": "文書科", "selectedIcon": "bi-check-lg" }, { "text": "單一窗口", "selectedIcon": "bi-check-lg" }, { "text": "法警室", "selectedIcon": "bi-check-lg" }, { "text": "民事執行處", "selectedIcon": "bi-check-lg" }, { "text": "第一法庭", "selectedIcon": "bi-check-lg" }, { "text": "第二法庭", "selectedIcon": "bi-check-lg" }, { "text": "第三法庭", "selectedIcon": "bi-check-lg" }, { "text": "簡易法庭", "selectedIcon": "bi-check-lg" }, { "text": "大法庭", "selectedIcon": "bi-check-lg" }, { "text": "公證處", "selectedIcon": "bi-check-lg" }, { "text": "調查保護室", "selectedIcon": "bi-check-lg" }, { "text": "律師閱卷室", "selectedIcon": "bi-check-lg" }, { "text": "庭長室", "selectedIcon": "bi-check-lg" }, { "text": "刑事紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "民事紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "簡易紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "法官室", "selectedIcon": "bi-check-lg" }, { "text": "法助事", "selectedIcon": "bi-check-lg" }, { "text": "公設辨護人室", "selectedIcon": "bi-check-lg" }, { "text": "司法事務官室", "selectedIcon": "bi-check-lg" }, { "text": "分案室", "selectedIcon": "bi-check-lg" }, { "text": "掃描室", "selectedIcon": "bi-check-lg" }, { "text": "院長室", "selectedIcon": "bi-check-lg" }, { "text": "書記官長室", "selectedIcon": "bi-check-lg" }, { "text": "研考科", "selectedIcon": "bi-check-lg" }, { "text": "檔案室", "selectedIcon": "bi-check-lg" }]
            }];
            
            return tree;
        }
        //[{ "text": "資訊室", "selectedIcon": "bi-check-lg" }, { "text": "會計室", "selectedIcon": "bi-check-lg" }, { "text": "統計室", "selectedIcon": "bi-check-lg" }, { "text": "總務科", "selectedIcon": "bi-check-lg" }, { "text": "政風室", "selectedIcon": "bi-check-lg" }, { "text": "人事室", "selectedIcon": "bi-check-lg" }, { "text": "文書科", "selectedIcon": "bi-check-lg" }, { "text": "單一窗口", "selectedIcon": "bi-check-lg" }, { "text": "法警室", "selectedIcon": "bi-check-lg" }, { "text": "民事執行處", "selectedIcon": "bi-check-lg" }, { "text": "第一法庭", "selectedIcon": "bi-check-lg" }, { "text": "第二法庭", "selectedIcon": "bi-check-lg" }, { "text": "第三法庭", "selectedIcon": "bi-check-lg" }, { "text": "簡易法庭", "selectedIcon": "bi-check-lg" }, { "text": "大法庭", "selectedIcon": "bi-check-lg" }, { "text": "公證處", "selectedIcon": "bi-check-lg" }, { "text": "調查保護室", "selectedIcon": "bi-check-lg" }, { "text": "律師閱卷室", "selectedIcon": "bi-check-lg" }, { "text": "庭長室", "selectedIcon": "bi-check-lg" }, { "text": "刑事紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "民事紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "簡易紀錄科", "selectedIcon": "bi-check-lg" }, { "text": "法官室", "selectedIcon": "bi-check-lg" }, { "text": "法助事", "selectedIcon": "bi-check-lg" }, { "text": "公設辨護人室", "selectedIcon": "bi-check-lg" }, { "text": "司法事務官室", "selectedIcon": "bi-check-lg" }, { "text": "分案室", "selectedIcon": "bi-check-lg" }, { "text": "掃描室", "selectedIcon": "bi-check-lg" }, { "text": "院長室", "selectedIcon": "bi-check-lg" }, { "text": "書記官長室", "selectedIcon": "bi-check-lg" }, { "text": "研考科", "selectedIcon": "bi-check-lg" }, { "text": "檔案室", "selectedIcon": "bi-check-lg" }]
        async function getData() {
            var res = await fetch('Controller/OrgController.ashx', { method: 'get' });
            var jsondata = await res.json();
            
            return await jsondata;
            //$.ajax({
            //    type: "get",
            //    url: "Controller/OrgController.ashx",
            //    contentType: "application/x-www-form-urlencoded; charset=utf-8",
            //    dataType: "text",
            //    async: false,
            //    success: function (response) {
            //        return response;
            //    },
            //    error: function (xhr, ajaxOptions, thrownError) {
            //        console.log(xhr.fail);
            //    }
            //});
        }
        function render(data) {
            document.querySelector("#chartReport").innerHTML = '<canvas id="myChart"></canvas>';
            
            var ctx = document.getElementById("myChart").getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
                    datasets: [{
                        label: '件數',
                        data: data,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)',
                            'rgba(15, 99, 132, 0.8)',
                            'rgba(54, 162, 235, 0.8)',
                            'rgba(255, 206, 86, 0.8)',
                            'rgba(75, 192, 192, 0.8)',
                            'rgba(153, 102, 255, 0.8)',
                            'rgba(255, 159, 64, 0.8)'
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
        }
        function renderpie(orgs, counts) {
            document.querySelector("#pieReport").innerHTML = '<canvas id="myPie"></canvas>';
            var ctx = document.getElementById("myPie").getContext('2d');
            var myPie = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: orgs,
                    datasets: [{
                        label: '件數',
                        data: counts,
                        backgroundColor: [
                            'blue',
                            'yellow',
                            'green',
                            'red',
                            'orange',
                            'gray',
                            'brown',
                            'purple',
                            'pink',
                            'azure',
                            'cyan',
                            'periwinkle',
                            'violet',
                            'aliceblue',
                            'gold',
                            'beige',
                            'turquoise',
                            'lime',
                            'maroon',
                            'salmon',
                            'navy',
                            'teal',
                            'springgreen',
                            'orchid',
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(175, 255, 192, 0.8)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 0, 64, 0.2)',
                            'rgba(80, 199, 132, 0.2)',
                            'rgba(222, 16, 235, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,255,255,1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    plugins: {
                        legend: {
                            labels: {
                                usePointStyle: true,
                            },
                        }
                    }
                },
            });
        }

        function monthdata(org) {
            $('#treepanel').css("height", screen.height-250);
            //var res = await fetch('Controller/CounterController.ashx', { method: 'get'});
            //var jsondata = await res.json();
            //console.log(jsondata);
            //render(jsondata)
            $.ajax({
                type: "get",
                url: "Controller/CounterController.ashx",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                data: { orgName: org },
                success: function (response) {
                    console.log(response);
                    render(response);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.fail);
                }
            });
        }

        async function yeardata() {
            var res = await fetch('Controller/OrgCounterController.ashx', { method: 'get'});
            var jsondata = await res.json();
            var orgs=[], counts=[];
            for (var k in jsondata) {
                orgs.push(jsondata[k].OrgName);
                counts.push(jsondata[k].count);
            }
            renderpie(orgs, counts)
        }
    </script>
</asp:Content>
