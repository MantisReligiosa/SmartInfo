namespace Web.Models
{
    public class ChangeCreditsRequest
    {
        public string Password { get; set; }
        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}