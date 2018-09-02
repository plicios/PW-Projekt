using Gorny.KetchupCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorny.KetchupCatalog.DaoDatabase
{
    public class Dao : IDao
    {
        private readonly KetchupCatalogDatabaseEntities _context;

        public Dao()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
            _context = new KetchupCatalogDatabaseEntities();
        }

        public IKetchup AddKetchup()
        {
            return new Ketchup
            {
                Name = string.Empty,
                ManufactureDate = DateTime.Now
            };
        }

        public IProducer AddProducer()
        {
            return new Producer
            {
                Name = string.Empty
            };
        }

        public void DeleteKetchup(IKetchup iKetchup)
        {
            if (iKetchup is Ketchup ketchup)
            {
                if (!_context.Ketchups.ToList().Contains(ketchup))
                {
                    _context.Ketchups.Remove(ketchup);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteProducer(IProducer iProducer)
        {
            if (iProducer is Producer producer)
            {
                if (!_context.Producers.ToList().Contains(producer))
                {
                    _context.Producers.Remove(producer);
                    _context.SaveChanges();
                }
            }
        }

        public IEnumerable<IKetchup> GetKetchups()
        {
            IEnumerable<IKetchup> ketchups = _context.Ketchups.ToList();
            return ketchups;
        }

        public IEnumerable<IProducer> GetProducers()
        {
            IEnumerable<IProducer> producers = _context.Producers.ToList();;
            return producers;
        }

        public void SaveKetchup(IKetchup iKetchup, int? index)
        {
            if (iKetchup is Ketchup ketchup)
            {
                if (!_context.Ketchups.ToList().Contains(ketchup))
                {
                    _context.Ketchups.Add(ketchup);
                }
                _context.SaveChanges();
            }
        }

        public void SaveProducer(IProducer iProducer, int? index)
        {
            if (iProducer is Producer producer)
            {
                if (!_context.Producers.ToList().Contains(producer))
                {
                    _context.Producers.Add(producer);
                }
                _context.SaveChanges();
            }
        }
    }
}
