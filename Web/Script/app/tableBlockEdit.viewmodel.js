function TableBlockEditViewModel(master) {
    var self = this;

    self.rowTypes = ['header', 'odd', 'even'];

    self.selectedFonts = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);
    self.selectedFontIndexes = ko.observableArray([""]);
    self.filePath = ko.observable();

    self.rows = ko.observableArray();
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

    self.openFileDialog = function () {
        $('#inputFile').on('change', function (e) {
            var file = this.files[0];
            var reader = new FileReader();

            reader.onload = (function (theFile) {
                return function (e) {
                    var string = e.target.result;
                    var lines = string.split('\n');
                    var rowsArray = [];
                    var headerArray = [];
                    var rowIndex = 0;
                    lines.forEach(function (line) {
                        if (line == "")
                            return;
                        var cells = line.split(',');
                        if (cells.length <= 1)
                            cells = line.split(';');
                        var columnIndex = 0;
                        if (rowIndex == 0) {
                            cells.forEach(function (cell) {
                                headerArray[columnIndex] = cell;
                                columnIndex++;
                            });
                        }
                        else {
                            var index = rowIndex - 1;
                            var row = { index: index, cells: [] };
                            cells.forEach(function (cell) {
                                row.cells[columnIndex] = cell;
                                columnIndex++;
                            });
                            rowsArray[index] = row;
                        }
                        rowIndex++;
                    });
                    self.rows.removeAll();
                    self.rows(rowsArray);
                    self.header.removeAll();
                    self.header(headerArray);
                };
            })(file);

            reader.readAsText(file, 'CP1251');
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
}