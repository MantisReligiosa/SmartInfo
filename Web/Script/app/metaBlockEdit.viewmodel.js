function MetaBlockEditViewModel(master) {
    var self = this;

    self.id = ko.observable();
    self.caption = ko.observable();
    self.metaFrames = ko.observableArray();
    self.metaFrames.subscribe(function (frames) {
        if (self.currentFrame.Index() == null) {
            self.selectFrame(frames[0]);
        }
    });
    self.currentFrame = {
        Index: ko.observable(),
        Duration: ko.observable(),

        UseInTimeInerval: ko.observable(false),
        UseFromTime: ko.observable(),
        UseToTime: ko.observable(),

        UseInDayOfWeek: ko.observable(false),
        UseInMon: ko.observable(),
        UseInTue: ko.observable(),
        UseInWed: ko.observable(),
        UseInThu: ko.observable(),
        UseInFri: ko.observable(),
        UseInSat: ko.observable(),
        UseInSun: ko.observable(),
         
        UseInDate: ko.observable(false),
        DateToUse: ko.observable()
    };

    self.currentFrame.UseInDayOfWeek.subscribe(function (enabled) {
        $('#daysOfWeek').multiselect(enabled ? 'enable' : 'disable');
    });

    var isDialogInitialized = false;

    self.initializeControls = function () {
        var daysOfWeekPicker = $('#daysOfWeek');
        var timeIntervalFrom = $('#timeIntervalFrom');
        var timeIntervalTo = $('#timeIntervalTo');
        var dateToUse = $('#dateToUse');
        if (
            !daysOfWeekPicker.length ||
            !timeIntervalFrom.length ||
            !timeIntervalTo.length ||
            !dateToUse.length ||
            isDialogInitialized) {
            return;
        }
        isDialogInitialized = true;

        daysOfWeekPicker.multiselect({
            onChange: function (element, checked) {
            }
        });
        daysOfWeekPicker.multiselect('disable');

        timeIntervalFrom.datetimepicker({
            onShow: function (ct) {
                this.setOptions({
                    maxTime: jQuery('#timeIntervalTo').val() ? jQuery('#timeIntervalTo').val() : false
                })
            },
            datepicker: false,
            format: 'H:i'
        });

        timeIntervalTo.datetimepicker({
            onShow: function (ct) {
                this.setOptions({
                    minTime: jQuery('#timeIntervalFrom').val() ? jQuery('#timeIntervalFrom').val() : false
                })
            },
            datepicker: false,
            format: 'H:i'
        });

        dateToUse.datetimepicker({
            timepicker: false,
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

    self.selectFrame = function (frame) {
        var tmp = self.currentFrame.Index();
        if (self.currentFrame.Index() !== undefined) {
            // сохраняем текущий фрейм
            debugger;
        }
        self.currentFrame.Index(frame.index);
        self.currentFrame.Duration(frame.duration);
        self.currentFrame.UseInTimeInerval(frame.useInTimeInterval == null ? false : frame.useInTimeInterval);
        debugger;
    }
}