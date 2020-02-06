/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
(function () {

    Configuration.controlSelectors = {
        node_cooperation: {
            pid: '#workflow_node',
            type: 'checkbox',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx) {
                $('#node_cooperation').prop('checked', parseInt(nx.cooperation, 10) === 1);
            }
        },
        decision_select: {
            pid: '#workflow_node',
            type: 'select',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx, $this) {
                //$('#node_cooperation').prop('checked', parseInt(nx.cooperation, 10) === 1);
                $this.loadDropdown(function () {

                    if (nx.command) {
                        $("#decision_select").val(nx.command.id);
                    }

                    layui.form.render(null, 'form_decision');
                });
            }
        },
        decision_script: {
            pid: '#workflow_node',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx, $this) {
                if (nx.command) {
                    $("#decision_script").val(nx.command.text);
                } else {
                    $("#decision_script").val('');
                }
            }
        },
        transition_name: {
            pid: '#workflow_transition',
            parse: function (id) {
                return $.SMF.getLineById(id);
            },
            invoke: function (nx) {
                $('#transition_name').val(nx.name);
            }
        },
        node_role: {
            pid: '#workflow_node',
            title: '角色配置',
            type: 'box',
            url: './roleSelect.html',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx) {
                var role = [];
                $.each(nx.group, function () {
                    role.push(this.name);
                });
                $('#node_role').html(role.join(","));
            }
        },
        node_user: {
            pid: '#workflow_node',
            title: '角色配置',
            type: 'box',
            url: './userSelect.html',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx) {
                var actor = [];
                $.each(nx.actor, function () {
                    actor.push(this.name);
                });
                $('#node_user').html(actor.join(","));
            }
        },
        node_action: {
            pid: '#workflow_node',
            title: '自定义动作配置',
            url: './actionSelect.html',
            type: 'box',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx) {
                var action = [];
                $.each(nx.action, function () {
                    action.push(this.name);
                });
                $('#node_action').html(action.join(","));
            }
        },
        node_name: {
            pid: '#workflow_node',
            parse: function (id) {
                return $.SMF.getNodeById(id);
            },
            invoke: function (nx) {
                $('#node_name').val(nx.name);
            }
        }
    };

    Configuration.findElementById = function (o) {
        var id = $(o).attr('id');
        var controlSelector = Configuration.controlSelectors[id];
        var vid = $(controlSelector.pid).attr('vid');
        return {
            element: controlSelector.parse(vid),
            descriptor: controlSelector
        };
    };

    Configuration.select = function (settings) {
        $.each(settings, function (i, selector) {
            $(selector).show();
            $(selector).siblings().each(function () {
                $(this).hide();
            });
        });
    };

    Configuration.show = function (settings) {
        $.each(settings, function (i, selector) {
            $(selector).show();
        });
    };

    Configuration.set = function (id) {
        $('#workflow_node').attr('vid', id);
        $('#workflow_node fieldset').hide();
    };

    Configuration.getDOMFrame = function (dom) {
        var frameId = dom.find("iframe").attr('id');
        return document.getElementById(frameId).contentWindow;
    };

    Configuration.open = function (nx, descriptor) {
        var settings = {
            type: 2,
            title: descriptor.title,
            area: ['750px', '560px'],
            shade: 0.8,
            closeBtn: 1,
            shadeClose: false,
            content: [descriptor.url, 'no']
        };

        settings.cancel = function (index, dom) {
            var frameContent = Configuration.getDOMFrame(dom);
            frameContent.setting.set(nx);
            descriptor.invoke(nx);
        };

        settings.success = function (dom, index) {
            var frameContent = Configuration.getDOMFrame(dom);
            frameContent.setting.load(nx);
        };

        layer.open(settings);
    };

    function Configuration(option) {
        this.option = $.extend({ success: '操作成功' }, option);

        this.init();
        this.bind();
    }

    Configuration.prototype.init = function () {

        var $this = this;
        $("#drawing").SMF({
            container: this.option.container,
            dblClick: function (nx) {
                $this.selectTab.call($this, nx);
            }
        });

        var id = util.doQuery('id');
        if (id) {
           var url = $this.option.url + '/' + id;
           util.ajaxService({
                url: url,
                type: 'Get',
                success: function (serverData) {
                    $.SMF.getComponentById($this.option.container).import(serverData.StructXml);
                }
            });
        } else {
            $.each(['start', 'end'], function (i, value) {
                $.SMF
                    .getComponentById($this.option.container)
                    .create(value, false);
            });
        }
    };

    Configuration.prototype.bind = function () {
        for (var propertyName in Configuration.controlSelectors) {

            var selector = '#' + propertyName,
                sel = Configuration.controlSelectors[propertyName];

            if (sel.type === 'checkbox') {
                $(selector).click(function () {
                    var result = Configuration.findElementById(this);
                    result.element.cooperation = $(this).is(':checked') ? 1 : 0;
                });
            } else if (sel.type === 'box') {
                $(selector).click(function () {
                    var result = Configuration.findElementById(this);
                    Configuration.open(result.element, result.descriptor);
                });
            } else if (sel.type === 'select') {
                layui.form.on('select(decision_select)', function (data) {
                    //alert(JSON.stringify(data));
                    if (data.value) {
                        var result = Configuration.findElementById($("#decision_select"));
                        result.element.command = $.extend(result.element.command || {}, {
                            id: data.value
                        });
                    }
                });
            }
            else {
                $(selector).keyup(function () {
                    var result = Configuration.findElementById(this);
                    var text = $(this).val();
                    if (result.element.category.toLowerCase() === 'decision') {
                        result.element.command = $.extend(result.element.command || {}, {
                            text: text
                        });
                    } else {
                        result.element.name = text;
                        if (result.element.brush) {
                            result.element.brush.text(text);
                        }
                    }
                });
            }
        }
        this.dynamicControl();
    };

    Configuration.prototype.selectTab = function (nx) {
        var $this = this,
            category = nx.category.toLowerCase(),
            _tabs = {
                start: ['#workflow_help'],
                end: ['#workflow_node'],
                line: ['#workflow_transition'],
                node: ['#workflow_node'],
                decision: ['#workflow_node']
            },
            _sTabs = {
                node: ['#attributes_node_info', '#attributes_role', '#attributes_config','#attributes_user'],
                decision: ['#attributes_decision_info', '#attributes_decision_expression', '#attributes_config'],
                end: ['#attributes_config']
            },
            controlGroup = {
                line: ['transition_name'],
                node: ['node_cooperation','node_name', 'node_role', 'node_action','node_user'],
                decision: ['decision_select', 'decision_script', 'node_action'],
                end: ['node_action']
            };

        Configuration.select(_tabs[category]);
        if (_sTabs[category]) {
            Configuration.set(nx.$id);
            Configuration.show(_sTabs[category]);
        }

        if (category === 'line') {
            $('#workflow_transition').attr('vid', nx.$id);
        }
        else if (category === 'decision') {
            $this.dynamic(nx);
        }
        if (controlGroup[category]) {
            $.each(controlGroup[category], function (i, propertyName) {
                Configuration.controlSelectors[propertyName].invoke(nx, $this);
            });
        }
    };

    Configuration.prototype.prompt = function (elementId, instance) {
        var ht = $("#" + elementId).html(),
            $this = this;

        layer.open({
            title: '流程信息',
            type: 1,
            closeBtn: 1,
            area: ['520px', '350px'], //宽高
            anim: 2,
            shadeClose: false, //开启遮罩关闭
            content: ht,
            btnAlign: 'c',
            btn: ['确定'],
            success: function (dom, index) {
                var form = layui.form;
                $this.loadCategory(function () {
                    form.render(null, 'layui_flow_info');
                    var id = util.doQuery('id');
                    if (id) {
                        $this.set(id);
                    }
                });
            },
            yes: function (index, dom) {
                var form = layui.form,
                    formData = form.val('layui_flow_info'),
                    id = util.doQuery('id');
                if (id) {
                    formData.NID = id;
                }
                formData.StructXml = instance.export();
                formData.CateName = $("#category_select :selected").text();
                util.ajaxService({
                    url: $this.option.save,
                    data: formData,
                    success: function () {
                        layer.closeAll();
                        if (window.opener && window.opener.doGridFresh) {
                            window.opener.doGridFresh();
                        }
                        window.close();
                    }
                });
            }
        });
    }

    Configuration.prototype.set = function (id) {
        var url = this.option.url + '/' + id;
        util.ajaxService({
            url: url,
            type: 'Get',
            success: function (serverData) {
                var form = layui.form;
                form.val('layui_flow_info', serverData);
            }
        });
    }

    Configuration.prototype.dynamic = function (nx) {
        var LC = nx.getTransitions();
        if (LC.length > 0) {
            var template = document.getElementById("common_expression").innerHTML,
                ele = [];

            $.each(LC, function (i) {
                ele.push(template.replace(/{{name}}/, this.name)
                    .replace(/{{expression}}/, this.expression)
                    .replace(/{{id}}/, this.$id)
                );
            });
            $("#form_expression").html(ele.join(''));
            layui.form.render(null, 'form_expression');
        }
    }
   
    Configuration.prototype.dynamicControl = function () {
        
        $("#form_expression").on("keyup", "textarea", function () {
            var vid = $('#workflow_node').attr('vid'),
                nx = $.SMF.getNodeById(vid),
                input = $(this);

            nx.set({
                id: input.attr("name"),
                expression: input.val()
                    .replace(/\r\n/g, ' ')
                    .replace(/\n/g, ' ')
                    .replace(/\s/g, ' ')
            });

        });
    }

    Configuration.prototype.loadCategory = function (callback) {
        var url = this.option.categoryUrl,
            id = '#' + this.option.categoryId;

        util.ajaxService({
            url: url,
            type: 'GET',
            success: function (serverData) {
                var htmlArray = [];
                $.each(serverData, function () {
                    htmlArray.push("<option value='" + this.NID + "'>" + this.Name + "</option>");
                });
                $(id).html(htmlArray.join(''));

                callback && callback();
            }
        });
    }

    Configuration.prototype.loadDropdown = function (callback) {
        var url = this.option.dataSourceUrl,
            id = '#' + this.option.dataSourceId;
        util.ajaxService({
            url: url,
            type: 'GET',
            success: function (serverData) {
                var htmlArray = [];

                htmlArray.push("<option value=\"\"></option>");
                $.each(serverData, function () {
                    htmlArray.push("<option value='" + this.ID + "'>" + this.Name + "</option>");
                });
                $(id).html(htmlArray.join(''));

                callback && callback();
            }
        });
    }

    Configuration.prototype.save = function () {
        var instance = $.SMF.getComponentById(this.option.container);
        if (instance.validate()) {
            this.prompt('smartflow_s_info', instance);
        } else {
            alert('流程图不符合流程定义规则');
        }
    }

    window.design = {
        Configuration: Configuration
    };

})();