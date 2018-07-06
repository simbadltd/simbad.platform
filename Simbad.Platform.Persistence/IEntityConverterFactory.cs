namespace Simbad.Platform.Persistence
{
    public interface IEntityConverterFactory
    {
        IEntityConverter<TId> Create<TId>();
    }
}