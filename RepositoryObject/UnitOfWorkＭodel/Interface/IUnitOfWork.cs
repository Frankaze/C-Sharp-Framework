using System;
using System.Data.Entity;

namespace RepositoryObject.UnitOfWorkModel.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }

        IUnitOfWork CreateSubUnit();

        int SaveChange();
    }
}