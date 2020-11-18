/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    function Record(option) {
        this.setting = $.extend({}, option);
        this.init();
    }

    Record.prototype.init = function () {
        var $this = this;
        var setting = $this.setting;
        this.loadBrigdge(setting.key,function (s) {
            $this.load(s.InstanceID);
        });
    }

    Record.prototype.load = function (instanceID) {
        var $this = this,
            setting = this.setting,
            url = setting.url + '/' + instanceID;
        util.ajaxWFService({
            url: url,
            type: 'get',
            success: function (serverData) {
                var htmlArray = [];
                $.each(serverData, function () {
                    var el = setting.templet;
                    htmlArray.push(
                        el.replace(/{{Name}}/ig, setting.Type ? this.OrganizationName : this.Name)
                          //  .replace(/{{NodeName}}/ig, setting.Type ? this.OrganizationName:this.NodeName)
                         //   .replace(/{{UserGroup}}/ig, this.UserGroup)
                            .replace(/{{Comment}}/ig, this.Comment)
                            .replace(/{{CreateTime}}/ig, this.CreateTime ? layui.util.toDateString(this.CreateTime, 'yyyy-MM-dd HH:mm') : '')
                            .replace(/{{Sign}}/ig, util.isEmpty(this.Url) ? '' : "<image src=\"" + this.Url + "\" />")
                            .replace(/{{AuditUserName}}/ig, this.AuditUserName)
                    );
                });

                $(setting.id).html(htmlArray.join(''));
                setting.done && setting.done(serverData);
            }
        });
    }

    Record.prototype.loadBrigdge = function (id, callback) {
        var $this = this;
        util.ajaxWFService({
            type: 'get',
            url: $this.setting.bridge + '/' + id,
            success: function (s) {
                callback && callback(s);
            }
        });
    }

    Record.prototype.refresh = function () {
        layui.table.reload(this.setting.config.id);
    };

    $.Record = function (option) {
        var templet = "<tr><td class=\"flow-node\" next=\"{{NodeName}}\">{{Name}}</td><td class=\"flow-message\">{{Comment}}<div class=\"flow-audit-info\"><span>审批时间：{{CreateTime}}</span><span>{{Sign}}</span><span>审批人：{{AuditUserName}}</span></div></td></tr>";
        return new Record($.extend({
            templet: templet,
            id: '#record-table',
            url: 'api/record/get',
            bridge: 'api/bridge/getbridge'
        }, option));
    }

})();
