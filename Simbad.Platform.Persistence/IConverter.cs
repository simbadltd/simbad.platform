using Simbad.Platform.Core.Substance;

namespace Simbad.Platform.Persistence
{
    public interface IConverter
    {
        TDao BusinessObject2Dao<TDao>(BusinessObject businessObject) where TDao : Dao;

        TBusinessObject Dao2BusinessObject<TBusinessObject>(Dao dao) where TBusinessObject : BusinessObject;
    }
}