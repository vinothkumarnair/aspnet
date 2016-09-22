using Security.Sample.Core.Data;

namespace Security.Sample.Core.Services
{
    public interface IServiceBase
    {
        IUnitOfWork UnitOfWork { get; }

    }
}