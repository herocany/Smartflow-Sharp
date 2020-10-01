(function () {
    window.util = {
        prefix: 'http://localhost/Smartflow.Web/',
        actor: 'http://localhost/Smartflow.Web/actorSelect.html',
        carbon: 'http://localhost/Smartflow.Web/carbonSelect.html',
        ajaxService: function (settings) {
            var url = util.prefix + settings.url;
            var defaultSettings = $.extend({
                dataType: 'json',
                type: 'post',
                contentType:'application/json',
                cache: false,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('流程发生异常,请与系统管理员联系');
                }
            }, settings, { url: url });
            $.ajax(defaultSettings);
        },
        confirm: function (message, callback) {
            window.top.layer.confirm(message, {
                title: util.title,
                btn: ['确定', '取消']
            }, function (index) {
                callback & callback();
                window.top.layer.close(index);
            }, function (index) {
                window.top.layer.close(index);
            });
        },
        message: {
            record: '请选择记录',
            running: '请选择正在运行中流程',
            success: '操作成功',
            kill: '确定终止流程',
            delete: '确定删除'
        },
        create: function (option) {
            var defaultSettings = {
                type: 2,
                title: false,
                closeBtn: 1,
                shade: [0.5],
                shadeClose: false,
                area: [option.width + 'px', option.height + 'px'],
                offset: 'auto'
            };
            return layer.open($.extend(defaultSettings, option));
        },
        serialize: function (obj) {
            var arr = [];
            for (var p in obj) {
                arr.push(p + '=' + obj[p]);
            }
            return arr.join('&');
        },
        isEmpty: function (value) {
            return (value == '' || !value) ? false : true;
        },
        openWin: function (url, title, width, height) {
            var h = height || 720;
            var w = width || 1100;
            var top = (window.screen.availHeight - h) / 2-20;
            var left = (window.screen.availWidth - w) / 2;
            window.open(url, title, "width=" + w + ", height=" + h + ",top=" + top + ",left=" + left + ",titlebar=no,menubar=no,scrollbars=yes,resizable=yes,status=yes,toolbar=no,location=no");
        },
        doQuery: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var rw = window.location.search.substr(1).match(reg);
            return (rw != null) ? rw[2]: false;
        },
        table: function (option) {
            var url = util.prefix + option.url;
            var setting = {
                page: true
                , defaultToolbar: false
                , method: 'post'
                , contentType: 'application/x-www-form-urlencoded'
                , cellMinWidth: 80
                , parseData: function (res) {
                    return {
                        "code": (res.Code == 200 ? 0 : res.Code),
                        "msg": res.Message,
                        "count": res.Total,
                        "data": res.Data
                    };
                }
            };
            layui.table.render($.extend(setting, option, { url: url }));
        }
    };
})();