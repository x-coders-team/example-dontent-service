using MongoDB.Driver;

namespace CrontabRegistry.Infrastructure.Repositories
{
    public class MongoAbstractRepository
    {
        protected IMongoDatabase _contextDb;

        public MongoAbstractRepository(
            IMongoDatabase contextDb
        )
        {
            _contextDb = contextDb;
        }
    }
}
