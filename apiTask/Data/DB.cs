using apiTask.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace apiTask.Data
{
    public class DB
    {
        private readonly IMongoDatabase _database;
        public DB()
        {
            var client= new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("apiTask");
        }
        public IMongoCollection<Patient> Patients => _database.GetCollection<Patient>("Patients");
        public IMongoCollection<PatientRecord> PatientRecords => _database.GetCollection<PatientRecord>("PatientRecords");
    }
}



//sql
//using apiTask.Model;
//using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver;

//namespace apiTask.Data
//{
//    public class DB: DbContext
//    {
//        public DB(DbContextOptions<DB> options): base(options)
//        {
//        }
//        public DbSet<Patient> Patients { get; set; }

//    }
//}
