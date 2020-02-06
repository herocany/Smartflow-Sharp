(function () {
    window.util = {
        prefix: 'http://localhost/Smartflow.Web/',
        process: 'http://localhost/Smartflow.Web/image.html?id=',
        pending: 'http://localhost/Smartflow.Web/pending.html?ticket=',
        audit: '/Smartflow.Web/auditFrame.html',
        ajaxService: function (settings) {
            var defaultSettings = $.extend({
                dataType: 'json',
                type: 'post',
                cache: false
            }, settings);

            $.ajax(defaultSettings);
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
            layer.open($.extend(defaultSettings, option));
        },
        isEmpty: function (value) {
            return (value == '' || !value) ? false : true;
        },
        doQuery: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var rw = window.location.search.substr(1).match(reg);
            return (rw != null) ? decodeURI(rw[2]) : false;
        },
        openWin: function (url, title) {
            var h = 660;
            var w = 1000;
            var top = (window.screen.availHeight - h) / 2;
            var left = (window.screen.availWidth - w) / 2;
            window.open(url, title, "width=" + w + ", height=" + h + ",top=" + top + ",left=" + left + ",titlebar=no,menubar=no,scrollbars=yes,resizable=yes,status=yes,toolbar=no,location=no");
        },
        edit: function (id, code, callback) {
            util.ajaxService({
                type: 'get',
                url: util.prefix + 'api/bridge/get/' + id + '?categoryId=' + code,
                success: function (serverData) {
                    var ticket = window.localStorage.getItem('ticket');
                    util.openWin(util.audit + '?code=' + serverData.CategoryID + '&instanceID=' + serverData.InstanceID + '&ticket=' + ticket);
                }
            });
        },
        image: function (id, code) {
            util.ajaxService({
                type: 'get',
                url: util.prefix + 'api/bridge/get/' + id + '?categoryId=' + code,
                success: function (serverData) {
                    util.openWin(util.process + serverData.InstanceID);
                }
            });
        }
    };
})();