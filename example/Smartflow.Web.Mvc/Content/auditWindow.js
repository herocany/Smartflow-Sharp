(function () {

    function AuditWindow(option) {

        var $this = this;
        this.settings = $.extend({}, option);

        $.each(['_bindSelect', '_initEvent'], function (index, propertyName) {
            $this[propertyName]();
        });
    };

    AuditWindow.prototype.close = function (action) {
        var $this = this,
            settings = $this.settings;
        parent.util.close(function () {
            var name = $this.getName();
            if (name== '开始' && action != 'reject') {
                $this.doAction(settings.instanceID);
            }
            parent.$.Audit.reload();
        });
    };

    AuditWindow.prototype._bindSelect = function () {
        //下拉框数据据绑定
        var $this = this.settings;
        util.ajaxPost({
            url: $this.select,
            data: { instanceID: $this.instanceID },
            success: function (serverData) {
                var htmlArray = [];
                $.each(serverData, function () {
                    var template = document.getElementById($this.dropdownTemplate).innerHTML;
                    htmlArray.push(
                        template
                            .replace(/{{Destination}}/ig, this.Destination)
                            .replace(/{{NID}}/ig, this.NID)
                            .replace(/{{Name}}/ig, this.Name)
                    );
                });
                $($this.selectOption).html(htmlArray.join(''));
            }
        });

    };

    AuditWindow.prototype.doAction = function () {
        parent.$.Audit.doAction(this.settings.instanceID);
    };

    AuditWindow.prototype.getName = function () {
        return parent.$.Audit.getCurrentName();      
    };

    AuditWindow.prototype._initEvent = function () {
        var $this = this;
        //审核窗口中确定按钮
        $($this.settings.ok).click(function () {
            var selectTransition = $("#ddlOperate option:selected").val();
            var message = $("#txtMessage").val();
            if (util.isEmpty(message)) {
                var methodName = selectTransition == 'back' ? 'back' : 'jump';
                $this[methodName](message, selectTransition);

                $this.setCurrent();
            } else {
                alert($this.settings.tips);
            }
        });

        $($this.settings.buttonReject).click(function () {
            $this.reject();
        });
    };

    AuditWindow.prototype.jump = function (message, transition) {
        var $this = this,
            settings = this.settings;

        util.ajaxPost({
            url: settings.jump,
            traditional: true,
            data: {
                instanceID: settings.instanceID,
                transitionID: transition,
                url: settings.page,
                message: message
            },
            success: function () {
                $this.close();
            }
        });
    };

    AuditWindow.prototype.reject = function () {
        var $this = this,
            settings = this.settings;
        util.ajaxPost({
            url: settings.reject,
            traditional: true,
            data: { instanceID: settings.instanceID },
            success: function () {
                $this.close('reject');
            }
        });
    };

    AuditWindow.prototype.back = function (message) {
        var $this = this,
            settings = this.settings;
        util.ajaxPost({
            url: settings.back,
            traditional: true,
            data: {
                instanceID: settings.instanceID,
                url: settings.page,
                message: message
            },
            success: function () {
                $this.close();
            }
        });
    };

    $.AuditWindow = function (option) {
        return new AuditWindow(option);
    };

})();

