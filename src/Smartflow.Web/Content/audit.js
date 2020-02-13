(function () {

    function Audit(option) {

        var instanceID = util.doQuery('instanceID'),
            code = util.doQuery('code');
        var ticket = util.doQuery('ticket');

        this.settings = $.extend({
            instanceID: instanceID,
            ticket: ticket,
            code: code
        }, option);

        this.load();
        this._initEvent();
    }

    Audit.prototype.load = function () {
        var $this = this;
        $.each(['setCurrent'], function (index, propertyName) {
            $this[propertyName]();
        });
    };

    Audit.join = function (url, id) {
        return (!!id) ? (url + (url.indexOf('?') == -1 ? '?id=' : '&id=') + id) : url;
    }

    Audit.prototype.openAuditWindow = function (callback) {
        var $this = this,
            settings = $this.settings;

        var url = settings.auditWindow + "?instanceID=" + settings.instanceID + "&code=" + settings.code;
        util.create({
            title: '审批窗口',
            width: 600,
            height: 300,
            type: 2,
            closeBtn: 0,
            content: url,
            success: function () {
                callback && callback.call(this);
            },
            end: function () {
                $.each(['loadRecord', 'setCurrent'], function (i, propertyName) {
                    $this[propertyName]();
                });
                if (!!window.opener.doGridFresh) {
                    window.opener.doGridFresh();
                }
            }
        });
    };

    Audit.prototype.doValidation = function () {
        return this.settings.frame.doValidation();
    };

    Audit.prototype.setCurrent = function () {
        var $this = this,
            setting = $this.settings;
        if (util.isEmpty(setting.instanceID)) {

            var result = JSON.parse(decodeURIComponent(setting.ticket));
            util.ajaxService({
                url: setting.current + '/' + setting.instanceID + '?actorId=' + result.IDENTIFICATION,
                type: 'GET',
                success: function (serverData) {
                    var buttonId = setting.buttonId;
                    $(buttonId).val(serverData.Name);
                    setting.name = serverData.Name;
                    if (serverData.Category.toLowerCase() == 'end') {
                        $(setting.buttonGroupId).hide();
                    } else {
                        if (!serverData.HasAuth) {
                            $(buttonId)
                                .addClass("layui-disabled")
                                .attr("disabled", "disabled");
                        }
                    }
                }
            });
        }
    };

    Audit.prototype._initEvent = function () {
        var $this = this,
            setting = this.settings;

        $.each(['_bindButton', 'loadCategory', 'loadRecord'], function (i, propertyName) {
            $this[propertyName]();
        });
    };

    Audit.prototype.loadCategory = function () {
        var $this = this,
            setting = this.settings,
            url = setting.category + '/' + setting.code;

        util.ajaxService({
            url: url,
            type: 'get',
            success: function (data) {
                $this.loadForm(data.Url);
            }
        });
    }

    Audit.prototype.loadForm = function (url) {
        var $this = this,
            setting = $this.settings;
        util.ajaxService({
            url: setting.formUrl + '/' + setting.instanceID,
            type: 'get',
            success: function (data) {

                document
                    .getElementById("iframeContent")
                    .setAttribute('src', Audit.join(url, data&&data['FormID']));
            }
        });
    }

    Audit.prototype.loadRecord = function () {
        var $this = this,
            setting = this.settings,
            url = setting.recordUrl + '/' + setting.instanceID;

        var templet = document.getElementById("common_row").innerHTML;
        if (util.isEmpty(setting.instanceID)) {
            util.ajaxService({
                url: url,
                type: 'get',
                success: function (serverData) {
                    var htmlArray = [];
                    $.each(serverData, function () {
                        var el = templet;
                        htmlArray.push(
                            el.replace(/{{Name}}/ig, this.Name)
                                .replace(/{{Comment}}/ig, this.Comment)
                                .replace(/{{CreateDateTime}}/ig, this.CreateDateTime ? layui.util.toDateString(this.CreateDateTime, 'yyyy-MM-dd HH:mm') : '')
                                .replace(/{{Sign}}/ig, util.isEmpty(this.Url) ? '' : "<image src=\"" + this.Url+"\" />")
                                .replace(/{{AuditUserName}}/ig, this.AuditUserName)
                        );
                    });
                    $(setting.recordId).html(htmlArray.join(''));
                }
            });
        }
    }

    Audit.prototype._bindButton = function () {
        var $this = this,
            settings = $this.settings;
        $(settings.buttonId).click(function () {
            var buttonText = $(this).val();
            var methodName = (buttonText === "开始" && !settings.instanceID) ? "start" : "openAuditWindow";
            if ($this.doValidation()) {
                $this[methodName]();
            } else {
                alert("请检查表单是否填写完整。");
            }
        });
    }

    Audit.prototype.start = function () {
        var $this = this,
            settings = $this.settings;
        util.ajaxService({
            url: settings.start + '/' + settings.code,
            type: 'post',
            success: function (instanceID) {
                settings.instanceID = instanceID;
                $this.openAuditWindow();
            }
        });
    };

    Audit.prototype.bridge = function (instanceID,formID) {
        var $this = this,
            settings = $this.settings;

        var param = JSON.stringify({
            InstanceID:instanceID,
            CategoryID:settings.code,
            FormID:formID
        });

        util.ajaxService({
            url: settings.bridgeUrl,
            type: 'post',
            contentType: 'application/json',
            data: param,
            success: function () {

            }
        });
    }

    $.Cache = {};

    $.Audit = function (option) {
        $.Cache['instance'] = new Audit(option);
    };

    $.Audit.doAction = function (instanceID) {
        document.getElementById("iframeContent").contentWindow.saveForm(instanceID,function (i, k) {
            $.Cache['instance'].bridge(i, k);
        });
    };

    $.Audit.getForm = function (instanceID) {
        return document.getElementById("iframeContent").contentWindow.getForm();
    };

    $.Audit.getCurrentName = function () {
        return $.Cache['instance'].settings.name;
    };

    $.Audit.getUserInfo = function () {
        return JSON.parse(decodeURIComponent($.Cache['instance'].settings.ticket));
    };


})();

