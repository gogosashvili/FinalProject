using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class GenreDataProvider
    {
        BookEntities _db = new BookEntities();

        public BookGenre GetGenreById(int genreId)
        {
            return _db.BookGenres.FirstOrDefault(e => e.GenreId == genreId);
        }

        public IEnumerable<BookGenre> GetAllGenre(string genreName)
        {
            return _db.BookGenres.Where(e => e.Name.Contains(genreName));
        }

        public void AddGenre(BookGenre model)
        {
            _db.BookGenres.Add(model);

            _db.SaveChanges();

        }

        public void EditGenre(CustomBookGenre model)
        {
            var result = GetGenreById(model.GenreId);

            result.Name = model.Name;

            _db.SaveChanges();
        }

        public void DeleteGenre(BookGenre model)
        {
            var result = GetGenreById(model.GenreId);
            var deletableBooks = _db.Books.Where(e => e.GenreId == model.GenreId);

            _db.Books.RemoveRange(deletableBooks);
            _db.BookGenres.Remove(result);

            _db.SaveChanges();
        }
    }
}