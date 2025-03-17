using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.DataAccess
{
    public class NoushGahDbContextFactory : IDesignTimeDbContextFactory<NoushGahDbContext>
    {
        public NoushGahDbContext CreateDbContext(string[] args)
        {
            string man = "Server=.;Database=NoushGah;User ID=sa; Trust Server Certificate=true; Password=ilia.1384;";
            var optionsBuilder = new DbContextOptionsBuilder<NoushGahDbContext>();
            optionsBuilder.UseSqlServer(man);

            return new NoushGahDbContext(optionsBuilder.Options);
        }
    }
}
