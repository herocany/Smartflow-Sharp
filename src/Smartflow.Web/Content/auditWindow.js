(function () {

    function AuditWindow(option) {

        var $this = this;
        var instanceID = util.doQuery("instanceID");
        var code = util.doQuery('code');

        this.settings = $.extend({
            instanceID: instanceID,
            code: code
        }, option);

        $.each(['_bindSelect', '_initEvent'], function (index, propertyName) {
            $this[propertyName]();
        });
    }

    AuditWindow.prototype.close = function () {
        var $this = this,
            setting = $this.settings;
        var name = $this.getName();

        if (name == '开始') {
            $this.doAction(setting.instanceID);
        }

        loadAuditUser();
        parent.layer.closeAll();

    };


    AuditWindow.prototype.loadAuditUser = function () {
        var $this = this.settings;
        util.ajaxService({
            url: $this.auditUser + '/' + $this.instanceID,
            type: 'get',
            success: function (serverData) {
                var actor = [];
                $.each(serverData, function () {
                    actor.push(this.UserName);
                });

                if (actor.length > 0) {
                    alert("下一点节审批人的账号是：" + actor.join(','));
                }
            }
        });
    };


    AuditWindow.prototype._bindSelect = function () {
        //下拉框数据据绑定
        var $this = this.settings;
        util.ajaxService({
            url: $this.select + '/' + $this.instanceID,
            type: 'get',
            success: function (serverData) {
                var htmlArray = [];
                $.each(serverData, function () {
                    htmlArray.push("<option value='" + this.NID + "'>" + this.Name + "</option>");
                });

                $($this.selectOption).html(htmlArray.join(''));
                layui.form.render();
            }
        });
    };

    AuditWindow.prototype.doAction = function () {
        parent.$.Audit.doAction(this.settings.instanceID);
    };

    AuditWindow.prototype.getForm = function () {
        return parent.$.Audit.getForm();
    };

    AuditWindow.prototype.getName = function () {
        return parent.$.Audit.getCurrentName();
    };

    AuditWindow.prototype.getUserInfo = function () {
        return parent.$.Audit.getUserInfo();
    };

    AuditWindow.prototype._initEvent = function () {
        var $this = this;
        //审核窗口中确定按钮
        $($this.settings.buttonId).click(function () {
            var data = layui.form.val('next_form'),
                message = data.message;
            if (util.isEmpty(message)) {
                $this.jump(message, data.to);
            } else {
                alert($this.settings.tips);
            }
        });
    };

    AuditWindow.prototype.jump = function (message, transition) {
        var json = this.getForm();
        var result = this.getUserInfo();
        var $this = this,
            settings = this.settings,
            param = JSON.stringify({
                instanceID: settings.instanceID,
                data: {
                    Transition: transition,
                    Message: message,
                    CateCode: settings.code,
                    UUID: result.IDENTIFICATION,
                    Name: result.USERNAME,
                    Json: json
                }
            });

        util.ajaxService({
            url: settings.jump,
            type: 'post',
            contentType: 'application/json',
            data: param,
            success: function () {
                $this.close();
            }
        });
    };

    $.AuditWindow = function (option) {
        return new AuditWindow(option);
    };

})();

