$(function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    moment.locale('ru');

    $(document).ready(function () {
        ko.bindingHandlers.htmlBound = {
            init: function () {
                return { controlsDescendantBindings: true };
            },
            update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                ko.utils.setHtml(element, valueAccessor());
                ko.applyBindingsToDescendants(bindingContext, element);
            }
        };

        ko.applyBindings(app);
    });
});