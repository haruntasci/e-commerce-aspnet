using System.ComponentModel.DataAnnotations;

namespace PusulaETicaret.Web.Models
{
    public class CreateUserViewModel : RegisterViewModel
    {
        public bool Locked { get; set; }
    }
}
