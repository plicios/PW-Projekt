using System;
using System.Collections.Generic;
using Gorny.KetchupCatalog.Core;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.DAOMock2
{
    public class Dao : IDao
    {
        private readonly List<IKetchup> _ketchups;
        private readonly List<IProducer> _producers;

        public Dao()
        {
            _producers = new List<IProducer>
            {
                new Producer { Name = "Heinz"},
                new Producer { Name = "Włocławek"}
            };

            _ketchups = new List<IKetchup>
            {
                new Ketchup { Name = "Ketchup pikantny", Producer = _producers[1], Type = KetchupType.Spicy, ManufactureDate = new DateTime(2018,6,20)},
                new Ketchup { Name = "Heinz Tomato Ketchup 38 oz. Bottle", Producer = _producers[0], Type = KetchupType.Mild, ManufactureDate = new DateTime(2018,5,14)},
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

        public IKetchup AddKetchup()
        {
            return new Ketchup { Name = "", ManufactureDate = DateTime.Now };
        }

        public IProducer AddProducer()
        {
            return new Producer { Name = "" };
        }

        public void SaveKetchup(IKetchup ketchup, int? index)
        {
            if (index.HasValue)
            {
                _ketchups[index.Value] = ketchup;
            }
            else
            {
                _ketchups.Add(ketchup);
            }
        }

        public void SaveProducer(IProducer producer, int? index)
        {
            if (index.HasValue)
            {
                _producers[index.Value] = producer;
            }
            else
            {
                _producers.Add(producer);
            }
        }

        public void DeleteKetchup(IKetchup ketchup)
        {
            if (_ketchups.Contains(ketchup))
            {
                _ketchups.Remove(ketchup);
            }
        }

        public void DeleteProducer(IProducer producer)
        {
            if (_producers.Contains(producer))
            {
                _producers.Remove(producer);
            }
        }
    }
}
