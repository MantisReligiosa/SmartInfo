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
        unselectBlocks();
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
        initReact();
    });

    initReact = function () {
        interact('.resize-drag')
            .draggable({
                inertia: true,
                restrict: {
                    restriction: "parent",
                    endOnly: true,
                    elementRect: { top: 0, left: 0, bottom: 1, right: 1 }
                },
                autoScroll: true,

                onmove: function (event) {
                    var target = event.target,
                        // keep the dragged position in the data-x/data-y attributes
                        x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                        y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

                    // translate the element
                    target.style.webkitTransform =
                        target.style.transform =
                        'translate(' + x + 'px, ' + y + 'px)';

                    // update the posiion attributes
                    target.setAttribute('data-x', x);
                    target.setAttribute('data-y', y);
                },

                onend: function (event) {
                    var target = event.target;
                    var id = target.getAttribute('id');
                    var block = self.blocks.remove(function (block) { return block.id === id; })[0];
                    block.left = +target.getAttribute('data-x') + block.left;
                    block.top = +target.getAttribute('data-y') + block.top;
                    self.blocks.push(block);
                    selectBlock(block);
                    saveBlock(block);
                }
            });
    };

    saveBlock = function (block) {
        app.request(
            "POST",
            "/api/saveBlock",
            block,
            function (data) {}
        );
    };

    loadBlocks = function () {
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

    loadDisplays = function () {
        app.request("POST", "/api/screenResolution", {
            refreshData: false
        }, function (data) {
            self.screenHeight(data.height);
            self.screenWidth(data.width);
            data.displays.forEach(function (screen) {
                self.screens.push(screen);
            });
        });
    }

    loadFonts = function () {
        app.request("POST", "/api/fonts", {}, function (data) {
            data.fonts.forEach(function (entry) {
                self.fonts.push(entry);
            });
            data.fonSizes.forEach(function (entry) {
                self.fontSizes.push(entry);
            });
        });
    }
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});