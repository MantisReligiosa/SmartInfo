function TableBlockEditViewModel(master) {
    var self = this;

    self.rowTypes = ['header', 'odd', 'even'];

    self.caption = ko.observable();
    self.selectedFonts = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);
    self.selectedFontIndexes = ko.observableArray([""]);
    self.filePath = ko.observable();
    self.encoding = ko.observable(0);
    self.format = ko.observable(0);
    self.accept = ko.computed(function () {
        if (self.format() == 0)
            return ".csv";
        if (self.format() == 1)
            return ".xls,.xlsx";
        return "";
    });

    self.sizeUnits = ko.observableArray();
    self.selectedRowUnit = ko.observable();
    self.selectedColumnUnit = ko.observable();
    self.selectedColumnHeader = ko.observable();

    self.rows = ko.observableArray();
    self.rowHeights = ko.observableArray();
    self.columnWidths = ko.observableArray();
    self.header = ko.observableArray();

    self.rowTypes.forEach(function (rowType) {
        self[rowType + 'BackColor'] = ko.observable();
        self[rowType + 'TextColor'] = ko.observable();
        self[rowType + 'Italic'] = ko.observable(false);
        self[rowType + 'Bold'] = ko.observable(false);
        self[rowType + 'Align'] = ko.observable();
    });

    self.setFont = function (font) {
        self.selectedFonts.removeAll();
        self.selectedFonts.push(font);
    };

    self.setFontSize = function (fontSize) {
        self.selectedFontSizes.removeAll();
        self.selectedFontSizes.push(fontSize);
    };

    self.setFontIndex = function (fontIndex) {
        self.selectedFontIndexes.removeAll();
        self.selectedFontIndexes.push(fontIndex);
    };

    self.initializeControls = function () {

        self.rowTypes.forEach(function (rowType) {
            $('#tableBlock' + capitalize(rowType) + 'BackgroundCP').colorpicker({ format: "rgba" });
            $('#tableBlock' + capitalize(rowType) + 'TextColorCP').colorpicker({ format: "rgba" });
        });
    }

    self.selectedRowHeight = ko.observable({ index: 0 });
    self.selectedColumnWidth = ko.observable({ index: 0 });

    self.selectRowHeight = function (rowHeight) {
        if (!rowHeight)
            return;
        self.saveCurrentRowChanges();
        self.selectedRowHeight(rowHeight);
        var selectedItem = ko.utils.arrayFirst(self.sizeUnits(), function (item) {
            return item.sizeUnits == rowHeight.units
        });
        if (selectedItem) {
            selectedItem.isSelected = true;
        }
        self.selectedRowUnit(selectedItem);
    };

    self.prevRow = function () {
        var currentIndex = self.selectedRowHeight().index;
        var prevRowHeight = ko.utils.arrayFirst(self.rowHeights(), function (item) {
            return item.index == currentIndex - 1;
        });
        if (prevRowHeight) {
            self.selectRowHeight(prevRowHeight);
        }
    }

    self.nextRow = function(){
        var currentIndex = self.selectedRowHeight().index;
        var nextRowHeight = ko.utils.arrayFirst(self.rowHeights(), function (item) {
            return item.index == currentIndex + 1;
        });
        if (nextRowHeight) {
            self.selectRowHeight(nextRowHeight);
        }
    }

    self.selectColumnWidth = function (columnWidth) {
        if (!columnWidth)
            return;
        self.saveCurrentColumnChanges();
        self.selectedColumnWidth(columnWidth);
        var selectedItem = ko.utils.arrayFirst(self.sizeUnits(), function (item) {
            return item.sizeUnits == columnWidth.units
        });
        var columnHeader = self.header()[self.selectedColumnWidth().index];
        if (columnHeader) {
            self.selectedColumnHeader(columnHeader);
        }
        if (selectedItem) {
            selectedItem.isSelected = true;
        }
        self.selectedColumnUnit(selectedItem);
    };

    self.saveCurrentColumnChanges = function () {
        if (!self.selectedColumnWidth())
            return;
        var currentColumn = ko.utils.arrayFirst(self.columnWidths(), function (item) {
            return item.index == self.selectedColumnWidth().index;
        });
        currentColumn.value = self.selectedColumnWidth().value;
        currentColumn.units = self.selectedColumnUnit().sizeUnits;
    }

    self.saveCurrentRowChanges = function() {
        if (!self.selectedRowHeight())
            return;
        var currentRow = ko.utils.arrayFirst(self.rowHeights(), function (item) {
            return item.index == self.selectedRowHeight().index;
        });
        currentRow.value = self.selectedRowHeight().value;
        currentRow.units = self.selectedRowUnit().sizeUnits;
    }

    self.prevCol = function () {
        var currentIndex = self.selectedColumnWidth().index;
        var prevColumnWidth = ko.utils.arrayFirst(self.columnWidths(), function (item) {
            return item.index == currentIndex - 1;
        });
        if (prevColumnWidth) {
            self.selectColumnWidth(prevColumnWidth);
        }
    }

    self.nextCol = function () {
        var currentIndex = self.selectedColumnWidth().index;
        var nextColumnWidth = ko.utils.arrayFirst(self.columnWidths(), function (item) {
            return item.index == currentIndex + 1;
        });
        if (nextColumnWidth) {
            self.selectColumnWidth(nextColumnWidth);
        }
    }

    self.openFileDialog = function () {
        $('#inputFile').on('change', function (e) {
            var tmp = $('#inputFile').val();
            if (tmp == "") {
                return;
            }
            var file = this.files[0];
            var reader = new FileReader();

            reader.onload = (function (theFile) {
                return function (e) {
                    var extension = '';
                    if (self.format() == 0)
                        extension = "csv";
                    if (self.format() == 1)
                        extension = "xls";

                    app.request(
                        "POST",
                        "/api/parseTable",
                        {
                            context: btoa(
                                new Uint8Array(e.target.result)
                                    .reduce((data, byte) => data + String.fromCharCode(byte), '')),
                            extension: extension
                        },
                        function (data) {
                            self.header.removeAll();
                            self.header(data.header);
                            self.rows.removeAll();
                            self.rows(data.rows);
                            data.rows.forEach(function (value, index, array) {
                                self.rowHeights.push({ index, units:0 })
                            });

                            data.header.forEach(function (value, index, array) {
                                self.columnWidths.push({ index, units: 0 })
                            });
                        }
                    );
                };
            })(file);
            //var encoding = self.encoding();
            //var encodingFormat = '';
            //if (encoding == "0") {
            //    encodingFormat = 'CP1251'
            //} else
            //    if (encoding == "1") {
            //        encodingFormat = 'utf8'
            //    }
            reader.readAsArrayBuffer(file);
            $('#inputFile').val("");
        }).click();
    }

    self.loadFile = function (evt) {
        debugger;
        var file = self.filePath();
        var reader = new FileReader();

        reader.onload = function (event) {
            debugger;
        };

        reader.readAsText(file);
    }

    function capitalize(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    self.OnKeyDown = function (ctx, e) {
        if (!((e.keyCode > 95 && e.keyCode < 106)
            || (e.keyCode > 47 && e.keyCode < 58)
            || e.keyCode == 8)) {
            return false;
        }
        return true;
    }
}