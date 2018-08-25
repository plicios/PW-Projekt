using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.BLC
{
    public class DataProvider
    {
        private readonly IDao _dao;
        public DataProvider(int mockNum)
        {
            if (mockNum == 0)
            {
                _dao = new DAOMock.Dao();
            }
            else
            {
                _dao = new DAOMock2.Dao();
            }
        }
    }
}
