function masterViewModel(app) {
    var self = this;

    self.addTextBlock = function () {
        toastr.info("Click!");
    }
}

app.attach({
    viewName: 'master',
    factory: masterViewModel
});