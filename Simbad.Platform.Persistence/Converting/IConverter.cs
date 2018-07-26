using Simbad.Platform.Core.Substance;
using Simbad.Platform.Persistence.Storage;

namespace Simbad.Platform.Persistence.Converting
{
    public interface IConverter
    {
        TDao BusinessObject2Dao<TDao>(BusinessObject businessObject) where TDao : Dao;

        TBusinessObject Dao2BusinessObject<TBusinessObject>(Dao dao) where TBusinessObject : BusinessObject;
    }
}