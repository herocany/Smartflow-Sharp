/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
$(function () {

    function Page(option) {
        this.setting = $.extend({}, option);
        this.init();
    }

    Page.prototype.init = function () {
        this.bind();
        this.load();
        this.loadCategory();
    }

    Page.prototype.bind = function () {
        var $this = this;
        $.each($this.setting.event, function (propertyName) {
            var selector = '#' + propertyName;
            $(selector).click(function () {
                $this.setting.event[propertyName].call($this);
            });
        });
    }

    Page.prototype.load = function () {
        var config = this.setting.config;
        var selector = '#' + config.id;
        util.table({
            elem: selector
            , url: config.url
            , cols: [[
                { type: 'radio' }
                , { width: 60, type: 'numbers', sort: false, title: '序号', align: 'center', unresize: true }
                , { field: 'StructName', width: 240, title: '名称', align: 'left' }
                , { field: 'CateName', width: 100, title: '业务类型', sort: false, align: 'center' }
                , { field: 'Status', width: 120, title: '状态', align: 'center', templet: config.templet.checkbox, unresize: true }
                , { field: 'Memo', title: '备注', minWidth: 120, align: 'left' }
            ]]
        });

        layui.form.on(config.checkbox, function (obj) {
            var id = $(obj.elem).attr('code');
            var useState = (obj.elem.checked ? 1 : 0);
            util.ajaxService({
                type: 'put',
                url: 'api/structure/put',
                data: JSON.stringify({ Status: useState, NID: id }),
                success: function () {
                    layui.table.reload(config.id);
                }
            });
        });
    }

    Page.prototype.loadCategory = function () {
        var url = this.setting.config.categoryUrl,
            id = '#' + this.setting.config.categoryId;
        util.ajaxService({
            url: url,
            type: 'GET',
            success: function (serverData) {
                var treeObj = $.fn.zTree.init($(id), {
                    callback: {
                        beforeClick: function (id, node) {
                            return !node.isParent;
                        },
                        onClick: function (event, id, node) {
                            $("#hidCateCode").val(node.NID);
                            $("#txtCateName").val(node.Name);
                        },
                        onDblClick: function () {
                            $("#hidCateCode").val(node.NID);
                            $("#txtCateName").val(node.Name);
                            $("#zc").hide();
                        }
                    },
                    data: {
                        key: {
                            name: 'Name'
                        },
                        simpleData: {
                            enable: true,
                            idKey: 'NID',
                            pIdKey: 'ParentID',
                            rootPId: 0
                        }
                    }
                }, serverData);
                var nodes = treeObj.getNodesByFilter(function (node) { return node.level == 0; });
                if (nodes.length > 0) {
                    treeObj.expandNode(nodes[0]);
                }
            }
        });
    }

    Page.prototype.refresh = function () {
        layui.table.reload(this.setting.config.id);
    }

    Page.prototype.search = function (searchCondition) {
        layui.table.reload(this.setting.config.id, searchCondition);
    }

    Page.prototype.delete = function (id) {
        var $this = this;
        var url = this.setting.config.delete + id;
        util.ajaxService({
            url: url,
            type: 'delete',
            success: function () {
                layer.closeAll();
                $this.refresh();
            }
        });
    }

    Page.check = function (id, callback) {
        var checkStatus = layui.table.checkStatus(id);
        var dataArray = checkStatus.data;
        if (dataArray.length == 0) {
            layer.msg('请选择记录');
        } else {
            callback && callback(dataArray[0]);
        }
    }

    Page.open = function (url) {
        var h = window.screen.availHeight;
        var w = window.screen.availWidth;
        window.open(url, '流程设计器', "width=" + w + ", height=" + h + ",top=0,left=0,titlebar=no,menubar=no,scrollbars=yes,resizable=yes,status=yes,toolbar=no,location=no");
    }

    var page = new Page({
        config: {
            id: 'struct-table',
            url: 'api/structure/query',
            templet: {
                checkbox: '#struct-col-checkbox'
            },
            checkbox: 'checkbox(form-status)', 
            delete: 'api/structure/delete/',
            categoryUrl: 'api/category/get',
            categoryId: 'ztree'
        },
        event: {
            add: function () {
                Page.open('./design.html');
            },
            edit: function () {
                Page.check('struct-table', function (data) {
                    Page.open('./design.html?id=' + data.NID);
                });
            },
            delete: function () {
                var $this = this;
                Page.check('struct-table', function (data) {
                    util.confirm(util.message.delete, function () {
                        $this.delete(data.NID);
                    });
                });
            },
            search: function () {
                var searchCondition = layui.form.val('form-search');
                var config = {
                    page: { curr: 1 },
                    where: {
                        arg: JSON.stringify(searchCondition)
                    }
                };
                this.search(config);
            }
        }
    });

    window.invoke = function () {
        page.refresh();
    }
});
