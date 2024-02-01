using MongoDB.Driver;
using Moq;

namespace Unit.Helpers
{
    internal class MocksMongoDbHelper
    {
        public static (Mock<IMongoClient>, Mock<IMongoDatabase>, Mock<IAsyncCursor<TModel>>, Mock<IMongoCollection<TModel>>) PrepareMongoCollectionForFindAsync<TModel>(
            string collectionName,
            string datatbaseName,
            IList<TModel> returnData,
            MongoCollectionSettings? collectionSettings = null,
            MongoDatabaseSettings? databaseSettings = null
        )
        {
            var asyncCursor = new Mock<IAsyncCursor<TModel>>(MockBehavior.Strict);
            var mockMongoCollection = new Mock<IMongoCollection<TModel>>();

            asyncCursor.Setup(x => x.Current).Returns(returnData);
            asyncCursor.Setup(x => x.Dispose());

            asyncCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            mockMongoCollection.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<TModel>>(),
                It.IsAny<FindOptions<TModel, TModel>>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(asyncCursor.Object);

            var (mockMongoClient, mockMongoDatabase) = InitializeMongoDb<TModel>(
                collectionName,
                datatbaseName,
                mockMongoCollection
            );

            return (mockMongoClient, mockMongoDatabase, asyncCursor, mockMongoCollection);
        }

        private static (Mock<IMongoClient>, Mock<IMongoDatabase>) InitializeMongoDb<TModel>(
            string collectionName,
            string datatbaseName,
            Mock<IMongoCollection<TModel>> mockMongoCollection,
            MongoCollectionSettings? collectionSettings = null,
            MongoDatabaseSettings? databaseSettings = null
        )
        {
            var mockMongoClient = new Mock<IMongoClient>();
            var mockMongoDatabase = new Mock<IMongoDatabase>(MockBehavior.Strict);

            mockMongoDatabase.Setup(m => m.GetCollection<TModel>(collectionName, collectionSettings))
                .Returns(mockMongoCollection.Object);

            return (mockMongoClient, mockMongoDatabase);
        }
    }
}
