namespace Simbad.Platform.Persistence
{
    public abstract class Dao<TId>
    {
        public TId Id { get; set; }
    }
}