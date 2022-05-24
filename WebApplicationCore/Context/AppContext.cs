using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Models;

namespace WebApplicationCore.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }
    }
}
