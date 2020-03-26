namespace BookStore.Domain.Concrete
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BookStore.Domain.Entities;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }
        public DbSet<Book> Books { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
