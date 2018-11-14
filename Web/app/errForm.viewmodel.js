function errFormViewModel() {
    var self = this;

    restart = function () {
        window.location.href = '/';
    };
}

app.attach({
    sourceName: 'errForm',
    factory: errFormViewModel
});