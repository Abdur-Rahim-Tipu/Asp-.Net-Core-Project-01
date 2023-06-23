using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookDetails.Controllers
{
    public class AuthorsController : Controller
    {
        private PublisherDbContext db;
        private IWebHostEnvironment env;
        public AuthorsController(PublisherDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var data = db.Authors.ToList();
            return View(data);
        }
        public IActionResult BookWithAuthor(int id)
        {
            var data = db.Authors.Where(p => p.BookId == id).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.books = db.Books.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(AuthorInsertModel model)
        {

            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    AuthorId = model.AuthorId,
                    AuthorName = model.AuthorName,
                    BirthDate = model.BirthDate,
                    WebsiteUrl = model.WebsiteUrl,
                    Phone = model.Phone,
                    Email = model.Email,
                    BookId = model.BookId
                };
                string ext = Path.GetExtension(model.Picture.FileName);
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                FileStream file = new FileStream(Path.Combine(env.WebRootPath, "AuthorImages", filename), FileMode.Create);
                model.Picture.CopyTo(file);
                author.Picture = filename;
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.books = db.Books.ToList();

            var data = db.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (data != null)
            {
                ViewBag.Picture = data.Picture;
                return View(new AuthorEditModel
                {
                    AuthorId = data.AuthorId,
                    BookId = data.BookId,
                    AuthorName = data.AuthorName,
                    BirthDate = data.BirthDate,
                    WebsiteUrl = data.WebsiteUrl,
                    Phone = data.Phone,
                    Email = data.Email
                });
            }


            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(AuthorInsertModel model)
        {
            var data = db.Authors.FirstOrDefault(x => x.AuthorId == model.AuthorId);
            if (data != null)
            {
                data.AuthorName = model.AuthorName;
                data.BirthDate = model.BirthDate;
                data.WebsiteUrl = model.WebsiteUrl;
                data.Phone = model.Phone;
                data.Email = model.Email;
                data.BookId = model.BookId;
                if (model.Picture != null)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    FileStream file = new FileStream(Path.Combine(env.WebRootPath, "AuthorImages", filename), FileMode.Create);
                    model.Picture.CopyTo(file);
                    data.Picture = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            Author a = new Author { AuthorId = id };
            db.Entry(a).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(new { id = id });

        }
    }
}
