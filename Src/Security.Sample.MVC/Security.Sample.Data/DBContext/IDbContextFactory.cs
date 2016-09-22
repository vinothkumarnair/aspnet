using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Security.Sample.Data.DBContext
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
}
