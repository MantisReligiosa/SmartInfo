function loginViewModel(app) {
    var self = this;
    self.login = ko.observable("").extend({ required: true });
    self.password = ko.observable("").extend({ required: true });
    self.reset = function () {
        self.login("");
        self.password("");
    };
    self.submit = function () {
        app.request({
            method: "POST",
            url: "/api/login",
            data:
            {
                login: self.login(),
                password: self.password()
            },
            successHandler: function (data) {
                if (data === true) {
                    Finch.route('#/master');
                }
                else {
                    toastr.error("В доступе отказано");
                }
            }
        });
    };

    $(document).ready(function () {
        $(".modal")
            .modal({ backdrop: 'static', keyboard: false })
            .modal("show");
    });
}

app.attach({
    sourceName: 'login',
    factory: loginViewModel
});