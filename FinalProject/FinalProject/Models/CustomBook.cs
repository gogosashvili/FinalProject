using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class CustomBook
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int DealTypeId { get; set; }
        public int GenreId { get; set; }
        public int ConditionId { get; set; }
        public double IsActive { get; set; }
        public int UserId { get; set; }
    }
}