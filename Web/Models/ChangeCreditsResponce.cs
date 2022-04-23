namespace Web.Models
{
    public class ChangeCreditsResponce
    {
        public bool Ok { get; internal set; }
        public string PasswordError { get; internal set; }
        public string NewLoginError { get; internal set; }
        public string NewPasswordError { get; internal set; }
        public string NewPasswordConfirmError { get; internal set; }
    }
}