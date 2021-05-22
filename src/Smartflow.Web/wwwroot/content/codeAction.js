/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
;(function () {


    function loadAction(nx) {
        var ajaxSettings = { url: 'api/setting/action/list', type: 'get' };
        ajaxSettings.data = ajaxSettings.data || {};

        ajaxSettings.success = function (serverData) {

            var leftDataSource = [], rightDataSource = [];

            $.each(serverData, function () {
                leftDataSource.push({
                    value: this.id,
                    title: this.name,
                    disabled: '',
                    checked: ''
                });
            });
            
            $.each(nx.action, function () {
                rightDataSource.push(this.id);
            });

            var transfer = layui.transfer;

            //基础效果
            transfer.render({
                elem: '#transfer'
                , title: ['待选择', '已选择']
                , data: leftDataSource
                , value: rightDataSource
                , height: 530
                , width: 391
                , id: 'rightGroup'
            });
        };

        util.ajaxService(ajaxSettings);
    }

    function setAction(nx) {

        var transfer = layui.transfer,
            rightData = transfer.getData('rightGroup');

        var actionArray = [];

        $(rightData).each(function () {
            var self = this;
            actionArray.push({
                id: self.value,
                name: self.title
            });
        });

        nx.action = actionArray;
    }

    window.setting = {
        load: loadAction,
        set: setAction
    };

})();