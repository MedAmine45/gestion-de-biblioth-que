using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()

        {
            return View(repository.Books);
        }
        public ViewResult Edit(int BookId)
        {
            Book book = repository.Books.FirstOrDefault(b => b.BookId == BookId);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = book.Title + "has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }

        [HttpGet]
        public ActionResult Delete(int BookId)
        {
            Book deletedBook = repository.DeleteBook(BookId);
            if (deletedBook != null)
            {
                TempData["message"] = deletedBook.Title + "was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}