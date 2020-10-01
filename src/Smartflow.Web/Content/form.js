/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    function load(nx) {
        var form = layui.form;
        form.val('form-bus', {
            name: nx.name,
            url: nx.url
        });
    }

    function set(nx) {
        var form = layui.form.val('form-bus');
        nx.name = form.name;
        nx.url = form.url;
        if (nx.brush) {
            nx.brush.text(nx.name);
        }
    }

    window.setting = {
        load: load,
        set: set
    };

})();