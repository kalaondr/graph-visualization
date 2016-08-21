using System;
using System.Collections.Generic;
using System.Linq;
using GraphAnalysisCore.Analysis;
using GraphAnalysisCore.Normalization;
using GraphShared.DataContracts;
using NUnit.Framework;
using Rhino.Mocks;

namespace GraphAnalysisCoreTest.AnalysisTestFixtures
{
    [TestFixture]
    public class BreadthFirstShortestPathFinderTestFixture
    {
        [Test]
        public void NullGraphThrows()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            Assert.Throws<ArgumentNullException>(() => shortestPathFinder.FindShortestPath(null, "1", "2"));
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void NullFromNodeIdThrows()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            Assert.Throws<ArgumentNullException>(() => shortestPathFinder.FindShortestPath(new Graph("aaa"), null, "2"));
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void NullToNodeIdThrows()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            Assert.Throws<ArgumentNullException>(() => shortestPathFinder.FindShortestPath(new Graph("aaa"), "1", null));
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void NotExistingFromNodeIdThrows()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "1";
            var graph = new Graph("aaa");
            Assert.That(() => shortestPathFinder.FindShortestPath(graph, fromNodeId, "2"),
                Throws.Exception.TypeOf<ArgumentException>()
                    .With.Message.EqualTo($"Node with id {fromNodeId} does not exist in the graph."));
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void NotExistingToNodeIdThrows()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "1";
            var toNodeId = "2";
            var graph = new Graph("aaa", new HashSet<Node> {new Node(fromNodeId, "bbb")});
            Assert.That(
                () =>
                    shortestPathFinder.FindShortestPath(graph, fromNodeId, toNodeId),
                Throws.Exception.TypeOf<ArgumentException>()
                    .With.Message.EqualTo($"Node with id {toNodeId} does not exist in the graph."));
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void FindPathFromNodeToItselfReturnsExistingPathWithNoEdges()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "1";
            var graph = new Graph("aaa", new HashSet<Node> {new Node(fromNodeId, "bbb", new HashSet<string>())});
            normalizerMock.Expect(x => x.Normalize(graph));
            var path = shortestPathFinder.FindShortestPath(graph, fromNodeId, fromNodeId);
            Assert.IsNotNull(path);
            Assert.AreEqual(fromNodeId, path.FromNodeId);
            Assert.AreEqual(fromNodeId, path.ToNodeId);
            Assert.IsTrue(path.PathExists);
            Assert.IsNotNull(path.EdgeSequence);
            Assert.IsFalse(path.EdgeSequence.Any());
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void FindPathFromNotConnectedNodesReturnNotExistingPathWithNoEdges()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "1";
            var toNodeId = "2";
            var graph = new Graph("aaa", new HashSet<Node> {new Node(fromNodeId, "bbb"), new Node(toNodeId, "ccc")});
            normalizerMock.Expect(x => x.Normalize(graph));
            var path = shortestPathFinder.FindShortestPath(graph, fromNodeId, toNodeId);
            Assert.IsNotNull(path);
            Assert.AreEqual(fromNodeId, path.FromNodeId);
            Assert.AreEqual(toNodeId, path.ToNodeId);
            Assert.IsFalse(path.PathExists);
            Assert.IsNotNull(path.EdgeSequence);
            Assert.IsFalse(path.EdgeSequence.Any());
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void FindPathFindsSimplePath()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "1";
            var toNodeId = "2";
            var graph = new Graph("aaa",
                new HashSet<Node>
                {
                    new Node(fromNodeId, "bbb", new HashSet<string> {toNodeId}),
                    new Node(toNodeId, "ccc")
                });
            normalizerMock.Expect(x => x.Normalize(graph));
            var path = shortestPathFinder.FindShortestPath(graph, fromNodeId, toNodeId);
            Assert.IsNotNull(path);
            Assert.AreEqual(fromNodeId, path.FromNodeId);
            Assert.AreEqual(toNodeId, path.ToNodeId);
            Assert.IsTrue(path.PathExists);
            Assert.IsNotNull(path.EdgeSequence);
            Assert.AreEqual(new List<UndirectedEdge> {new UndirectedEdge(fromNodeId, toNodeId)}, path.EdgeSequence);
            normalizerMock.VerifyAllExpectations();
        }

        [Test]
        public void FindPathFindsLongerPath()
        {
            var normalizerMock = MockRepository.GenerateStrictMock<IGraphNormalizer>();
            var shortestPathFinder = new BreadthFirstShortestPathFinder(normalizerMock);
            var fromNodeId = "a";
            var toNodeId = "j";
            var graph = new Graph("aaa",
                new HashSet<Node>
                {
                    new Node(fromNodeId, "aaa", new HashSet<string> {"b"}),
                    new Node("b", "bbb", new HashSet<string> {"c", "d", "f"}),
                    new Node("c", "ccc", new HashSet<string> {"d"}),
                    new Node("d", "ddd", new HashSet<string> {"e"}),
                    new Node("f", "fff", new HashSet<string> {"g", "h"}),
                    new Node("h", "hhh", new HashSet<string> {"i", "j"}),
                    new Node("i", "ddd", new HashSet<string>()),
                    new Node(toNodeId, "ddd", new HashSet<string>())
                });
            normalizerMock.Expect(x => x.Normalize(graph));
            var path = shortestPathFinder.FindShortestPath(graph, fromNodeId, toNodeId);
            Assert.IsNotNull(path);
            Assert.AreEqual(fromNodeId, path.FromNodeId);
            Assert.AreEqual(toNodeId, path.ToNodeId);
            Assert.IsTrue(path.PathExists);
            Assert.IsNotNull(path.EdgeSequence);
            Assert.AreEqual(new List<UndirectedEdge>
            {
                new UndirectedEdge(fromNodeId, "b"),
                new UndirectedEdge("b", "f"),
                new UndirectedEdge("f", "h"),
                new UndirectedEdge("h", toNodeId),
            }, path.EdgeSequence);
            normalizerMock.VerifyAllExpectations();
        }
    }
}