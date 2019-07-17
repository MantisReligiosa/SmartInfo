function MetaBlockEditViewModel(master) {
    var self = this;

    self.id = ko.observable();
    self.caption = ko.observable();
    self.metaFrames = ko.observableArray();

    var isDialogInitialized = false;

    self.initializeControls = function () {
        var daysOfWeekPicker = $('#daysOfWeek');
        if (!daysOfWeekPicker.length || isDialogInitialized) {
            return;
        }
        isDialogInitialized = true;
        $('#daysOfWeek').multiselect({
            onChange: function (element, checked) {
            }
        });
    }

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
        var index = Math.max(...self.metaFrames().map(function (f) {
            return f.index;
        })) + 1;
        var frame = {
            selected: false,
            index: index,
            duration: 5,
            blocks: ko.observableArray()
        };
        self.metaFrames.push(frame);
    }

    self.selectFrame = function (f) {

    }
}