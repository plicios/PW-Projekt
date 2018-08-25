using System.Collections.Generic;

namespace Gorny.KetchupCatalog.Interfaces
{
    public interface IDao
    {
        IEnumerable<IProducer> GetProducers();
        IEnumerable<IKetchup> GetKetchups();
    }
}
