using System;
using System.Collections.Generic;
using System.Reflection;
using Gorny.KetchupCatalog.Interfaces;

namespace Gorny.KetchupCatalog.BLC
{
    public class DataProvider
    {
        private readonly IDao _dao;

        public IEnumerable<IKetchup> Ketchups => _dao.GetKetchups();
        public IEnumerable<IProducer> Producers => _dao.GetProducers();

        public DataProvider(string mockName)
        {
            string dllFolder = "Release";
#if DEBUG
            dllFolder = "Debug";
#endif
            Assembly assembly = Assembly.UnsafeLoadFrom($@"..\..\..\{mockName}\bin\{dllFolder}\{mockName}.dll");

            Type daoType = assembly.GetType($"Gorny.KetchupCatalog.{mockName}.Dao");

            _dao = Activator.CreateInstance(daoType) as IDao;
        }
    }
}
