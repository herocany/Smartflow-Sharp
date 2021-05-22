(function () {

    function doGetSelectTransition(instanceID, actorID, callback) {
        var $this = this;
        var url = 'api/smf/'+instanceID+'/transition/'+actorID+'/list';
        util.ajaxService({
            url: url,
            type:'get',
            success: function (serverData) {
                callback && callback(serverData);
            }
        });
    }

    window.util.next = function (message, instanceID, option, callback) {
        var $this = this;
        var result = util.getUser();
        var entry = {
            instanceID: instanceID,
            actorID: result.ID,
            message: message,
            data: {
                UUID: result.ID,
                Name: result.Name,
                CategoryCode: '',
                Group: '',
                Actor: '',
                Carbon: '',
                Organization: ''
            }
        };

        $.extend(entry.data, option);

        doGetSelectTransition(instanceID, result.ID, function (transitionArray) {
            var tran = transitionArray[0];
            entry.TransitionID = tran.NID;
            util.ajaxService({
                url: 'api/smf/jump',
                type: 'post',
                contentType: 'application/json',
                data: JSON.stringify(entry),
                success: function () {
                    callback && callback();
                }
            });
        });
    }

})();

