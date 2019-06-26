using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class AuthorDataProvider
    {
        BookEntities _db = new BookEntities();

        public BookAuthor GetAuthorById(int authorId)
        {
            return _db.BookAuthors.FirstOrDefault(e => e.AuthorId == authorId);
        }

        public IEnumerable<BookAuthor> GetAllAuthor(string name, string surname, string pseudonym)
        {
            return _db.BookAuthors.Where(e => e.Name.Contains(name) && e.Surname.Contains(surname) && e.Pseudonym.Contains(pseudonym));
        }

        public void AddAuthor(BookAuthor model)
        {
            _db.BookAuthors.Add(model);
            _db.SaveChanges();
        }

        public void EditAuthor(CustomAuthor model)
        {
            var result = GetAuthorById(model.AuthorId);

            result.Name = model.Name;
            result.Surname = model.Surname;
            result.Pseudonym = model.Pseudonym;

            _db.SaveChanges();
        }

        public void DeleteAuthor(BookAuthor model)
        {
            var result = GetAuthorById(model.AuthorId);
            var deletableBooks = _db.Books.Where(e => e.AuthorId == model.AuthorId);

            _db.Books.RemoveRange(deletableBooks);
            _db.BookAuthors.Remove(result);

            _db.SaveChanges();
        }
    }
}