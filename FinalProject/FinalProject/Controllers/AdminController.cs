using FinalProject.Helper;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FinalProject.Controllers
{
    public class AdminController : Controller
    {
        AccountDataProvider accountDataProvider = new AccountDataProvider();
        GenreDataProvider genreDataProvider = new GenreDataProvider();
        AuthorDataProvider authorDataProvider = new AuthorDataProvider();
        AdminDataProvider adminDataProvider = new AdminDataProvider();
        BookDataProvider bookdataProvider = new BookDataProvider();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Admin
        public ActionResult AdminList()
        {
            return View(adminDataProvider.GetAllAdmins());
        }

        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //adminDataProvider.add(new User()
                accountDataProvider.add(new Models.User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    //Password = SHA.GenerateSHA12Stirng(model.Password),
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    IsAdmin = 1,
                    IsActive = 1
                });
            }

            return RedirectToAction("AdminList");
        }

        public ActionResult EditAdmin(int userId)
        {
            var result = accountDataProvider.GetUserById(userId);

            var cutomUser = new RegisterViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                Email = result.Email,
                Password = result.Password,
                PhoneNumber = result.PhoneNumber,
                ConfirmPassword = result.Password
            };

            return View(cutomUser);
        }

        [HttpPost]
        public ActionResult EditAdmin(RegisterViewModel model)
        {
            accountDataProvider.EditAccount(model);

            return RedirectToAction("AdminList");
        }

        public ActionResult DeleteAdmin(int userId)
        {
            return View(accountDataProvider.GetUserById(userId));
        }

        //არმუშაობს
        [HttpPost]
        public ActionResult DeleteAdmin(User model)
        {
            accountDataProvider.Delete(model);

            return RedirectToAction("AdminList");
        }
        #endregion


        #region User

        public ActionResult UserList(string name = "", string surname = "", string mail = "", int page = 1)
        {
            ViewBag.Name = name;
            ViewBag.Surname = surname;
            ViewBag.Email = mail;
            var result = accountDataProvider.GetAllUsers(name, surname, mail).ToList();

            return View(result.ToPagedList(page, 5));
        }

        public ActionResult SortUserByEmail(string sortByEmail, int page = 1,
                                        string searchEmail = "", bool fromPage = false)
        {
            //ViewBag.IsEmail = true;

            var email = "";
            if (!fromPage)
            {
                email = sortByEmail == null || sortByEmail == "orderBy" ? "desc" : "orderBy";
                ViewBag.SortByEmail = email;
            }
            else
            {
                email = sortByEmail == null || sortByEmail == "orderBy" ? "orderBy" : "desc";
            }

            var result = accountDataProvider.GetAllUsers("", "", "");
            switch (email)
            {
                case "desc":
                    result = accountDataProvider.GetAllUsers("", "", "")
                             .Where(e => e.Email.Contains(searchEmail))
                             .OrderByDescending(e => e.Email).ToList();
                    break;

                default:
                    result = accountDataProvider.GetAllUsers("", "", "")
                              .Where(e => e.Email.Contains(searchEmail))
                              .OrderBy(e => e.Email).ToList();
                    break;
            }

            return View("UserList", result.ToPagedList(page, 5));
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                accountDataProvider.add(new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    //Password = SHA.GenerateSHA12Stirng(model.Password),
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    IsAdmin = 0,
                    IsActive = 1
                });
            }

            return RedirectToAction("UserList");
        }

        public ActionResult EditUser(int userId)
        {
            var result = accountDataProvider.GetUserById(userId);

            var cutomUser = new RegisterViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                Email = result.Email,
                Password = result.Password,
                PhoneNumber = result.PhoneNumber,
                ConfirmPassword = result.Password
            };

            return View(cutomUser);
        }

        [HttpPost]
        public ActionResult EditUser(RegisterViewModel model)
        {
            accountDataProvider.EditAccount(model);

            return RedirectToAction("UserList");
        }


        //არ მუშაობს :( 
        public ActionResult DeleteUser(int userId)
        {
            var result = accountDataProvider.GetUserById(userId);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeleteUser(User model)
        {
            accountDataProvider.Delete(model);

            return RedirectToAction("UserList");
        }
        #endregion


        #region Book
        public ActionResult BookList(string name = "", int page = 1)
        {
            ViewBag.Name = name;
            var result = bookdataProvider.GetAllBooks(name).ToList();

            return View(result.ToPagedList(page, 5));
        }

        public ActionResult SortByBookName(string sortByName, int page = 1,
                                           string searchName = "", bool fromPage = false)
        {
            //ViewBag.IsName = true;

            var name = "";
            if (!fromPage)
            {
                name = sortByName == null || sortByName == "orderBy" ? "desc" : "orderBy";
                ViewBag.SortByName = name;
            }
            else
            {
                name = sortByName == null || sortByName == "orderBy" ? "orderBy" : "desc";
            }

            var result = bookdataProvider.GetAllBooks("");
            switch (name)
            {
                case "desc":
                    result = bookdataProvider.GetAllBooks("")
                             .Where(e => e.Name.Contains(searchName))
                             .OrderByDescending(e => e.Name).ToList();
                    break;

                default:
                    result = bookdataProvider.GetAllBooks("")
                              .Where(e => e.Name.Contains(searchName))
                              .OrderBy(e => e.Name).ToList();
                    break;
            }

            return View("BookList", result.ToPagedList(page, 5));
        }

        //არვ მუშაობს :(
        public JsonResult GetBookByName(string term)
        {
            var r = bookdataProvider.GetAllBooks(term).Select(e => e.Name);
            return Json(r, JsonRequestBehavior.AllowGet);
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
            bookdataProvider.AddBook(new Book
            {
                Name = model.Name,
                AuthorId = model.AuthorId,
                ConditionId = model.ConditionId,
                GenreId = model.GenreId,
                DealTypeId = model.DealTypeId,
                IsActive = 1,
                UserId = 1 /*LoginHelper.CurrentUser().Id*/   //არ მუშაობს :(
            });
            return RedirectToAction("BookList");
        }

        public ActionResult EditBook(int bookId)
        {
            var result = bookdataProvider.GetBookById(bookId);

            var authors = authorDataProvider.GetAllAuthor("", "", "").ToList();
            ViewBag.AuthorId = new SelectList(authors, "AuthorId", "Name", result.AuthorId); //დეფოულტ მნიშვნელობა არ ჯდება

            var conditions = adminDataProvider.GetAllCondition().ToList();
            ViewBag.ConditionId = new SelectList(conditions, "ConditionId", "Name", result.ConditionId); //დეფოულტ მნიშვნელობა არ ჯდება

            var genres = genreDataProvider.GetAllGenre("").ToList();
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", result.GenreId); //დეფოულტ მნიშვნელობა არ ჯდება

            var dealTypes = adminDataProvider.GetAllDealType().ToList();
            ViewBag.DealTypeId = new SelectList(dealTypes, "DealTypeId", "Name", result.DealTypeId); //დეფოულტ მნიშვნელობა არ ჯდება

            var customBook = new CustomBook()
            {
                Name = result.Name,
                AuthorId = result.AuthorId,
                ConditionId = result.ConditionId,
                GenreId = result.GenreId,
                DealTypeId = result.DealTypeId,
                IsActive = result.IsActive,
                UserId = result.UserId
            };
            return View(customBook);
        }

        [HttpPost]
        public ActionResult EditBook(CustomBook model)
        {
            bookdataProvider.EditBook(model);

            return RedirectToAction("BookList");
        }

        public ActionResult DeleteBook(int bookId)
        {
            return View(bookdataProvider.GetBookById(bookId));
        }

        [HttpPost]
        public ActionResult DeleteBook(Book model)
        {
            bookdataProvider.DeleteBook(model);

            return RedirectToAction("BookList");
        }
        #endregion


        #region Author
        public ActionResult AuthorList(string name = "", string surname = "", string pseudonym = "", int page = 1)
        {
            ViewBag.Name = name;
            ViewBag.Surname = surname;
            ViewBag.Pseudonym = pseudonym;
            var result = authorDataProvider.GetAllAuthor(name, surname, pseudonym).ToList();

            return View(result.ToPagedList(page, 5));
        }

        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAuthor(CustomAuthor model)
        {
            authorDataProvider.AddAuthor(new BookAuthor
            {
                Name = model.Name,
                Surname = model.Surname,
                Pseudonym = model.Pseudonym
            });
            return RedirectToAction("AuthorList");
        }

        public ActionResult EditAuthor(int authorId)
        {
            return View(authorDataProvider.GetAuthorById(authorId));
        }

        [HttpPost]
        public ActionResult EditAuthor(CustomAuthor model)
        {
            authorDataProvider.EditAuthor(model);

            return RedirectToAction("AuthorList");
        }

        public ActionResult DeleteAuthor(int authorId)
        {
            return View(authorDataProvider.GetAuthorById(authorId));
        }

        [HttpPost]
        public ActionResult DeleteAuthor(BookAuthor model)
        {
            authorDataProvider.DeleteAuthor(model);

            return RedirectToAction("AuthorList");
        }
        #endregion


        #region Genre
        public ActionResult GenreList(string name = "", int page = 1)
        {
            ViewBag.Name = name;
            var result = genreDataProvider.GetAllGenre(name).ToList();

            return View(result.ToPagedList(page, 5));
        }

        public ActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGenre(CustomBookGenre model)
        {
            genreDataProvider.AddGenre(new BookGenre
            {
                Name = model.Name
            });

            return RedirectToAction("GenreList");
        }

        public ActionResult EditGenre(int genreId)
        {
            return View(genreDataProvider.GetGenreById(genreId));
        }

        [HttpPost]
        public ActionResult EditGenre(CustomBookGenre model)
        {
            genreDataProvider.EditGenre(model);

            return RedirectToAction("genreList");
        }

        public ActionResult DeleteGenre(int genreId)
        {
            return View(genreDataProvider.GetGenreById(genreId));
        }

        [HttpPost]
        public ActionResult DeleteGenre(BookGenre model)
        {
            genreDataProvider.DeleteGenre(model);

            return RedirectToAction("GenreList");
        }
        #endregion
    }
}