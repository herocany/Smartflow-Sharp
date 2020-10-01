/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    function load(nx) {
        var form = layui.form;
        loadDataSource(function () {
            if (nx.command) {
                $("#decision_select").val(nx.command.id);
                $("#decision_script").val(nx.command.text);
                form.render('select');
            }
        });

        dynamicGenControl(nx);
    }

    function dynamicGenControl(nx) {
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

    function dynamicGetControl(nx) {
        var controls = $("#form_expression").find("textarea");
        $.each(controls, function () {
            var input = $(this);
            nx.set({
                id: input.attr("name"),
                expression: input.val()
                    .replace(/\r\n/g, ' ')
                    .replace(/\n/g, ' ')
                    .replace(/\s/g, ' ')
            });
        });
    }

    function loadDataSource(callback) {
        util.ajaxService({
            url: 'api/setting/GetDatabaseSourceList',
            type: 'GET',
            success: function (serverData) {
                var htmlArray = [];

                htmlArray.push("<option value=\"\"></option>");
                $.each(serverData, function () {
                    htmlArray.push("<option value='" + this.ID + "'>" + this.Name + "</option>");
                });

                $('#decision_select').html(htmlArray.join(''));
                layui.form.render(null, 'form-decision');
                callback && callback();
            }
        });
    }

    function set(nx) {

        var form = layui.form.val('form-decision');
        nx.command = $.extend(nx.command || {}, form);
        dynamicGetControl(nx);
    }

    window.setting = {
        load: load,
        set: set
    };

})();