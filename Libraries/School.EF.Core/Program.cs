using Microsoft.EntityFrameworkCore;
using SchoolEF.Core.Data;
using SchoolEF.Core.Entities;

namespace SchoolEF.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var dbcontext = new SchoolDbContext();

            //dbcontext.Database.Migrate();

            //dbcontext.Database.EnsureCreated();
            //
            //dbcontext.Schools.Add(new School { Name = "Perez Alonzo" });
            //
            //dbcontext.SaveChanges();
            //
            //dbcontext.Schools.ToList().ForEach(school => Console.WriteLine(school.Name));
            //
            //dbcontext.Database.EnsureDeleted();
        }
    }
}