function ChangePasswordViewModel(master) {
    var self = this;

    self.password = ko.observable();
    self.passwordError = ko.observable('');
    self.newPassword = ko.observable();
    self.newPasswordError = ko.observable('');
    self.newPasswordConfirm = ko.observable();
    self.newPasswordConfirmError = ko.observable('');
    self.newLogin = ko.observable();
    self.newLoginError = ko.observable('');
}