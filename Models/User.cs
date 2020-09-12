using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    public class User :BaseEntity  {

        [Column("name")]
        public string Name { get; set; }

        
      

        [Column("surname")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("profileimageurl")]
        public string ProfileImageUrl { get; set; }

        [Column("password")]
        public string Password { get; set; }
        [Column("unlimited")]
        public bool Unlimited { get; set; }=false;

        [Column("rightids")]
        public int[] RightIds { get; set; }

    }
}