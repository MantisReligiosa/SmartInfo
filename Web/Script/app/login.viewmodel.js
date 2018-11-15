function loginViewModel(app) {
    var self = this;
    self.login = ko.observable("").extend({ required: true });
    self.password = ko.observable("").extend({ required: true });
    self.reset = function () {
        self.login("");
        self.password("");
    };
    self.submit = function () {
        app.request(
             "POST",
            "/api/login",
            {
                login: self.login(),
                password: self.password()
            },
            function (data) {
                if (data === true) {
                    window.location.href = '/master';
                }
                else {
                    toastr.error("В доступе отказано");
                }
            }
        );
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