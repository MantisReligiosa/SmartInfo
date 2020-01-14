﻿function DatetimeBlockEditViewModel(master) {
	var self = this;

    self.caption = ko.observable();
    self.textColor = ko.observable();
	self.backColor = ko.observable();
    self.selectedFonts = ko.observableArray([""]);
    self.selectedFormats = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);
    self.selectedFontIndexes = ko.observableArray([""]);
    self.align = ko.observable(0);
    self.italic = ko.observable(false);
    self.bold = ko.observable(false);

	self.setFont = function (font) {
		self.selectedFonts.removeAll();
		self.selectedFonts.push(font);
    };

    self.setFormat = function (format) {
        self.selectedFormats.removeAll();
        self.selectedFormats.push(format);
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
        $('#datetimeBlockBackgroundCP').colorpicker({
            format: "rgba"
        });

        $('#datetimeBlockTextColorCP').colorpicker({
            format: "rgba"
        });
    }
}