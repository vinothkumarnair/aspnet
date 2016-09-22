using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Core.Data
{
    public interface IUnitOfWork : IDisposable
{
       IUserAccountRepository UserAccountRepository { get; }
     
       IRepository<MenuMappedToRole> MenuMappedToRoleRepository { get; }

       IRepository<Menu> MenuRepository { get; }
    
        void SaveChanges();
        void Dispose();
   }
}
