using System;
using Security.Sample.Core.Data;
using Security.Sample.Data.DBContext;
using Security.Sample.Data;
using Security.Sample.Core.Services;

namespace Security.Sample.Service
{
    public class ServiceBase : IServiceBase
    {
        private UnitOfWork _unitOfWork;

        public ServiceBase()
        {
            IDbContextFactory dbContextFactory = new DbContextFactory();
            _unitOfWork = new UnitOfWork(dbContextFactory);
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }

        }
    }
}