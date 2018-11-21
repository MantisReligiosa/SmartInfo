function masterViewModel(app) {
    var self = this;

    self.fonts = ko.observableArray([]);
    self.fontSizes = ko.observableArray([]);
    self.screenHeight = ko.observable();
    self.screenWidth = ko.observable();

    self.addTextBlock = function () {
        toastr.info("Click!");
    }

    $(document).ready(function () {
        app.request(
            "POST",
            "/api/fonts",
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
        app.request(
            "POST",
            "/api/screenResolution",
            {
                refreshData: true
            },
            function (data) {
                self.screenHeight(data.height);
                self.screenWidth(data.width);
                debugger;
            }
        );
    });
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});