using System;
using Gorny.KetchupCatalog.Core;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.DAOMock2
{
    class Ketchup :IKetchup
    {
        public string Name { get; set; }
        public KetchupType Type { get; set; }
        public DateTime ManufactureDate { get; set; }
        public IProducer Producer { get; set; }
    }
}
