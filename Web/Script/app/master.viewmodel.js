function masterViewModel(app) {
    var self = this;

    self.fonts = ko.observableArray([]);
    self.fontSizes = ko.observableArray([]);

    self.addTextBlock = function () {
        toastr.info("Click!");
    }

    $(document).ready(function () {
        app.request(
            "POST",
            "/master/fonts",
            {},
            function (data) {
                data.fonts.forEach(function (entry) {
                    self.fonts.push(entry);
                });
                data.fonSizes.forEach(function (entry) {
                    self.fontSizes.push(entry);
                });
            }
        );
    });
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});