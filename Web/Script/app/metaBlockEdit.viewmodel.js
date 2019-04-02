function MetaBlockEditViewModel(master) {
    var self = this;

    self.id = ko.observable();
    self.caption = ko.observable();
    self.metaFrames = ko.observableArray();

    self.deleteFrames = function () {
        if (self.metaFrames().filter(function (metaframe) {
            return !metaframe.selected;
        }).length == 0) {
            alert("Нельзя удалить все фреймы!");
            return;
        }
        self.metaFrames.remove(function (frame) {
            return frame.selected;
        });
    }

    self.addFrame = function () {
        var index = self.metaFrames().length + 1;
        var frame = {
            selected: false,
            index: index,
            duration: 5
        };
        self.metaFrames.push(frame);
    }
}