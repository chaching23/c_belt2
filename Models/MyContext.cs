using Microsoft.EntityFrameworkCore;
 
namespace c_belt.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions  options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Hobby> Hobbys {get; set;}
        public DbSet<Join> Joins {get; set;}


    }
}


