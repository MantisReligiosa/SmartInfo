function AppViewModel() {
    var self = this;
    //self.sendRequest = function (url, method, data, success, error, async) {

    //    $.ajax({
    //        method: method,
    //        url: url,
    //        data: data,
    //        dataType: "json",
    //        headers: {
    //            'Authorization': 'Bearer ' + dataModel.getAccessToken(),
    //            'Accept': 'application/json'
    //        },
    //        success: success,
    //        async: async,
    //        statusCode: {

    //            401: function () {
    //                self.navigateToLoggedOff();
    //            },
    //            403: function () {
    //                self.navigateToLoggedOff();
    //            },
    //            404: function () {
    //                dataModel.user(null);
    //                window.location.replace("/error/_404");
    //            }
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            if (xhr.status === 403 || xhr.status === 401)
    //                return
    //            if (typeof error != 'undefined') {
    //                if (error(xhr.responseJSON))
    //                    return;
    //            }

    //            var err = eval("(" + xhr.responseText + ")");
    //            var message = err.ExceptionMessage == undefined ? (err.Message == undefined ? "" : err.Message) : err.ExceptionMessage;

    //            if (message && xhr.status === 500) {
    //                try {//локализация ошибок с сервера (в виде структурированного текста)
    //                    var jsonError = JSON.parse(message);
    //                    if (jsonError && jsonError.MessageLocalize) {
    //                        var messageLocalize = app.localizeTag(jsonError.MessageLocalize);//локализируем сообщение
    //                        if (messageLocalize) {
    //                            var fullErrorMassage = messageLocalize;
    //                            if (jsonError.Params && jsonError.Params.length > 0)//подставляем значения параметров в локализированный шаблон сообщения 
    //                            {
    //                                var array = [];
    //                                if (jsonError.Params.length > 1) {
    //                                    array = $.map(jsonError.Params, function (value, index) {
    //                                        return [value];
    //                                    });

    //                                }
    //                                else {
    //                                    try {
    //                                        array = $.map(jsonError.Params, function (value, index) {
    //                                            return [value];
    //                                        });
    //                                    }
    //                                    catch (e) {
    //                                        array.push(jsonError.Params);
    //                                    }
    //                                }
    //                                fullErrorMassage = messageLocalize.format.apply(messageLocalize, array);
    //                            }
    //                            self.showErrorMessage(fullErrorMassage);
    //                        }
    //                        return;
    //                    }
    //                }
    //                catch (e) { }
    //            }

    //            self.showErrorMessage(message);
    //        }
    //    });
    //}

    //self.showErrorMessage = function (message) {
    //    toastr.error(message);
    //}

    //self.showSuccessMessage = function (message) {
    //    toastr.success(message);
    //}

    //self.showWarningMessage = function (message) {
    //    toastr.warning(message);
    //}
}

app = new AppViewModel();