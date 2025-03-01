using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Book> Book { get; set; } = default!;
        public DbSet<WebApp.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<WebApp.Models.Author> Author { get; set; } = default!;
        public DbSet<WebApp.Models.Category> Category { get; set; } = default!;
        public DbSet<WebApp.Models.Member> Member { get; set; } = default!;
        public DbSet<WebApp.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
