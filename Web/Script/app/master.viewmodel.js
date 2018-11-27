function masterViewModel(app) {
    var self = this;

    self.fonts = ko.observableArray([]);
    self.fontSizes = ko.observableArray([]);
    self.screenHeight = ko.observable();
    self.screenWidth = ko.observable();
    self.screens = ko.observableArray();
    self.displays = ko.computed(function () {
        var code = $("<div>");
        self.screens().forEach(function (s) {
            code = code.append(
                $('<div>')
                    .addClass('display')
                    .css('width', s.width)
                    .css('height', s.height)
                    .css('left', s.left)
                    .css('top', s.top)
            );
        })
        return code.html();
    });

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
                refreshData: false
            },
            function (data) {
                self.screenHeight(data.height);
                self.screenWidth(data.width);
                data.displays.forEach(function (screen) {
                    //$('#videopanel').add($('div'));
                    self.screens.push(screen);
                })
            }
        );
    });
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});