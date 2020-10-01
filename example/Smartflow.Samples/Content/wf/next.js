(function () {

    function doGetSelectTransition(instanceID, actorID, callback) {
        var $this = this;
        util.ajaxWFService({
            url: 'api/smf/GetTransition',
            type: 'post',
            data: JSON.stringify({
                ID: instanceID,
                ActorID: actorID
            }),
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
                CateCode: '',
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
            util.ajaxWFService({
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

