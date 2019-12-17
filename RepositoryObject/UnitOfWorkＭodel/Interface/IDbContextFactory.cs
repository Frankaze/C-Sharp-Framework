
using System.Data.Entity;

namespace RepositoryObject.UnitOfWorkModel.Interface
{
    public interface IDbContextFactory
    {
        DbContext GetDbContext();
    }
}
