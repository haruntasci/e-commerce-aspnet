
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using NETCore.Encrypt.Extensions;
using PusulaETicaret.Web.Entities;
using PusulaETicaret.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PusulaETicaret.Web.Helpers;
using System.Collections.Generic;

namespace PusulaETicaret.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly DatabaseContext _databaseContext;
        private readonly IHasher _hasher;


        public AccountController(DatabaseContext databaseContext, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _hasher = hasher;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = _hasher.DoMD5HashedString(model.Password);

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);

                if (user != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Fullname));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));


                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Products", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }

            }

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromServices]IMapper _mapper, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if(_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Bu kullanıcı adı kullanılıyor.");
                    return View(model);
                }


                var user = _mapper.Map<User>(model);

                string hashedPassword = _hasher.DoMD5HashedString(model.Password);

                user.Password = hashedPassword;

                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Profile()
        {
            ProfileInfoLoader();

            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.Find(userid);

            ViewData["Fullname"] = user.Fullname;

            ViewData["ProfileImage"] = user.ProfileImageFileName;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullname([Required(ErrorMessage = "Ad Soyad ekleyiniz.")][StringLength(50)] string fullname)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.Find(userid);    

                user.Fullname = fullname;

                _databaseContext.SaveChanges();
                ViewData["result"] = "NameChanged";
                //return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required(ErrorMessage = "Şifre giriniz.")][MinLength(6)][MaxLength(16)] string password)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.Find(userid);

                string hashedPassword = _hasher.DoMD5HashedString(password);

                user.Password = hashedPassword;

                _databaseContext.SaveChanges();

                ViewData["result"] = "PasswordChanged";

            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public IActionResult ProfileChangeImage([Required] IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

                //p_guid.jpg
                string fileName = $"p_{userid}.{file.ContentType.Split('/')[1]}";

                //string fileName = $"p_{userid}.{file.ContentType.Split('/')[1]}";
                Stream stream = new FileStream($"wwwroot/profilephotos/{fileName}", FileMode.OpenOrCreate);
                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                user.ProfileImageFileName = fileName;
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Profile));
            }
            ProfileInfoLoader();
            return View("Profile");
        }

    }
}
