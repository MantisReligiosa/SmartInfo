using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    [Table("Users")]
    public class UserEntity : Entity
    {
        [Column("Login")]
        public string Login { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("GUID")]
        public string GuidStr { get; set; }
    }
}
