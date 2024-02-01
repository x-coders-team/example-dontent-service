using CrontabRegistry.Domain.Models;
using CrontabRegistry.Domain.Repositories;
using CrontabRegistry.Infrastructure.Repositories;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using Unit.Helpers;

namespace Unit.CrontabRegistry.Infrastructure.Repositories
{
    public class WeatherForecastRepositoryTest
    {
        private IWeatherForecastRepository? _sut;
        private Mock<IMongoClient> _mockMongoClient;
        private Mock<IMongoDatabase> _mockMongoDatabase;
        private Mock<IMongoCollection<SummarieModel>> _mockMongoCollection;
        private Mock<IAsyncCursor<SummarieModel>> _summarieCursor;

        [SetUp]
        public void SetUp()
        {
            _mockMongoClient = new Mock<IMongoClient>(MockBehavior.Strict);
            _mockMongoDatabase = new Mock<IMongoDatabase>(MockBehavior.Strict);
            _mockMongoCollection = new Mock<IMongoCollection<SummarieModel>>(MockBehavior.Strict);
            _summarieCursor = new Mock<IAsyncCursor<SummarieModel>>(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            _mockMongoDatabase.VerifyAll();
            _mockMongoCollection.VerifyAll();
            _mockMongoClient.VerifyAll();
            _summarieCursor.VerifyAll();
        }

        [Test]
        public async Task GetSummariesLshould_return_list_of_summaries_as_array_of_strings()
        {
            // arrange
            var expectedFatabaseResult = new List<SummarieModel>() {
                new SummarieModel () { Id = "65a2923acea7dd8f30e8cb41", Name = "Freezing"},
                new SummarieModel () { Id = "65a292b0cea7dd8f30e8cb42", Name = "Bracing"},
                new SummarieModel () { Id = "65a292d7cea7dd8f30e8cb43", Name = "Chilly"},
                new SummarieModel () { Id = "65a29309cea7dd8f30e8cb44", Name = "Cool"},
                new SummarieModel () { Id = "65a29323cea7dd8f30e8cb45", Name = "Mild"},
                new SummarieModel () { Id = "65a29344cea7dd8f30e8cb46", Name = "Warm"},
                new SummarieModel () { Id = "65a29369cea7dd8f30e8cb47", Name = "Balmy"},
                new SummarieModel () { Id = "65a29387cea7dd8f30e8cb48", Name = "Hot"},
                new SummarieModel () { Id = "65a293a4cea7dd8f30e8cb49", Name = "Sweltering"},
                new SummarieModel () { Id = "65a293c3cea7dd8f30e8cb4a", Name = "Scorching"},
            };
            var expectedSummaries = new[]
            {
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching"
            };

            (_mockMongoClient, _mockMongoDatabase, _summarieCursor, _mockMongoCollection) = MocksMongoDbHelper
                .PrepareMongoCollectionForFindAsync<SummarieModel>(
                    "summaries",
                    "crontab-registry",
                    expectedFatabaseResult,
                    null,
                    null
                );

            _sut = new WeatherForecastRepository(_mockMongoDatabase.Object);

            // act
            var results = await _sut.GetSummaries();

            // assert
            results.Should().Equal(expectedSummaries);
        }
    }
}
