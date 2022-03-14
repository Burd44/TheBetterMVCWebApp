using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class BooksController1 : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController1(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Books> books = _db.Books.ToList();
            return View(books);
        }
    }
}
