using Simbad.Platform.Core.Substance;

namespace Simbad.Platform.Persistence
{
    public interface IEntityConverter<TId>
    {
        TDao Entity2Dao<TDao>(Entity<TId> entity) where TDao : Dao<TId>;

        TEntity Dao2Entity<TEntity>(Dao<TId> dao) where TEntity : Entity<TId>;
    }
}