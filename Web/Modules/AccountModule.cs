﻿using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using ServiceInterfaces;
using Web.Models;

namespace Web.Modules
{
    public class AccountModule : NancyModule
    {
        private readonly IAccountController _accountController;

        public AccountModule(IAccountController accountController)
            : base()
        {
            _accountController = accountController;

            Post["/api/changePassword"] = parameters =>
            {
                var data = this.Bind<ChangeCreditsRequest>();
                var user = _accountController.GetUserByName(Context.CurrentUser.UserName);
                var responce = new ChangeCreditsResponce { Ok = true };

                //ToDo: перенести это в логику AccountController
                if (user == null || !_accountController.IsPasswordCorrect(user, data.Password))
                {
                    responce.Ok = false;
                    responce.PasswordError = "Неверный пароль";
                }

                if (!_accountController.IsNewLoginValid(data.NewLogin))
                {
                    responce.Ok = false;
                    responce.NewLoginError = "Недопустимый логин";
                }

                if (!_accountController.IsNewPasswordValid(data.NewPassword))
                {
                    responce.Ok = false;
                    responce.NewPasswordError = "Недопустимый пароль";
                }

                if (data.NewPassword != data.NewPasswordConfirm)
                {
                    responce.Ok = false;
                    responce.NewPasswordConfirmError = "Пароли не совпадают";
                }

                if (!_accountController.IsNewPasswordValid(data.NewPasswordConfirm))
                {
                    responce.Ok = false;
                    if (!string.IsNullOrEmpty(responce.NewPasswordConfirmError))
                    {
                        responce.NewPasswordConfirmError += ' ';
                    }
                    responce.NewPasswordConfirmError += "Недопустимый пароль";
                }

                if (responce.Ok)
                {
                    _accountController.ChangePassword(user.GUID, data.NewLogin, data.NewPassword);
                    this.Logout("/");
                }
                return Response.AsJson(responce);
            };
        }
    }
}