using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı giriniz.")]
        [StringLength(30, ErrorMessage = "Kullanıcı adı en fazla 30 karakter olabilir.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre giriniz.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter olabilir.")]
        public string Password { get; set; }
    }

}
