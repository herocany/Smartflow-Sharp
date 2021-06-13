/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 ********************************************************************
 */
; (function () {

    window.setting = {
        load: function (nx) {
            $('iframe').each(function () {
                $(this).context.contentWindow.setting.load(nx);
            })
        },
        set: function (nx) {
            $('iframe').each(function () {
                $(this).context.contentWindow.setting.set(nx);
            })
        }
    };

})();