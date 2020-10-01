/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
;(function () {


    function loadAction(nx) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.load(nx);
        });
    }

    function setAction(nx) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.set(nx);
        });
    }

    window.setting = {
        load: loadAction,
        set: setAction
    };

})();