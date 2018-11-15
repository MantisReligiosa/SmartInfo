function AppViewModel() {
    var self = this;

    self.attach = function (options) {
        self[options.sourceName] = ko.computed(function () {
            return new options.factory(self);
        });
    }

    self.request = function (method, url, data, successHandler) {
        $.ajax({
            method: method,
            url: url,
            data: data,
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