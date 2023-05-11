using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PusulaETicaret.Web.Entities;
using PusulaETicaret.Web.Models;
using System.Diagnostics;

namespace PusulaETicaret.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Products([FromServices] DatabaseContext _databaseContext)
        {
            var products = _databaseContext.Products.ToList();
            List<ProductsViewModel> model =
              _databaseContext.Products.ToList()
                  .Select(x => _mapper.Map<ProductsViewModel>(x)).ToList();


            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();  
        }

      
    }
}