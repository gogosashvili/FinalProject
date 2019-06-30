using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class MainPageDataProvider
    {
        BookEntities _db = new BookEntities();

        public IEnumerable<Book> GetFreeBooks()
        {
            return _db.Books.Where(e => e.DealTypeId == (int)BookDealTypeEnum.free && e.IsActive == 1);
        }

        public IEnumerable<Book> GetSwapBooks()
        {
            return _db.Books.Where(e => e.DealTypeId == (int)BookDealTypeEnum.swap && e.IsActive == 1);
        }

        public IEnumerable<Book> GetPaidBooks()
        {
            return _db.Books.Where(e => e.DealTypeId == (int)BookDealTypeEnum.paid && e.IsActive == 1);
        }
    }
}