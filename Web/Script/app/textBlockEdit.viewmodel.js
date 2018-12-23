function TextBlockEditViewModel(master) {
	var self = this;

	self.textColor = ko.observable();
	self.backColor = ko.observable();
	self.selectedFonts = ko.observableArray([""]);
    self.selectedFontSizes = ko.observableArray([""]);
    self.text = ko.observable();
    self.align = ko.observable();

	self.setFont = function (font) {
		self.selectedFonts.removeAll();
		self.selectedFonts.push(font);
	};

	self.setFontSize = function (fontSize) {
	    self.selectedFontSizes.removeAll();
        self.selectedFontSizes.push(fontSize);
	};
}