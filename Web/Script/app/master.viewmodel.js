function masterViewModel(app) {
    var self = this;

    self.fonts = ko.observableArray([]);
    self.fontSizes = ko.observableArray([]);
    self.screenHeight = ko.observable();
    self.screenWidth = ko.observable();
    self.screens = ko.observableArray();
    self.blocks = ko.observableArray();
    self.selectedBlock = ko.observable();

    self.addTextBlock = function () {
        app.request(
            "POST",
            "/api/addTextBlock",
            {},
            function (data) {
                data.selected = false;
                self.blocks.push(data);
            }
        );
    };

    selectBlock = function (bind) {
        self.blocks.remove(bind);
        /*
        self.blocks.forEach(function (block) {
            block.selected = false;
        });
        */
        self.selectedBlock.selected = false;
        bind.selected = true;
        self.blocks.push(bind);
        self.selectedBlock(bind);
    };

    unselectBlocks = function () {
        var blocks = self.blocks.remove(function (block) { return block.selected; });
        blocks.forEach(function (block) {
            block.selected = false;
            self.blocks.push(block);
        });
        self.selectedBlock(null);
    }

    $(document).ready(function () {
        loadFonts();
        loadDisplays();
        loadBlocks();
    });

    function loadBlocks() {
        app.request(
            "GET",
            "/api/blocks",
            {},
            function (data) {
                data.forEach(function (block) { 
                    block.selected = false;
                    self.blocks.push(block);
                });
            }
        );
    }

    function loadDisplays() {
        app.request("POST", "/api/screenResolution", {
            refreshData: false
        }, function(data) {
            self.screenHeight(data.height);
            self.screenWidth(data.width);
            data.displays.forEach(function(screen) {
                self.screens.push(screen);
            });
        });
    }

    function loadFonts() {
        app.request("POST", "/api/fonts", {}, function(data) {
            data.fonts.forEach(function(entry) {
                self.fonts.push(entry);
            });
            data.fonSizes.forEach(function(entry) {
                self.fontSizes.push(entry);
            });
        });
    }
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});