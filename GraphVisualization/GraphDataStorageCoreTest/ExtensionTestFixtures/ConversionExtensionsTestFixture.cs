using System.Collections.Generic;
using System.Linq;
using GraphDataStorageCore.Extensions;
using GraphShared.DataContracts;
using NUnit.Framework;
using Graph = GraphDataStorageCore.Entities.Graph;
using Node = GraphDataStorageCore.Entities.Node;

namespace GraphDataStorageCoreTest.ExtensionTestFixtures
{
    [TestFixture]
    public class ConversionExtensionsTestFixture
    {
        [Test]
        public void NullEntityConvertsToNullDataContract()
        {
            Graph entity = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var contract = entity.ToDataContract();
            Assert.IsNull(contract);
        }

        [Test]
        public void NullDataContractConvertsToNullEntity()
        {
            GraphShared.DataContracts.Graph contract = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var entity = contract.ToEntity();
            Assert.IsNull(entity);
        }

        [Test]
        public void EntityConvertsToDataContract()
        {
            var entity = new Graph("aaa",
                new HashSet<Node>
                {
                    new Node("1", "111", new HashSet<string> {"2", "3"}),
                    new Node("2", "222", new HashSet<string> {"1", "3"}),
                    new Node("3", "333", new HashSet<string> {"2", "1"})
                });
            // ReSharper disable once ExpressionIsAlwaysNull
            var contract = entity.ToDataContract();
            Assert.IsNotNull(contract);
            Assert.AreEqual(entity.Id, contract.Id);
            Assert.AreEqual(entity.Nodes.Count, contract.Nodes.Count);
            Assert.IsTrue(
                entity.Nodes.All(
                    x =>
                        contract.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label && y.AdjacentNodeIds != null &&
                                y.AdjacentNodeIds.Equals(x.AdjacentNodeIds))));
            Assert.IsTrue(
                contract.Nodes.All(
                    x =>
                        entity.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label && y.AdjacentNodeIds != null &&
                                y.AdjacentNodeIds.Equals(x.AdjacentNodeIds))));
        }

        [Test]
        public void DataContractConvertsToEntity()
        {
            var contract = new GraphShared.DataContracts.Graph("aaa",
                new HashSet<GraphShared.DataContracts.Node>
                {
                    new GraphShared.DataContracts.Node("1", "111", new HashSet<string> {"2", "3"}),
                    new GraphShared.DataContracts.Node("2", "222", new HashSet<string> {"1", "3"}),
                    new GraphShared.DataContracts.Node("3", "333", new HashSet<string> {"2", "1"})
                });
            // ReSharper disable once ExpressionIsAlwaysNull
            var entity = contract.ToEntity();
            Assert.IsNotNull(entity);
            Assert.AreEqual(contract.Id, entity.Id);
            Assert.AreEqual(contract.Nodes.Count, entity.Nodes.Count);
            Assert.IsTrue(
                contract.Nodes.All(
                    x =>
                        entity.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label && y.AdjacentNodeIds != null &&
                                y.AdjacentNodeIds.Equals(x.AdjacentNodeIds))));
            Assert.IsTrue(
                entity.Nodes.All(
                    x =>
                        contract.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label && y.AdjacentNodeIds != null &&
                                y.AdjacentNodeIds.Equals(x.AdjacentNodeIds))));
        }

        [Test]
        public void NullGraphConvertsToNullGraphWithEdges()
        {
            Graph graph = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var graphWithEdges = graph.ToGraphWithEdges();
            Assert.IsNull(graphWithEdges);
        }

        [Test]
        public void GraphConvertsToGraphWithEdges()
        {
            var graph = new Graph("aaa",
                new HashSet<Node>
                {
                    new Node("1", "111", new HashSet<string> {"2", "3"}),
                    new Node("2", "222", new HashSet<string> {"1", "3"}),
                    new Node("3", "333", new HashSet<string> {"2", "1"})
                });
            // ReSharper disable once ExpressionIsAlwaysNull
            var graphWithEdges = graph.ToGraphWithEdges();
            Assert.IsNotNull(graphWithEdges);
            Assert.AreEqual(graph.Id, graphWithEdges.Id);
            Assert.AreEqual(graph.Nodes.Count, graphWithEdges.Nodes.Count);
            Assert.IsTrue(
                graph.Nodes.All(
                    x =>
                        graphWithEdges.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label)));
            Assert.IsTrue(
                graphWithEdges.Nodes.All(
                    x =>
                        graph.Nodes.Any(
                            y =>
                                y.Id == x.Id && y.Label == x.Label)));
            Assert.AreEqual(3, graphWithEdges.Edges.Count);
            Assert.IsTrue(graphWithEdges.Edges.Contains(new UndirectedEdge("1", "2")));
            Assert.IsTrue(graphWithEdges.Edges.Contains(new UndirectedEdge("1", "3")));
            Assert.IsTrue(graphWithEdges.Edges.Contains(new UndirectedEdge("2", "3")));
        }
    }
}