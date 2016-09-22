using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Security.Sample.Data.DBContext
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContext dbContext;

        public DbContextFactory()
        {
            dbContext = new MisDB();
        }

        public DbContext GetContext()
        {
            return dbContext;
        }
    }
}
