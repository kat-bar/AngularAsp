using AngularAsp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAsp.Controllers
{
		[ApiController]
		[Route("api/products")]
	public class ProductController : Controller
	{
        ApplicationContext db;
        public ProductController(ApplicationContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Products.Add(new Product { Name = "451° по Фаренгейту", Company = "Рэй Брэдбери", Price = 1953 });
                db.Products.Add(new Product { Name = "1984", Company = "Джордж Оруэлл", Price = 1948 });
                db.Products.Add(new Product { Name = "Мастер и Маргарита", Company = "Михаил Булгаков", Price = 1940 });
                db.Products.Add(new Product { Name = "Шантарам", Company = "Грегори Дэвид Робертс", Price = 2003 });
                db.Products.Add(new Product { Name = "Три товарища", Company = "Эрих Мария Ремарк", Price = 1936 });
                db.Products.Add(new Product { Name = "Цветы для Элджернона", Company = "Дэниел Киз", Price = 1959 });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.SaveChanges();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}
