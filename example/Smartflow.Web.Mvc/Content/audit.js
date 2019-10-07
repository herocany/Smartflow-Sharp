(function () {

    function Audit(option) {
        this.settings = $.extend({}, option);
        this.reload();
        this._initEvent();
    }

    Audit.prototype.reload = function () {
        var $this = this;
        $.each(['setCurrent', 'load', 'loadAuditUser'], function (index, propertyName) {
            $this[propertyName]();
        });
    };

    Audit.prototype.openAuditWindow = function (callback) {
        var $this = this,
            settings = $this.settings;

        var url = settings.auditWindow + '?url=' + settings.page + "&instanceID=" + settings.instanceID;

        util.create({
            title: '审批窗口',
            width: 600,
            height: 300,
            type: 2,
            content: url,
            success: function () {
                callback && callback.call(this);
            }
        });
    };

    Audit.prototype.getStructure = function () {
        return this.settings.frame.getWorkflowId();
    };

    Audit.prototype.loadAuditUser = function () {
        var template = "<span style=\"color: red;\">{{USERNAME}}({{EMPLOYEENAME}})</span>";

        //下拉框数据据绑定
        var settings = this.settings;
        if (util.isEmpty(settings.instanceID)) {
            util.ajaxPost({
                url: settings.auditUser,
                data: {
                    instanceID: settings.instanceID
                },
                success: function (serverData) {
                    var htmlArray = [];
                    $.each(serverData, function () {
                        var el = template;
                        htmlArray.push(
                            el.replace(/{{USERNAME}}/ig, this.USERNAME)
                                .replace(/{{EMPLOYEENAME}}/ig, this.EMPLOYEENAME)
                        );
                    });

                    $(settings.auditID).html('当前节点待办审批用户：' + htmlArray.join(''));
                }
            });
        }
    };

    Audit.prototype.doValidation = function () {
        return this.settings.frame.doValidation();
    }

    Audit.prototype.selectTab = function () {
        $("#tabs li:eq(1)").trigger('click');
    };

    Audit.prototype.setCurrent = function () {
        var $this = this,
            settings = $this.settings;
        if (util.isEmpty(settings.instanceID)) {

            util.ajaxPost({
                url: settings.current,
                async: false,
                data: {
                    instanceID: settings.instanceID
                },
                success: function (serverData) {
                    var buttonID = settings.buttonID;
                    $(buttonID).val(serverData.Name);
                    settings.name = serverData.Name;

                    if (serverData.Category.toLowerCase() == 'end') {
                        $(settings.buttonGroup).hide();
                    } else {
                        if (!serverData.HasAuth) {
                            $(buttonID)
                                .addClass("layui-disabled")
                                .attr("disabled", "disabled");
                        }
                    }
                }
            });
        }
    };

    Audit.prototype.load = function () {

        var self = this,
            settings = self.settings;

        if (util.isEmpty(settings.instanceID)) {
            util.ajaxPost({
                url: settings.record,
                data: { instanceID: settings.instanceID },
                success: function (serverData) {
                    var htmlArray = [];
                    $.each(serverData, function () {
                        var template = settings.recordTemplate;
                        htmlArray.push(
                            template
                                .replace(/{{NODENAME}}/ig, this.NODENAME)
                                .replace(/{{MESSAGE}}/ig, this.MESSAGE)
                        );
                    });
                    $(settings.recordID).html(htmlArray.join(''));
                }
            });
        }
    };

    Audit.prototype._initEvent = function () {
        var $this = this;
        $.each(['_bindTab', '_bindButton'], function (i, propertyName) {
            $this[propertyName]();
        });
    };

    Audit.prototype._bindTab = function () {
        $("#tabs li").click(function () {
            var $el = $(this),
                current_filter_selector = "div[relationship=" + $el.attr("relationship") + "]";

            if (!$el.hasClass("smartflow_tab_select")) {
                $el.addClass("smartflow_tab_select");
                $("div.smartflow_tab_content").filter(current_filter_selector).show();
            }

            $el.siblings().each(function () {
                if ($(this).hasClass("smartflow_tab_select")) {
                    $(this).removeClass("smartflow_tab_select");
                }
                var siblings_filter_selector = "div[relationship=" + $(this).attr("relationship") + "]";
                $("div.smartflow_tab_content").filter(siblings_filter_selector).hide();
            });
        });

        $("#tabs li:eq(0)").trigger('click');
    };

    Audit.prototype._bindButton = function () {

        var $this = this,
            settings = $this.settings;
        $(settings.buttonID).click(function () {
            var buttonText = $(this).val();
            var methodName = buttonText === "开始" ? "start" : "openAuditWindow";

            if ($this.doValidation()) {
                $this[methodName]();
            } else {
                alert("请检查表单是否填写完整。");
            }
        });
    };

    Audit.prototype.start = function () {
        var $this = this,
            structureID = $this.getStructure(),
            settings = $this.settings;
        util.ajaxPost({
            url: settings.start,
            data: { structureID: structureID },
            success: function (instanceID) {
                settings.instanceID = instanceID;
                $this.openAuditWindow();
            }
        });
    };

    $.Cache = {};

    $.Audit = function (option) {
        $.Cache['instance'] = new Audit(option);
    };

    $.Audit.doAction = function (instanceID) {
        document.getElementById("iframeContent").contentWindow.saveForm(instanceID);
    };

    $.Audit.getCurrentName = function () {
        return $.Cache['instance'].settings.name;
    }

    $.Audit.reload = function () {

        $.each(['selectTab', 'reload'], function (i,methodName) {
            $.Cache['instance'][methodName]();
        });
    };

})();

