using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class AdminDataProvider
    {
        BookEntities _db = new BookEntities();

        #region Admin
        public IEnumerable<User> GetAllAdmins()
        {
            return _db.Users.Where(e => e.IsAdmin == 1);
        }

        public bool ExistUser(User model)
        {
            return _db.Users.FirstOrDefault(e => e.Email == model.Email) == null ? false : true;
        }

        //public void add(User model)
        //{
        //    if (!ExistUser(model))
        //    {
        //        _db.Users.Add(model);
        //        _db.SaveChanges();
        //    }
        //}
        #endregion


        #region Condition
        public IEnumerable<BookCondition> GetAllCondition()
        {
            return _db.BookConditions;
        }
        #endregion


        #region DealType
        public IEnumerable<DealType> GetAllDealType()
        {
            return _db.DealTypes;
        }
        #endregion


        //წასაშლელი
        //#region User
        //public IEnumerable<User> GetAllUsers(string name, string surname, string email)
        //{
        //    return _db.Users.Where(e => e.IsAdmin == 0 && e.Name.Contains(name) && e.Surname.Contains(surname) && e.Email.Contains(email));
        //}
        //#endregion

        //#region Book
        //public Book GetBookById(int bookId)
        //{
        //    return _db.Books.FirstOrDefault(e => e.BookId == bookId);
        //}

        //public IEnumerable<Book> GetAllBooks(string bookName)
        //{
        //    return _db.Books.Where(e => e.Name.Contains(bookName));
        //}

        //public void AddBook(Book model)
        //{
        //    _db.Books.Add(model);
        //    _db.SaveChanges();
        //}

        //public void EditBook(CustomBook model)
        //{
        //    var result = GetBookById(model.BookId);

        //    result.Name = model.Name;
        //    result.AuthorId = model.AuthorId;
        //    result.GenreId = model.GenreId;
        //    result.DealTypeId = model.DealTypeId;
        //    result.ConditionId = model.ConditionId;

        //    _db.SaveChanges();
        //}

        //public void DeleteBook(Book model)
        //{
        //    var result = GetBookById(model.BookId);

        //    result.IsActive = 0;

        //    _db.SaveChanges();
        //}
        //#endregion


        //#region Author
        //public BookAuthor GetAuthorById(int authorId)
        //{
        //    return _db.BookAuthors.FirstOrDefault(e => e.AuthorId == authorId);
        //}

        //public IEnumerable<BookAuthor> GetAllAuthor(string name, string surname, string pseudonym)
        //{
        //    return _db.BookAuthors.Where(e => e.Name.Contains(name) && e.Surname.Contains(surname) && e.Pseudonym.Contains(pseudonym));
        //}

        //public void AddAuthor(BookAuthor model)
        //{
        //    _db.BookAuthors.Add(model);
        //    _db.SaveChanges();
        //}

        //public void EditAuthor(CustomAuthor model)
        //{
        //    var result = GetAuthorById(model.AuthorId);

        //    result.Name = model.Name;
        //    result.Surname = model.Surname;
        //    result.Pseudonym = model.Pseudonym;

        //    _db.SaveChanges();
        //}

        //public void DeleteAuthor(BookAuthor model)
        //{
        //    var result = GetAuthorById(model.AuthorId);
        //    var deletableBooks = _db.Books.Where(e => e.AuthorId == model.AuthorId);

        //    _db.Books.RemoveRange(deletableBooks);
        //    _db.BookAuthors.Remove(result);

        //    _db.SaveChanges();
        //}
        //#endregion


        //#region Genre
        //public BookGenre GetGenreById(int genreId)
        //{
        //    return _db.BookGenres.FirstOrDefault(e => e.GenreId == genreId);
        //}

        //public IEnumerable<BookGenre> GetAllGenre(string genreName)
        //{
        //    return _db.BookGenres.Where(e => e.Name.Contains(genreName));
        //}

        //public void AddGenre(BookGenre model)
        //{
        //    _db.BookGenres.Add(model);

        //    _db.SaveChanges();

        //}

        //public void EditGenre(CustomBookGenre model)
        //{
        //    var result = GetGenreById(model.GenreId);

        //    result.Name = model.Name;

        //    _db.SaveChanges();
        //}

        //public void DeleteGenre(BookGenre model)
        //{
        //    var result = GetGenreById(model.GenreId);
        //    var deletableBooks = _db.Books.Where(e => e.GenreId == model.GenreId);

        //    _db.Books.RemoveRange(deletableBooks);
        //    _db.BookGenres.Remove(result);

        //    _db.SaveChanges();
        //}
        //#endregion
    }
}