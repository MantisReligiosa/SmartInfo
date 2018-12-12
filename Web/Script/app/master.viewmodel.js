function masterViewModel(app) {
    var self = this;

    self.fonts = ko.observableArray([]);
    self.fontSizes = ko.observableArray([]);
    self.screenHeight = ko.observable();
    self.screenWidth = ko.observable();
    self.screens = ko.observableArray();
    self.blocks = ko.observableArray();
    self.selectedBlock = ko.observable();
    self.isSelectedBlock = ko.computed(function () {
        var block = self.selectedBlock();
        var result = (block != null);
        return result;
    });
    self.gridSteps = ko.observableArray([5, 10, 20, 25, 50]);
    self.selectedGridSteps = ko.observableArray([5]);
    self.gridEnabled = ko.observable(true);

    self.background = ko.observable("#ffffff");

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

    self.showProperties = function () {
        $("#properties")
            .modal({ backdrop: 'static', keyboard: false })
            .modal("show");
    };

    self.applyProperties = function () {
        $("#properties").modal("hide");
        var block = self.selectedBlock();
        if (block == null) {
            app.request(
                "POST",
                "/api/setBackground",
                { color: backColor },
                function () {
                    self.background(backColor);
                }
            );
        }

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
        init();
        loadFonts();
        loadDisplays();
        loadBlocks();
        initReact();
    });

    var backColor;

    init = function () {
        $('#backgroundCP').colorpicker({
            format: "rgba"
        });
        $('#backgroundCP').on('colorpickerChange', function (e) {
            backColor = e.color.toString();
        });
    };

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

                onmove: onResizeMove,

                onend: applyResizeMove
            })
            .resizable({
                // resize from all edges and corners
                edges: { left: true, right: true, bottom: true, top: true },

                // keep the edges inside the parent
                restrictEdges: {
                    outer: 'parent',
                    endOnly: true,
                },

                // minimum size
                restrictSize: {
                    min: { width: 100, height: 50 },
                },

                inertia: true,
            })
            .on('resizemove', onResizeMove)
            .on('resizeend', applyResizeMove);
    };

    onResizeMove = function (event) {
        var target = event.target,
            // keep the dragged position in the data-x/data-y attributes
            x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
            y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

        if (event.rect != null) {
            x = (parseFloat(target.getAttribute('data-x')) || 0);
            y = (parseFloat(target.getAttribute('data-y')) || 0);

            // update the element's style
            target.style.width = event.rect.width + 'px';
            target.style.height = event.rect.height + 'px';

            // translate when resizing from top or left edges
            x += event.deltaRect.left;
            y += event.deltaRect.top;

            target.setAttribute('data-w', event.rect.width);
            target.setAttribute('data-h', event.rect.height);
        }

        target.style.webkitTransform =
            target.style.transform =
            'translate(' + x + 'px, ' + y + 'px)';

        // update the posiion attributes
        target.setAttribute('data-x', x);
        target.setAttribute('data-y', y);
    };

    applyResizeMove = function (event) {
        var target = event.target;
        var id = target.getAttribute('id');
        var block = self.blocks.remove(function (block) { return block.id === id; })[0];
        var w = +target.getAttribute('data-w');
        var h = +target.getAttribute('data-h');
        if (w > 0) {
            if (self.gridEnabled()) {
                w = adjustToStep(w);
            }
            block.width = w;
        }
        if (h > 0) {
            if (self.gridEnabled) {
                h = adjustToStep(h);
            }
            block.height = h;
        }
        var x = +target.getAttribute('data-x') + block.left;
        var y = +target.getAttribute('data-y') + block.top;

        if (self.gridEnabled()) {
            x = adjustToStep(x);
            y = adjustToStep(y);
        };

        block.left = x;
        block.top = y;
        self.blocks.push(block);
        selectBlock(block);
        saveBlock(block);
    };

    adjustToStep = function (value) {
        var step = self.selectedGridSteps()[0];
        return Math.round(value / step) * step;
    }

    saveBlock = function (block) {
        app.request(
            "POST",
            "/api/saveBlock",
            block,
            function (data) { }
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
                    self.screens.push(screen);
                });
            });
        app.request(
            "GET",
            "/api/background",
            {},
            function (data) {
                if (data == '') {
                    self.background("#ffffff");
                }
                else {
                    self.background(data);
                }
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