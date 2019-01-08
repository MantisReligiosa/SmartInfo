function TableBlockEditViewModel(master) {
    var self = this;

    self.selectedFonts = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);
    self.headerBackColor = ko.observable();
    self.headerTextColor = ko.observable();
    self.headerItalic = ko.observable(false);
    self.headerBold = ko.observable(false);
    self.headerAlign = ko.observable();
    self.oddBackColor = ko.observable();
    self.oddTextColor = ko.observable();
    self.oddItalic = ko.observable(false);
    self.oddBold = ko.observable(false);
    self.oddAlign = ko.observable();

    self.setFont = function (font) {
        self.selectedFonts.removeAll();
        self.selectedFonts.push(font);
    };

    self.setFontSize = function (fontSize) {
        self.selectedFontSizes.removeAll();
        self.selectedFontSizes.push(fontSize);
    };

    self.initializeControls = function () {
        $('#tableBlockHeaderBackgroundCP').colorpicker({format: "rgba"});
        $('#tableBlockHeaderTextColorCP').colorpicker({ format: "rgba" });
        $('#tableBlockOddBackgroundCP').colorpicker({ format: "rgba" });
        $('#tableBlockOddTextColorCP').colorpicker({ format: "rgba" });
    }
}