using Security.Sample.Core.Data;
using Security.Sample.Core.Model;
using Security.Sample.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Data;
using log4net;

namespace Security.Sample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context = null;
        //private readonly ILog _log = LogManager.GetLogger("Security.Sample.Service");

        public UnitOfWork(IDbContextFactory factory)
        {
            _context = factory.GetContext();
        }

        private IRepository<Menu> _menuRepository;

        public IRepository<Menu> MenuRepository
        {
            get
            {
                return _menuRepository ?? (_menuRepository = new Repository<Menu>(_context));
            }
        }
        


 
        private IRepository<MenuMappedToRole> _menuMappedToRoleRepository;

        public IRepository<MenuMappedToRole> MenuMappedToRoleRepository
        {
            get
            {
                return _menuMappedToRoleRepository ?? (_menuMappedToRoleRepository = new Repository<MenuMappedToRole>(_context));
            }
        }
        private IUserAccountRepository _userAccountRepository;

        public IUserAccountRepository UserAccountRepository
        {
            get
            {
                return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(_context));
            }
        }

        public void SaveChanges()
        {

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //_log.Error(this, ex);
                throw ex;
            }
          
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}

