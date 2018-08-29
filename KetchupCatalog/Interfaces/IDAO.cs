using System.Collections.Generic;

namespace Gorny.KetchupCatalog.Interfaces
{
    public interface IDao
    {
        IEnumerable<IProducer> GetProducers();
        IEnumerable<IKetchup> GetKetchups();

        IKetchup AddKetchup();
        IProducer AddProducer();

        void SaveKetchup(IKetchup ketchup, int? index);
        void SaveProducer(IProducer producer, int? index);
    }
}
