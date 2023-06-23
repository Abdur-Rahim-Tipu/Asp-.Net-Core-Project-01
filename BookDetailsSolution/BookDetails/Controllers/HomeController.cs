using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookDetails.Controllers
{
    public class HomeController : Controller
    {
        private PublisherDbContext db;
        private IWebHostEnvironment env;
        public HomeController(PublisherDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult<IEnumerable<ListView>>> Dashboard()
        {
           
            ViewBag.Orders = db.Orders.Count();
            ViewBag.Publishers = db.Publishers.Count();
            ViewBag.Books = db.Books.Count();
            ViewBag.Authors = db.Authors.Count(); 
            ViewBag.Orders = db.Orders.Count();
            var data = await db.Authors.Select(c =>

              new ListView
              {
                  PublisherName = c.Book == null ? "" : c.Book.Publisher == null ? "" : c.Book.Publisher.PublisherName,
                  Title = c.Book == null ? "" : c.Book.Title,
                  AuthorName = c.AuthorName,
                  CoverPage = c.Book == null ? "" : c.Book.CoverPage,
                  Contract = c.Phone,
                  AuthorPic = c.Picture == null ? "" : c.Picture
              }).ToListAsync();
            return View(data);
        }
        public async Task<ActionResult<IEnumerable<ListView>>> Products()
        {
            
            var data = await db.Authors.Select( c =>

               new ListView
               {
                   PublisherName = c.Book == null ? "": c.Book.Publisher == null ? "" : c.Book.Publisher.PublisherName,
                   Title = c.Book == null ? "" : c.Book.Title,
                   AuthorName = c.AuthorName,
                   CoverPage = c.Book == null ? "" : c.Book.CoverPage,
                   PublishDate = c.Book == null ? new DateTime() : c.Book.PublishDate,
                   Price = c.Book == null ? 0 : c.Book.CoverPrice,
               }).ToListAsync();
            return View(data);
        }
        public async Task<ActionResult<IEnumerable<ListView>>> AuthorList()
        {

            var data = await db.Authors.Select(c =>

               new ListView
               {

                   AuthorName = c.AuthorName,
                   Contract = c.Phone,
                   AuthorPic = c.Picture == null ? "" : c.Picture
               }).ToListAsync();
            return View(data);
        }

    }
}