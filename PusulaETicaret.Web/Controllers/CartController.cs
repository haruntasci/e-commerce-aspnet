using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusulaETicaret.Web.Entities;
using PusulaETicaret.Web.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PusulaETicaret.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CartController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Add(int id)
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var product = _databaseContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var item = _databaseContext.Items.Where(x=> x.UserId == userid && x.ProductId == id).FirstOrDefault();

            if(item == null)
            {
                Item newItem = new()
                {
                    UserId = userid,
                    ProductId = id,
                    Quantity = 1,
           
                    
                };
                _databaseContext.Items.Add(newItem);
            }
            else
            {
                item.Quantity++;

            }
            _databaseContext.SaveChanges();

            return RedirectToAction("Products","Home");
        }

        public IActionResult MyCart([FromServices] IMapper _mapper)
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var items = _databaseContext.Items.Where(x => x.UserId == userid).ToList();
            

            List<ItemsViewModel> products = items.Select(x => _mapper.Map<ItemsViewModel>(x)).ToList();

            foreach(var item in products)
            {
                item.ProductName = _databaseContext.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault().ProductName;
                item.ProductPrice = _databaseContext.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault().ProductPrice;
                item.Image = _databaseContext.Products.Where(x => x.ProductId == item.ProductId).FirstOrDefault().Image;

            }


            return View(products);
        }

        public IActionResult Increase(int id)
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var product = _databaseContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var item = _databaseContext.Items.Where(x => x.UserId == userid && x.ProductId == id).FirstOrDefault();

            item.Quantity++;

            _databaseContext.SaveChanges();

            return RedirectToAction("MyCart", "Cart");
        }

        public IActionResult Decrease(int id)
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var product = _databaseContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var item = _databaseContext.Items.Where(x => x.UserId == userid && x.ProductId == id).FirstOrDefault();

            if(item.Quantity > 0)
            {
                item.Quantity--;

                if(item.Quantity == 0)
                {
                    _databaseContext.Items.Remove(item);
                }

                _databaseContext.SaveChanges();
            }

           
            return RedirectToAction("MyCart", "Cart");

        }
        public IActionResult Remove(int id) 
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var product = _databaseContext.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var item = _databaseContext.Items.Where(x => x.UserId == userid && x.ProductId == id).FirstOrDefault();

            _databaseContext.Items.Remove(item);

            _databaseContext.SaveChanges();

            return RedirectToAction("MyCart", "Cart");
        }

    }
}
