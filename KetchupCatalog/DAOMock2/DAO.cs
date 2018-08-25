using System.Collections.Generic;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.DAOMock2
{
    public class Dao : IDao
    {
        public IEnumerable<IKetchup> GetKetchups()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IProducer> GetProducers()
        {
            throw new System.NotImplementedException();
        }
    }
}
