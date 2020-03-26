using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { set; get; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentSpecilization { set; get; }
    }
}