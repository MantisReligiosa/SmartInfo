function TextBlockEditViewModel(master) {
	var self = this,
		textBlockBackColor,
		textBlockTextColor;

	self.setBlockBackColor = function (color) {
	    //var t = $('#textBlockBackgroundCP');
	    //var x = $('#textBlockBackgroundCP').colorpicker({ "color": "#16813D" });
	    //debugger;
	    //$("#textBlockBackgroundCP").colorpicker({ "color": "rgb(22, 129, 61)" });
		//$("#textBlockBackgroundCP").trigger('change');
	};

	self.setBlockTextColor = function (color) {
		//$("#textBlockTextColorCP").val(color);
		//$("#textBlockTextColorCP").trigger('change');
		//$("#textBlockTextColorCP").colorpicker({ color: color })
		//$('#textBlockTextColorCP').colorpicker(document.getElementById('inputcolor'), '#ea0437');
	},

	self.setFont = function (font) {
		self.selectedFonts.removeAll();
		self.selectedFonts.push(font);
	};

	self.selectedFonts = ko.observableArray([""]);

}