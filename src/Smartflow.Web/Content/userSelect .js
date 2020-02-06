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

        table.render({
            elem: '#table_left'
            , url: 'api/setting/GetActor'
            , height: 449
            , page: {
                layout: ['prev', 'page', 'next']
            }
            , where: {
                actor: (nx.actor.length > 0) ? actors.join(',') : ''
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

       

        table.render({
            elem: '#table_right'
            , url: 'api/setting/GetAssignActor'
            , where: {
                actor: (nx.actor.length > 0) ? actors.join(',') : ''
            }
            , height: 449
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

    $('#reload').on('click', function () {
        var key = $('#title').val();

        var cacheData = layui.table.cache.table_right;
        var actors = [];
        $.each(cacheData, function () {
            actors.push(this.ID);
        });

        var config = {
            page: { curr: 1 },
            where: {
                key: key,
                actor: actors.join(',')
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
                    key: key,
                    actor: actors.join(',')
                }
            };

            layui.table.reload('table_left', config);
            layui.table.reload('table_right', {
                page: false,
                where: {
                    actor: actors.join(',')
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
                        key: key,
                        actor: actors.join(',')
                    }
                };

                layui.table.reload('table_left', config);
                layui.table.reload('table_right', {
                    page: false,
                    where: {
                        actor: actors.join(',')
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

});