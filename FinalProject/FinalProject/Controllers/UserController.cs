using FinalProject.Helper;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        AccountDataProvider accountDataProvider = new AccountDataProvider();
        GenreDataProvider genreDataProvider = new GenreDataProvider();
        AuthorDataProvider authorDataProvider = new AuthorDataProvider();
        AdminDataProvider adminDataProvider = new AdminDataProvider();
        BookDataProvider bookDataProvider = new BookDataProvider();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        #region Book
        public ActionResult InActiveBookList()
        {
            var userId = LoginHelper.CurrentUser().Id; 
            var result = bookDataProvider.GetUserBooks(userId).Where(e => e.IsActive == 0);

            return View(result);
        }

        public ActionResult ActiveBookList()
        {
             var userId = LoginHelper.CurrentUser().Id;
            var result = bookDataProvider.GetUserBooks(userId).Where(e => e.IsActive == 1);

            return View(result);
        }

        public ActionResult BookDetails(int bookId)
        {
            return View(bookDataProvider.GetBookById(bookId));
        }

        public ActionResult CreateBook()
        {
            var authors = authorDataProvider.GetAllAuthor("", "", "").ToList();
            authors.Insert(0, new BookAuthor() { AuthorId = 0, Name = "ავტორი" });
            ViewBag.AuthorId = new SelectList(authors, "AuthorId", "Name");

            var conditions = adminDataProvider.GetAllCondition().ToList();
            conditions.Insert(0, new BookCondition() { ConditionId = 0, Name = "გარიგების ტიპი" });
            ViewBag.ConditionId = new SelectList(conditions, "ConditionId", "Name");

            var genres = genreDataProvider.GetAllGenre("").ToList();
            genres.Insert(0, new BookGenre() { GenreId = 0, Name = "ჟანრი" });
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name");

            var dealTypes = adminDataProvider.GetAllDealType().ToList();
            dealTypes.Insert(0, new DealType() { DealTypeId = 0, Name = "მდგომარეობა" });
            ViewBag.DealTypeId = new SelectList(dealTypes, "DealTypeId", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(CustomBook model)
        {
            bookDataProvider.AddBook(new Book
            {
                Name = model.Name,
                AuthorId = model.AuthorId,
                ConditionId = model.ConditionId,
                GenreId = model.GenreId,
                DealTypeId = model.DealTypeId,
                IsActive = 1,
                UserId = LoginHelper.CurrentUser().Id,
                Price = model.Price
            });
            return RedirectToAction("BookList");
        }
        #endregion
    }
}