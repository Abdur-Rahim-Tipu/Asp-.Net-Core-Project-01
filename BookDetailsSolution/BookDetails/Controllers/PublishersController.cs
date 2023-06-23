using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookDetails.Controllers
{
    public class PublishersController : Controller
    {
        private PublisherDbContext db;
        private IWebHostEnvironment env;
        public PublishersController(PublisherDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            var data = db.Publishers.ToList();
            return View(data);
        }
        public IActionResult Create()
        {

            Publisher publisher = new Publisher();
            publisher.Books.Add(new Book());

                foreach(var book2 in publisher.Books)
                {
                    book2.Authors.Add(new Author());
                }

            return View(publisher);
        }
        [HttpPost]
        public IActionResult Create(Publisher publisher)
        { 
            if (publisher != null)
            {
                foreach (var book in publisher.Books)
                {
                    if(book.CoverImage != null)
                    {
                        string ext = Path.GetExtension(book.CoverImage.FileName);
                        string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        FileStream file = new FileStream(Path.Combine(env.WebRootPath, "BookImages", filename), FileMode.Create);
                        book.CoverImage.CopyTo(file);
                        book.CoverPage = filename;
                    }
                    else
                    {
                        book.CoverPage = "";
                    }
                    foreach (var author in book.Authors)
                    {
                        if(author.AuthorImage.Length > 0)
                        {
                            string Aext = Path.GetExtension(author.AuthorImage.FileName);
                            string Afilename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Aext;
                            FileStream Afile = new FileStream(Path.Combine(env.WebRootPath, "AuthorImages", Afilename), FileMode.Create);
                            author.AuthorImage.CopyTo(Afile);
                            author.Picture = Afilename;
                        }
                        else
                        {
                            author.Picture = "";
                        }
                    }
                }
                try
                {
                    db.Database.BeginTransaction();
                    db.Publishers.Add(publisher);
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                }
                return RedirectToAction("Index");
            }


            return BadRequest();
            }
        public IActionResult Edit(int id)
        {

            var data = db.Publishers.Include(x => x.Books).ThenInclude(a=>a.Authors).FirstOrDefault(x => x.PublisherId == id);
            if (data != null)
            {
                ViewBag.Bookpic = data.Books == null ? "" : data.Books[data.Books.Count - 1].CoverPage;
            }
            
           
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            if (publisher != null)
            {
                foreach (var book in publisher.Books)
                {
                    if (book.CoverImage != null)
                    {
                        string ext = Path.GetExtension(book.CoverImage.FileName);
                        string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        FileStream file = new FileStream(Path.Combine(env.WebRootPath, "BookImages", filename), FileMode.Create);
                        book.CoverImage.CopyTo(file);
                        book.CoverPage = filename;
                    }
                    foreach (var author in book.Authors)
                    {
                        if (author.AuthorImage != null)
                        {
                            string Aext = Path.GetExtension(author.AuthorImage.FileName);
                            string Afilename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Aext;
                            FileStream Afile = new FileStream(Path.Combine(env.WebRootPath, "AuthorImages", Afilename), FileMode.Create);
                            author.AuthorImage.CopyTo(Afile);
                            author.Picture = Afilename;
                        }
                    }
                }
                try
                {
                    db.Entry(publisher).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                }
                return RedirectToAction("Index");
            }


            return BadRequest();
        }

        public IActionResult Delete(int id)
        {
            Publisher p = new Publisher { PublisherId = id };
            db.Entry(p).State = EntityState.Deleted;
             db.SaveChanges();
            return Json(new { id = id });

        }
    }
}
