/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    function set(nx) {

        var table = layui.table,
            cacheData = table.cache.table_right;
        var actorArray = [];

        $(cacheData).each(function () {
            var self = this;
            actorArray.push({
                id: self.ID,
                name: self.Name
            });
        });

        nx.actor = actorArray;
    }

    function load(nx) {

        var table = layui.table;

        var actors = [];
        $.each(nx.actor, function () {
            actors.push(this.id);
        });

        util.table({
            elem: '#table_left'
            , url: 'api/setting/GetActor'
            , method: 'post'
            , contentType: 'application/x-www-form-urlencoded'
            , height: 489
            , page: {
                layout: ['prev', 'page', 'next', 'count']
            }
            , where: {
                arg: JSON.stringify({ actor: actors.join(',')})
            }
            , cellMinWidth: 80 //全局定义常规单元格的最小宽度，layui 2.2.1 新增
            , cols: [[
                { checkbox: true, fixed: true }
                , { field: 'ID', title: 'ID', hide: true }
                , { field: 'Name', title: '用户名', width: 120, align: 'left' }
                , {
                    title: '部门名称', templet: function (d) {
                        return d.Data.OrgName;
                    }
                }
            ]]
        });

        util.table({
            elem: '#table_right'
            , url: 'api/setting/GetAssignActor'
            , method: 'post'
            , contentType: 'application/x-www-form-urlencoded'
            , where: {
                arg: JSON.stringify({ actor: actors.join(',') })
            }
            , height: 489
            , cellMinWidth: 80 //全局定义常规单元格的最小宽度，layui 2.2.1 新增
            , page: false
            , cols: [[
                { checkbox: true, fixed: true }
                , { field: 'ID', title: 'ID', hide: true }
                , { field: 'Name', title: '用户名', width: 120, align: 'center' }
                , {
                    title: '部门名称', templet: function (d) {
                        return d.Data.OrgName;
                    }
                }
            ]]
        });

        table.on('checkbox(table_left)', function (obj) {
            //左边表格点击触发
            var checkStatus = table.checkStatus('table_left'), data = checkStatus.data;
            var methodName = data.length > 0 ? 'removeClass' : 'addClass';
            $('#right_table_1')[methodName]('layui-btn-disabled');
        });

        table.on('checkbox(table_right)', function (obj) {

            //右边表格点击触发
            var checkStatus = table.checkStatus('table_right'), data = checkStatus.data;
            var methodName = data.length > 0 ? 'removeClass' : 'addClass';
            $('#left_table_1')[methodName]('layui-btn-disabled');
        });
    }
   
    window.setting = {
        load: load,
        set: set
    };

})();


$(function () {

    loadTree();
    $("#tree").click(function () {
        var display = $("#zc").is(":hidden");
        if (display) {
            $("#zc").show();
        } else {
            $("#zc").hide();
        }
    });

    $("#zc").hover(function () { }, function () {
        $("#zc").hide();
    });

    $('#reload').on('click', function () {
        var key = $('#title').val();

        var cacheData = layui.table.cache.table_right;
        var actors = [];
        $.each(cacheData, function () {
            actors.push(this.ID);
        });

        var orgCode = $("#node-value").val();

        var searchCondition = {
            searchKey: key,
            orgCode: orgCode,
            actor: actors.join(',')
        };

        var config = {
            page: { curr: 1 },
            where: {
                arg: JSON.stringify(searchCondition)
            }
        };

        $("#right_table_1").addClass('layui-btn-disabled');
        layui.table.reload('table_left', config);
    });

    $("#left_table_1").click(function () {
        var $this = $(this);
        var key = $('#title').val();
        if (!$this.hasClass('layui-btn-disabled')) {
            var checkStatus = layui.table.checkStatus('table_right');
            removeData(checkStatus.data);

            var cacheData = layui.table.cache.table_right;
            var actors = [];
            $.each(cacheData, function () {
                actors.push(this.ID);
            });

            var config = {
                page: { curr: 1 },
                where: {
                    arg: JSON.stringify({
                        searchKey: key,
                        actor: actors.join(',')
                    })
                }
            };

            layui.table.reload('table_left', config);
            layui.table.reload('table_right', {
                page: false,
                where: {
                    arg: JSON.stringify({
                        actor: actors.join(',')
                    })
                }
            });

            $this.addClass('layui-btn-disabled');
        }
    });

    $("#right_table_1").click(function () {
        var $this = $(this);
        var key = $('#title').val();
        if (!$this.hasClass('layui-btn-disabled')) {
            var checkStatus = layui.table.checkStatus('table_left'), data = checkStatus.data;
            if (checkStatus.data.length > 0) {
                var cacheData = layui.table.cache.table_right;
                var actors = [];
                $.each(cacheData, function () {
                    actors.push(this.ID);
                });
                if (checkStatus.data.length > 0) {
                    $.each(checkStatus.data, function () {
                        actors.push(this.ID);
                    });
                }

                var config = {
                    page: { curr: 1 },
                    where: {
                        arg: JSON.stringify({
                            searchKey: key,
                            actor: actors.join(',')
                        })
                    }
                };

                layui.table.reload('table_left', config);
                layui.table.reload('table_right', {
                    page: false,
                    where: {
                        arg: JSON.stringify({
                            actor: actors.join(',')
                        })
                    }
                });
            }

            $this.addClass('layui-btn-disabled');
        }
    });

    function removeData(selectDataArray) {
        var cacheData = layui.table.cache.table_right;
        $.each(selectDataArray, function () {
            for (var i = 0, len = cacheData.length; i < len; i++) {
                var c = cacheData[i];
                if (this.ID == c.ID) {
                    cacheData.splice(i, 1);
                    break;
                }
            }
        });
    } 

    function loadTree() {
        util.ajaxWFService({
            type: 'get',
            url: 'api/setting/getorgs',
            success: function (serverData) {
                $.fn.zTree.init($("#ztree"), {
                    callback: {
                        onClick: function (event, treeId,node) {
                           // alert(node.ID);
                            $("#tree").val(node.Name);
                            $("#node-value").val(node.ID);
                        }
                    },
                    data: {
                        key: {
                            name:'Name'
                        },
                        simpleData: {
                            enable: true,
                            idKey: 'ID',
                            pIdKey: 'ParentID',
                            rootPId: 0
                        }
                    }
                }, serverData);
            }
        });
    }

});