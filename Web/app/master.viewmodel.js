function masterViewModel(app) {
    var self = this;

    self.addTextBlock = function () {
        toastr.info("Click!");
    }
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});