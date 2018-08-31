using Gorny.KetchupCatalog.Core;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.DaoDatabase
{
    public partial class Ketchup : IKetchup
    {
        public KetchupType Type { get => (KetchupType) Type_ID; set => Type_ID = (int) value; }
        IProducer IKetchup.Producer { get => Producer; set => Producer = value as Producer; }
    }
}
