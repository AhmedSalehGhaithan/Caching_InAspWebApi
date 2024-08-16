using Microsoft.EntityFrameworkCore;
using WebApiCrud_DotNetCore8.Models.Entities;

namespace WebApiCrud_DotNetCore8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student>Students { get; set; }
    }
}
