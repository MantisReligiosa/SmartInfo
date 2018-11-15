function masterViewModel(app) {
    var self = this;

    self.addTextBlock = function () {
        toastr.info("Click!");
    }

    $(document).ready(function () {
        //$('[href="#profile"]').tab('show');
    });
}

app.attach({
    sourceName: 'master',
    factory: masterViewModel
});