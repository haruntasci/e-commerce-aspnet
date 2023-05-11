using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Models
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public bool Locked { get; set; }


        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

    }
}
