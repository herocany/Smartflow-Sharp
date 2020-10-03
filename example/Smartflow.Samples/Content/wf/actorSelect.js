/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    function loadActor(nx,type) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.load(nx);
        });
    }

    function setActor(nx) {
        $('iframe').each(function () {
            $(this).context.contentWindow.setting.set(nx);
        });
    }

    window.setting = {
        load: loadActor,
        set: setActor
    };

})();
