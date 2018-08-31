using Gorny.KetchupCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorny.KetchupCatalog.DaoDatabase
{
    public class Dao : IDao
    {
        KetchupCatalogDatabaseEntities _context;

        public Dao()
        {
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

        public void DeleteKetchup(IKetchup ketchup)
        {
            _context.Ketchups.Remove(ketchup as Ketchup);
            _context.SaveChanges();
        }

        public void DeleteProducer(IProducer producer)
        {
            _context.Producers.Remove(producer as Producer);
            _context.SaveChanges();
        }

        public IEnumerable<IKetchup> GetKetchups()
        {
            return _context.Ketchups.ToList();
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return _context.Producers.ToList();
        }

        public void SaveKetchup(IKetchup ketchup, int? index)
        {
            if (!_context.Ketchups.Contains(ketchup))
            {
                _context.Ketchups.Add(ketchup as Ketchup);
                _context.SaveChanges();
            }
        }

        public void SaveProducer(IProducer producer, int? index)
        {
            if (!_context.Producers.Contains(producer))
            {
                _context.Producers.Add(producer as Producer);
                _context.SaveChanges();
            }
        }
        //        private SQLiteConnection _conntection;

        //        private const string KetchupsTable = "Ketchups";
        //        private const string ProducersTable = "Producers";

        //        private const int KetchupIdRowNumber = 0;
        //        private const int KetchupNameRowNumber = 1;
        //        private const int KetchupProducerIdRowNumber = 2;
        //        private const int KetchupTypeIdRowNumber = 3;
        //        private const int KetchupManufacureDateRowNumber = 4;

        //        private const int ProducerIdRowNumber = 0;
        //        private const int ProducerNameRowNumber = 1;

        //        public Dao()
        //        {
        //            string dllFolder = "Release";
        //#if DEBUG
        //            dllFolder = "Debug";
        //#endif
        //            string databaseName = new Settings().DatabaseName;
        //            string databasePath = $@"..\..\..\DaoDatabase\bin\{dllFolder}\{databaseName}.dll";
        //            string connectionString = $"Data Source={databaseName}";
        //            _conntection = new SQLiteConnection(connectionString);
        //        }

        //        private void Open()
        //        {
        //            _conntection.Open();
        //        }

        //        private void Close()
        //        {
        //            _conntection.Close();
        //        }

        //        private SQLiteCommand Command()
        //        {
        //            return _conntection.CreateCommand();
        //        }

        //        private int GetLastId()
        //        {
        //            return (int)_conntection.LastInsertRowId;
        //            //SQLiteCommand getIdCommand = Command();
        //            //getIdCommand.CommandText = "SELECT SCOPE_IDENTITY();";
        //            //SQLiteDataReader reader = getIdCommand.ExecuteReader();
        //            //reader.Read();
        //            //int id = reader.GetInt32(0);
        //            //return id;
        //        }

        //        public IKetchup AddKetchup()
        //        {
        //            return new Ketchup {Id = -1, Name = "", ManufactureDate = DateTime.Now };
        //        }

        //        public IProducer AddProducer()
        //        {
        //            return new Producer {Id = -1, Name = "" };
        //        }

        //        public void DeleteKetchup(IKetchup iKetchup)
        //        {
        //            if (iKetchup is Ketchup ketchup && ketchup.Id > -1)
        //            {
        //                Open();
        //                SQLiteCommand command = Command();
        //                command.CommandText = $"DELETE FROM {KetchupsTable} WHERE Id = {ketchup.Id};";
        //                command.ExecuteNonQuery();
        //                Close();
        //            }
        //        }

        //        public void DeleteProducer(IProducer iProducer)
        //        {
        //            if (iProducer is Producer producer && producer.Id > -1)
        //            {
        //                Open();
        //                SQLiteCommand command = Command();
        //                command.CommandText = $"DELETE FROM {ProducersTable} WHERE Id = {producer.Id};";
        //                command.ExecuteNonQuery();
        //                Close();
        //            }
        //        }

        //        public IEnumerable<IKetchup> GetKetchups()
        //        {
        //            List<IKetchup> ketchups = new List<IKetchup>();
        //            List<Producer> producers = GetProducers() as List<Producer>;
        //            Open();
        //            SQLiteCommand command = Command();
        //            command.CommandText = $"SELECT * FROM {KetchupsTable}";
        //            SQLiteDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                int id = reader.GetInt32(KetchupIdRowNumber);
        //                string name = reader.GetString(KetchupNameRowNumber);
        //                int producerId = reader.GetInt32(KetchupProducerIdRowNumber);
        //                int typeId = reader.GetInt32(KetchupTypeIdRowNumber);
        //                string manufactureDate = reader.GetString(KetchupManufacureDateRowNumber);

        //                Ketchup ketchup = new Ketchup
        //                {
        //                    Id = id,
        //                    Name = name,
        //                    Producer = producers.Find(producer => producer.Id == producerId),
        //                    Type = (KetchupType)typeId,
        //                    ManufactureDate = DateTime.ParseExact(manufactureDate, Constants.DateFormat, null)
        //                };

        //                ketchups.Add(ketchup);
        //            }

        //            reader.Close();
        //            Close();

        //            return ketchups;
        //        }

        //        public IEnumerable<IProducer> GetProducers()
        //        {
        //            List<IProducer> producers = new List<IProducer>();
        //            Open();
        //            SQLiteCommand command = Command();
        //            command.CommandText = $"SELECT * FROM {ProducersTable}";
        //            SQLiteDataReader reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                int id = reader.GetInt32(ProducerIdRowNumber);
        //                string name = reader.GetString(ProducerNameRowNumber);

        //                Producer producer = new Producer
        //                {
        //                    Id = id,
        //                    Name = name
        //                };

        //                producers.Add(producer);
        //            }

        //            reader.Close();
        //            Close();

        //            return producers;
        //        }

        //        public void SaveKetchup(IKetchup iKetchup, int? index)
        //        {
        //            if(iKetchup is Ketchup ketchup)
        //            {
        //                Open();
        //                SQLiteCommand command = Command();
        //                if (ketchup.Id == -1)
        //                {
        //                    command.CommandText = $"INSERT INTO {KetchupsTable} (Name, Producer_ID, Type_ID, ManufactureDate) VALUES " +
        //                        $"({ketchup.Name}, {(ketchup.Producer as Producer).Id}, {(int) ketchup.Type}, {ketchup.ManufactureDate.ToString(Constants.DateFormat)});";
        //                    command.ExecuteNonQuery();
        //                    ketchup.Id = GetLastId();
        //                }
        //                else
        //                {
        //                    command.CommandText = $"UPDATE {KetchupsTable} SET Name = {ketchup.Name}, Producer_ID = {(ketchup.Producer as Producer).Id}, Type_ID = {(int) ketchup.Type}, ManufactureDate = {ketchup.ManufactureDate.ToString(Constants.DateFormat)} WHERE Id = {ketchup.Id}";
        //                    command.ExecuteNonQuery();
        //                }
        //                Close();
        //            }
        //        }

        //        public void SaveProducer(IProducer iProducer, int? index)
        //        {
        //            if (iProducer is Producer producer)
        //            {
        //                Open();
        //                SQLiteCommand command = Command();
        //                if (producer.Id == -1)
        //                {
        //                    command.CommandText = $"INSERT INTO {ProducersTable} (Name) VALUES ({producer.Name});";
        //                    command.ExecuteNonQuery();
        //                    producer.Id = GetLastId();
        //                }
        //                else
        //                {
        //                    command.CommandText = $"UPDATE {KetchupsTable} SET Name = {producer.Name} WHERE Id = {producer.Id}";
        //                    command.ExecuteNonQuery();
        //                }
        //                Close();
        //            }
        //        }

    }
}
