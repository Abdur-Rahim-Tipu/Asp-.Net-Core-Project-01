using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookDetails.Controllers
{

    public class BooksController : Controller
    {
        private PublisherDbContext db;
        private IWebHostEnvironment env;
        public BooksController(PublisherDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var data = db.Books.ToList();
            return View(data);
        }
        public IActionResult PublisherWithBook(int id)
        {
            var data = db.Books.Where(p => p.PublisherId == id).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Publishers = db.Publishers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookInsertModel model)
        {

            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    BookId = model.BookId,
                    Title = model.Title,
                    PublishDate = model.PublishDate,
                    TotalPage = model.TotalPage,
                    CoverPrice = model.CoverPrice,
                    IsStock = model.IsStock,
                    PublisherId = model.PublisherId
                };
                string ext = Path.GetExtension(model.CoverPage.FileName);
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                FileStream file = new FileStream(Path.Combine(env.WebRootPath, "BookImages", filename), FileMode.Create);
                model.CoverPage.CopyTo(file);
                book.CoverPage = filename;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Publishers = db.Publishers.ToList();

            var data = db.Books.FirstOrDefault(x => x.BookId == id);
            if (data != null)
            {
                ViewBag.CoverPage = data.CoverPage;
                return View(new BookEditModel
                {
                    BookId = data.BookId,
                    Title = data.Title,
                    PublishDate = data.PublishDate,
                    TotalPage = data.TotalPage,
                    CoverPrice = data.CoverPrice,
                    IsStock = data.IsStock,
                    PublisherId = data.PublisherId
                });
            }


            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(BookEditModel model)
        {
            var data = db.Books.FirstOrDefault(x => x.BookId == model.BookId);
            if ( data != null)
            {
                data.Title = model.Title;
                data.PublishDate = model.PublishDate;
                data.TotalPage = model.TotalPage;
                data.CoverPrice = model.CoverPrice;
                data.PublisherId = model.PublisherId;
                data.IsStock = model.IsStock;
                if (model.CoverPage != null)
                {
                    string ext = Path.GetExtension(model.CoverPage.FileName);
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    FileStream file = new FileStream(Path.Combine(env.WebRootPath, "BookImages", filename), FileMode.Create);
                    model.CoverPage.CopyTo(file);
                    data.CoverPage = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            Book b = new Book { BookId = id };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { id = id });

        }
    }
}
