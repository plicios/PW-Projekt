using System;
using Gorny.KetchupCatalog.Core;

namespace Gorny.KetchupCatalog.Interfaces
{
    public interface IKetchup
    {
        string Name { get; set; }
        KetchupType Type { get; set; }
        DateTime ManufactureDate { get; set; }
        IProducer Producer { get; set; }
    }
}
