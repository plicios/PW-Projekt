using System;
using System.Collections.Generic;
using Gorny.KetchupCatalog.Core;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.DAOMock
{
    public class Dao : IDao
    {
        private readonly List<IKetchup> _ketchups;
        private readonly List<IProducer> _producers;

        public Dao()
        {
            _producers = new List<IProducer>
            {
                new Producer { Name = "Kotlin"},
                new Producer { Name = "Pudliszki"}
            };

            _ketchups = new List<IKetchup>
            {
                new Ketchup { Name = "Ketchup ostry najsmaczniejszy", Producer = _producers[1], Type = KetchupType.Hot, ManufactureDate = new DateTime(2018,6,20)},
                new Ketchup { Name = "Łagodny ketchup kotlin", Producer = _producers[0], Type = KetchupType.Mild, ManufactureDate = new DateTime(2018,5,14)},
            };
        }

        public IEnumerable<IKetchup> GetKetchups()
        {
            return _ketchups;
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return _producers;
        }
    }
}
