using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookDetails.Controllers
{
    public class OrdersController : Controller
    {
        private PublisherDbContext db;
        public OrdersController(PublisherDbContext db)
        {
            this.db = db;

        }
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> Index()
        {
            var data = await db.Orders
                .Select(c =>
               new OrderViewModel
               {
                   //OrderId = c.OrderId,
                   //CustomerName = c.CustomerName,
                   //OrderDate = c.OrderDate,
                   //Price = c.Book == null ? 0 : c.Book.CoverPrice,
                   //DiscountRate = c.Discount,
                   //Quantity = c.Quantity,
                   //TotalPrice = Math.Floor(((c.Book == null ? 0 : c.Book.CoverPrice) * c.Quantity)),
                   //DiscountAmount = Math.Floor(((c.Book == null ? 0 : c.Book.CoverPrice) * c.Quantity) * (c.Discount / 100)),
                   //NetPay = Math.Floor(((c.Book == null ? 0 : c.Book.CoverPrice) * c.Quantity) - (c.Price * c.Quantity) * (c.Discount / 100)),
                   //BookTitle = c.Book == null ? "" : c.Book.Title
                   OrderId = c.OrderId,
                   CustomerName = c.CustomerName,
                   OrderDate = c.OrderDate,
                   Price = c.Price,
                   DiscountRate = c.Discount,
                   Quantity = c.Quantity,
                   TotalPrice = Math.Floor(c.Price * c.Quantity),
                   DiscountAmount = Math.Floor(c.Price * c.Quantity) * (c.Discount / 100),
                   NetPay = Math.Floor((c.Price * c.Quantity) - (c.Price * c.Quantity) * (c.Discount / 100)),
                   BookTitle = c.Book == null ? "" : c.Book.Title

               })
                .ToListAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.books = db.Books.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
        public IActionResult Edit(int id)
        {
            var data = db.Orders.FirstOrDefault(o=>o.OrderId == id);
            ViewBag.books = db.Books.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, Order order)
        {
            if(ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
        public IActionResult Delete(int id)
        {
            Order o = new Order { OrderId = id };
            db.Entry(o).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { id = id });

        }
    }
}
