using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PusulaETicaret.Web.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string ProfileImageFileName { get; set; } = "no-image.png";

        [StringLength(30)]
        public string Role { get; set; } = "user";
    }
}
