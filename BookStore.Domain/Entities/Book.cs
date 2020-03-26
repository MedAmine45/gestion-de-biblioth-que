using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BookId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public string Specialization { set; get; }

    }
}
