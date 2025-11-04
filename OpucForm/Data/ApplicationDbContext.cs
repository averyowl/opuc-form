using Microsoft.EntityFrameworkCore;
using OpucForm.Models;

namespace OpucForm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormEntry> FormEntries { get; set; } = null!;
    }
}
