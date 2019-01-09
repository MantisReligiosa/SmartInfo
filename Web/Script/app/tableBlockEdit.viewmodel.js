function TableBlockEditViewModel(master) {
    var self = this;

    self.rowTypes = ['header', 'odd', 'even'];

    self.selectedFonts = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);

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

    self.initializeControls = function () {
        self.rowTypes.forEach(function (rowType) {
            $('#tableBlock' + capitalize(rowType) + 'BackgroundCP').colorpicker({ format: "rgba" });
            $('#tableBlock' + capitalize(rowType) + 'TextColorCP').colorpicker({ format: "rgba" });
        });
    }

    function capitalize(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
}