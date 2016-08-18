using System;
using System.Linq.Expressions;
using System.Threading;
using GraphDataStorageCore.Context;
using GraphDataStorageCore.Entities;
using GraphDataStorageCore.Repositories;
using MongoDB.Driver;
using NUnit.Framework;
using Rhino.Mocks;

namespace GraphDataStorageCoreTest.RepositoryTestFixtures
{
    [TestFixture]
    public class GraphRepositoryTestFixture
    {
        [Test]
        public void NullGraphDbContextThrows()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new GraphRepository(null));
        }

        [Test]
        public void SaveOrUpdateGraphReplacesGraph()
        {
            var graph = new Graph("aaa");
            var graphsMock = MockRepository.GenerateStrictMock<IMongoCollection<Graph>>();
            var databaseMock = MockRepository.GenerateStrictMock<IMongoDatabase>();
            var dbContextMock = MockRepository.GenerateStrictMock<IGraphDbContext>();
            dbContextMock.Expect(x => x.Database).Return(databaseMock);
            databaseMock.Expect(x => x.GetCollection<Graph>("Graphs")).Return(graphsMock);
            graphsMock.Expect(x => x.DeleteOne(Arg<Expression<Func<Graph, bool>>>.Is.NotNull, Arg<CancellationToken>.Is.Equal(default(CancellationToken))));
            graphsMock.Expect(x => x.InsertOne(graph));
            var repository = new GraphRepository(dbContextMock);
            dbContextMock.VerifyAllExpectations();
            repository.SaveOrUpdateGraph(graph);
            databaseMock.VerifyAllExpectations();
            graphsMock.VerifyAllExpectations();
        }

        [Test, Ignore("Find is an extension method, would require a wrapper to mock it.")]
        public void GetGraphFindsGraph()
        {
            var graph = new Graph("aaa");
            var graphsMock = MockRepository.GenerateStrictMock<IMongoCollection<Graph>>();
            var databaseMock = MockRepository.GenerateStrictMock<IMongoDatabase>();
            var dbContextMock = MockRepository.GenerateStrictMock<IGraphDbContext>();
            var findFluentMock = MockRepository.GenerateStrictMock<IFindFluent<Graph, Graph>>();
            dbContextMock.Expect(x => x.Database).Return(databaseMock);
            databaseMock.Expect(x => x.GetCollection<Graph>("Graphs")).Return(graphsMock);
            graphsMock.Expect(x => x.Find(Arg<Expression<Func<Graph, bool>>>.Is.NotNull, Arg<FindOptions>.Is.Null)).Return(findFluentMock);
            findFluentMock.Expect(x => x.FirstOrDefault()).Return(graph);
            var repository = new GraphRepository(dbContextMock);
            repository.GetGraph("aaa");
            dbContextMock.VerifyAllExpectations();
            repository.SaveOrUpdateGraph(graph);
            databaseMock.VerifyAllExpectations();
            graphsMock.VerifyAllExpectations();
            findFluentMock.VerifyAllExpectations();
        }
    }
}