using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusulaETicaret.Web.Entities;
using PusulaETicaret.Web.Helpers;
using PusulaETicaret.Web.Models;

namespace PusulaETicaret.Web.Controllers
{
    [Authorize(Roles = "admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public ProductController(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _databaseContext.Products.ToList();
            List<ProductsViewModel> model =
              _databaseContext.Products.ToList()
                  .Select(x => _mapper.Map<ProductsViewModel>(x)).ToList();


            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(ProductsViewModel model)
        {
            if (ModelState.IsValid)
            {

                Product product = _mapper.Map<Product>(model);

                string fileName = model.File.FileName.ToString();
                Stream stream = new FileStream($"wwwroot/images/{fileName}", FileMode.OpenOrCreate);
                model.File.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                product.Image = fileName;
                _databaseContext.Products.Add(product);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = _databaseContext.Products.Find(id);

            if (product != null)
            {
                _databaseContext.Products.Remove(product);
                _databaseContext.SaveChanges();
            }


            return RedirectToAction(nameof(Index));

        }


    }
}
