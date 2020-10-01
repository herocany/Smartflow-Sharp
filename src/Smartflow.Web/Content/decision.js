/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
;(function () {


    function load(nx) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.load(nx);
        });
    }

    function set(nx) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.set(nx);
        });
    }

    window.setting = {
        load: load,
        set: set
    };

})();