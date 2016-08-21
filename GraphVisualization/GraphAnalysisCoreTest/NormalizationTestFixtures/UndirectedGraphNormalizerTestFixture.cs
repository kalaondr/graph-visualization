using System;
using System.Collections.Generic;
using System.Linq;
using GraphAnalysisCore.Normalization;
using GraphShared.DataContracts;
using NUnit.Framework;

namespace GraphAnalysisCoreTest.NormalizationTestFixtures
{
    [TestFixture]
    public class UndirectedGraphNormalizerTestFixture
    {
        private IGraphNormalizer graphNormalizer = new UndirectedGraphNormalizer();

        [Test]
        public void NormalizeNullGraphThrows()
        {
            Assert.Throws<ArgumentNullException>(() => graphNormalizer.Normalize(null));
        }

        [Test]
        public void NormalizeGraphWithNoNodesChangesNothing()
        {
            var graphId = "aaa";
            var graph = new Graph(graphId);
            graphNormalizer.Normalize(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.IsFalse(graph.Nodes.Any());
        }

        [Test]
        public void NormalizeRemovesInvalidAdjacentNodeId()
        {
            var graphId = "aaa";
            var firstNodeId = "1";
            var secondNodeId = "2";
            var firstNodeLabel = "bbb";
            var secondNodeLabel = "ccc";
            var firstNode = new Node(firstNodeId, firstNodeLabel, new HashSet<string> {secondNodeId});
            var secondNode = new Node(secondNodeId, secondNodeLabel, new HashSet<string> {firstNodeId, "3"});
            var graph = new Graph(graphId,
                new HashSet<Node>
                {
                    firstNode,
                    secondNode
                });
            graphNormalizer.Normalize(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.AreEqual(new HashSet<Node> {firstNode, secondNode}, graph.Nodes);
            Assert.AreEqual(firstNodeId, firstNode.Id);
            Assert.AreEqual(firstNodeLabel, firstNode.Label);
            Assert.AreEqual(new HashSet<string> {secondNodeId}, firstNode.AdjacentNodeIds);
            Assert.AreEqual(secondNodeId, secondNode.Id);
            Assert.AreEqual(secondNodeLabel, secondNode.Label);
            Assert.AreEqual(new HashSet<string> {firstNodeId}, secondNode.AdjacentNodeIds);
        }

        [Test]
        public void NormalizeMakesUnidirectionalEdgeBidirectional()
        {
            var graphId = "aaa";
            var firstNodeId = "1";
            var secondNodeId = "2";
            var firstNodeLabel = "bbb";
            var secondNodeLabel = "ccc";
            var firstNode = new Node(firstNodeId, firstNodeLabel, new HashSet<string> {secondNodeId});
            var secondNode = new Node(secondNodeId, secondNodeLabel, new HashSet<string>());
            var graph = new Graph(graphId,
                new HashSet<Node>
                {
                    firstNode,
                    secondNode
                });
            graphNormalizer.Normalize(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.AreEqual(new HashSet<Node> {firstNode, secondNode}, graph.Nodes);
            Assert.AreEqual(firstNodeId, firstNode.Id);
            Assert.AreEqual(firstNodeLabel, firstNode.Label);
            Assert.AreEqual(new HashSet<string> {secondNodeId}, firstNode.AdjacentNodeIds);
            Assert.AreEqual(secondNodeId, secondNode.Id);
            Assert.AreEqual(secondNodeLabel, secondNode.Label);
            Assert.AreEqual(new HashSet<string> {firstNodeId}, secondNode.AdjacentNodeIds);
        }
    }
}