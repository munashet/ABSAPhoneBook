using System.Data.Entity;

namespace PhoneBook.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("Name=ConnectionString")
        {
           
        }

        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}