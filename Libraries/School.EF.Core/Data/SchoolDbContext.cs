using Microsoft.EntityFrameworkCore;
using SchoolEF.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEF.Core.Data
{
    internal class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
        }

        //For Dependency Injection (DI)
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        //For Simple Initialization Instance
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;database=school;trusted_connection=true");
        }

        public DbSet<School> Schools { get; set; }
    }
}
