using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "Tekrar şifre giriniz.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter olabilir.")]
        [Compare(nameof(Password), ErrorMessage = "Şifre Tekrar ve Şifre eşleşmiyor.")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Ad-Soyad giriniz.")]
        [StringLength(50, ErrorMessage = "Ad-Soyad alanına en fazla 50 karakter girilebilir.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Lütfen bir rol seçiniz.")]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }

}
