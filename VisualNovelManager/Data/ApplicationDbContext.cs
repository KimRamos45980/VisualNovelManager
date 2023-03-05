using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisualNovelManager.Models;

namespace VisualNovelManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<VisualNovelManager.Models.VisualNovel> VisualNovel { get; set; }
        public DbSet<VisualNovelManager.Models.VNList> VNList { get; set; }
    }
}