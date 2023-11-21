using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Grundlagen.Models
{
    public class User
    {
        public string Name { get; set; }

        [Key]
        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public String Password { get; set; }


        [NotMapped]
        public String PasswordRetype { get; set; }

    }
}
