$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});

var data = [{
    'order-id': 1,
    'userid': 9,
    'total-money': 2333,
    'consignee': '阿炜',
    'address': '广东省广州市广州中医药大学',
    'phone': '13531262130',
    'time': '2020-06-07',
    'status': '未处理',
}, {
    'order-id': 2,
    'userid': 9,
    'total-money': 2333,
    'consignee': '阿炜',
    'address': '广东省广州市广州中医药大学',
    'phone': '13531262130',
    'time': '2020-06-07',
    'status': '未处理',
}]

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_orders').bootstrapTable({
            url: '/Home/adminordersearch', //请求后台的URL（*）
            method: 'get', //请求方式（*）
            toolbar: '#toolbar', //工具按钮用哪个容器
            striped: true, //是否显示行间隔色
            cache: false, //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true, //是否显示分页（*）
            sortable: false, //是否启用排序
            sortOrder: "asc", //排序方式
            queryParams: oTableInit.queryParams, //传递参数（*）
            sidePagination: "server", //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1, //初始化加载第一页，默认第一页
            pageSize: 10, //每页的记录行数（*）
            pageList: [10, 25, 50, 100], //可供选择的每页的行数（*）
            search: true, //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true, //是否显示所有的列
            showRefresh: true, //是否显示刷新按钮
            minimumCountColumns: 2, //最少允许的列数
            clickToSelect: true, //是否启用点击选中行
            height: 500, //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ID", //每一行的唯一标识，一般为主键列
            showToggle: true, //是否显示详细视图和列表视图的切换按钮
            cardView: false, //是否显示详细视图
            detailView: false, //是否显示父子表
            columns: [{
                field: 'orderid',
                title: '订单编号'
            }, {
                field: 'userid',
                title: '用户编号',
            }, {
                field: 'totalprice',
                title: '金额'
            }, {
                field: 'name',
                title: '收货人'
            }, {
                field: 'address',
                title: '收获地址',
            }, {
                field: 'phone',
                title: '联系电话'
            }, {
                field: 'time',
                title: '时间',
                formatter: function (value, row, index) {
                    var timestamp = parseInt(value.substr(6, value.length - 8));
                    return '<p>' + timestampToTime(timestamp) + '</p>';
                }
            }, {
                field: 'status',
                title: '状态'
            },{
                field: 'operator',
                title: '操作',
                events: operateEvents, //给按钮注册事件
				formatter: addFunctionAlty //表格中增加按钮 
            },],
            //data: data
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit, //页面大小
            offset: params.offset, //页码
            orderid: $("#txt_search_orderid").val(),
            status: $("#txt_search_status").val()
        };
        return temp;
    };
    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    oInit.Init = function () {
        $("#btn_query").click(function () {
            $("#tb_orders").bootstrapTable('refresh');
        });
    };

    return oInit;
};

function addFunctionAlty(value, row, index) {
    return [
        '<button id="edit" type="button" class="btn btn-default">发货</button>',
        '<button id="more" type="button" class="btn btn-default">详情</button>'
    ].join('');
};

window.operateEvents = {
    'click #edit': function (e, value, row, index) {
        //alert(row["status"]);
        if (row["status"] === true) {
            toastr.warning('已发货');
            return;
        }
        $.ajax({
            type: "post",
            url: "/Home/delivery",
            // 预期服务器返回的数据类型
            dataType: "text", // json,html,text等等
            // 发送信息至服务器时内容编码类型
            // contentType: "application/json",
            data: {
                "order-id": row["orderid"],
            },
            success: function (status) {
                if (status === "1") {
                    toastr.success('发货成功');
                    $("#tb_orders").bootstrapTable('refresh');
                }
            },
            error: function () {
                toastr.error('Error');
            },
            complete: function () {
            }
        });
        
        //alert('编号：' + row["order-id"]);
    },
    'click #more': function (e, value, row, index) {
        window.open("/Home/order_page?orderid=" +row["orderid"],"_blank"); 
    }
};

function timestampToTime(timestamp) {
    var date = new Date(timestamp);
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = date.getDate() + ' ';
    h = date.getHours() + ':';
    m = date.getMinutes() + ':';
    s = date.getSeconds();
    return Y + M + D + h + m + s;
}