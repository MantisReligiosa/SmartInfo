function AppViewModel() {
    var self = this;

    self.attach = function (options) {
        self[options.sourceName] = ko.computed(function () {
            return new options.factory(self);
        });
    }

    self.errorMsg = function (message) {
        toastr.error(message);
    }

    self.infoMsg = function (message) {
        toastr.info(message);
    }

    self.request = function (method, url, data, successHandler) {
        $.ajax({
            method: method,
            url: url,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                'Accept': 'application/json'
            },
            statusCode: {

                401: function () {
                    debugger;
                },
                403: function () {
                    debugger;
                },
                404: function () {
                    debugger;
                },
                406: function () {
                    location.reload(true);
                }
            },
            success: successHandler,
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.error(thrownError);
            }
        });
    }
}

var app = new AppViewModel();