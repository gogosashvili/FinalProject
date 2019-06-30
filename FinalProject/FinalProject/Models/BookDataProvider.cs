using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class BookDataProvider
    {
        BookEntities _db = new BookEntities();

        public Book GetBookById(int bookId)
        {
            return _db.Books.FirstOrDefault(e => e.BookId == bookId);
        }

        public IEnumerable<Book> GetAllBooks(string bookName)
        {
            return _db.Books.Where(e => e.Name.Contains(bookName));
        }

        public IEnumerable<Book> GetUserBooks(int userId)
        {
            return _db.Books.Where(e => e.UserId == userId);
        }

        public void AddBook(Book model)
        {
            _db.Books.Add(model);
            _db.SaveChanges();
        }

        public void EditBook(CustomBook model)
        {
            var result = GetBookById(model.BookId);

            result.Name = model.Name;
            result.AuthorId = model.AuthorId;
            result.GenreId = model.GenreId;
            result.DealTypeId = model.DealTypeId;
            result.ConditionId = model.ConditionId;
            result.UserId = model.UserId;
            result.Price = model.Price;

            _db.SaveChanges();
        }

        public void DeleteBook(Book model)
        {
            var result = GetBookById(model.BookId);

            result.IsActive = 0;

            _db.SaveChanges();
        }
    }
}