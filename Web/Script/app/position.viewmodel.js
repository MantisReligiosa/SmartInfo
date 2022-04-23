function PositionViewModel(master) {
    var self = this;

    self.top = ko.observable();
    self.left = ko.observable();
    self.width = ko.observable();
    self.height = ko.observable();
    self.zIndex = ko.observable();
}