using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Book book, int quantity = 1)
        {
            CartLine line = lineCollection
                                    .Where(b => b.Book.BookId == book.BookId)
                                    .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                { Book = book, Quantity = quantity });
            }
            else
                line.Quantity += quantity;
        }
        public void RemoveLine(Book book)
        {
            lineCollection.RemoveAll(b => b.Book.BookId == book.BookId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(cl => cl.Book.Price * cl.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
       
    }//end of Cart Class
    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { set; get; }
    }//end of cartLine

}
