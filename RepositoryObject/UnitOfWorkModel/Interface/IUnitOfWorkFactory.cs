namespace RepositoryObject.UnitOfWorkModel.Interface
{
    public interface IUnitOfWorkFactory
    {
        IDbContextFactory Create();
    }
}