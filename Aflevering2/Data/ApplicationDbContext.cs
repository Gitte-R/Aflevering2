using Microsoft.EntityFrameworkCore;
using Smiley.Data.Models;

namespace Smiley.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Report> Reports { get; set; }
    }
}
