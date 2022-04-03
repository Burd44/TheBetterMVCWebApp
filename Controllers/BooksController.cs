using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Books> books = _db.Books.ToList();
            return View(books);
        }
        //Getter
        public IActionResult Create()
        { 
            return View();
        }

        //Poster
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Books obj)
        {
            if(obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("name", "Name can't match Description"); 
            }
            if (ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Book successfully added!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Getter
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDB = _db.Books.Find(id);

            if(BookFromDB == null)
            { 
                return NotFound();
            }

            return View(BookFromDB);
        }

        //Poster
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Books obj)
        {
            if (obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("name", "Name can't match Description");
            }
            if (ModelState.IsValid)
            {
                _db.Books.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Book successfully edited!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Getter
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDB = _db.Books.Find(id);

            if (BookFromDB == null)
            {
                return NotFound();
            }

            return View(BookFromDB);
        }

        //Poster
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Books.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            _db.Books.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Book successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
