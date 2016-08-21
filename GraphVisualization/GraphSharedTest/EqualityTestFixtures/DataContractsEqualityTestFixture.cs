using System.Collections.Generic;
using GraphShared.DataContracts;
using NUnit.Framework;

namespace GraphSharedTest.EqualityTestFixtures
{
    [TestFixture]
    public class DataContractsEqualityTestFixture
    {
        [Test]
        public void GraphEqualityIsBasedOnId()
        {
            var firstGraph = new Graph("aaa");
            var secondGraph = new Graph("aaa");
            Assert.IsTrue(firstGraph.Equals(secondGraph));
            Assert.AreEqual(firstGraph.GetHashCode(), secondGraph.GetHashCode());
        }

        [Test]
        public void GraphWithEdgesEqualityIsBasedOnId()
        {
            var firstGraph = new GraphWithEdges("aaa");
            var secondGraph = new GraphWithEdges("aaa");
            Assert.IsTrue(firstGraph.Equals(secondGraph));
            Assert.AreEqual(firstGraph.GetHashCode(), secondGraph.GetHashCode());
        }

        [Test]
        public void NodeEqualityIsBasedOnId()
        {
            var firstNode = new Node("aaa", "bbb");
            var secondNode = new Node("aaa", "bbb");
            Assert.IsTrue(firstNode.Equals(secondNode));
            Assert.AreEqual(firstNode.GetHashCode(), secondNode.GetHashCode());
        }

        [Test]
        public void SimpleNodeEqualityIsBasedOnId()
        {
            var firstNode = new SimpleNode("aaa", "bbb");
            var secondNode = new SimpleNode("aaa", "bbb");
            Assert.IsTrue(firstNode.Equals(secondNode));
            Assert.AreEqual(firstNode.GetHashCode(), secondNode.GetHashCode());
        }

        [Test]
        public void UndirectedEdgeEqualityIsBasedOnNodeIds()
        {
            var firstEdge = new UndirectedEdge("1", "2");
            var secondEdge = new UndirectedEdge("1", "2");
            Assert.IsTrue(firstEdge.Equals(secondEdge));
            Assert.AreEqual(firstEdge.GetHashCode(), secondEdge.GetHashCode());
        }

        [Test]
        public void UndirectedEdgeEqualityIsBasedOnNodeIdsAndIgnoresDirection()
        {
            var firstEdge = new UndirectedEdge("1", "2");
            var secondEdge = new UndirectedEdge("2", "1");
            Assert.IsTrue(firstEdge.Equals(secondEdge));
            Assert.AreEqual(firstEdge.GetHashCode(), secondEdge.GetHashCode());
        }

        [Test]
        public void GraphPathEqualityIsBasedOnAllProperties()
        {
            var firstNodeId = "1";
            var secondNodeId = "2";
            var thirdNodeId = "3";
            var firstEdge = new UndirectedEdge(firstNodeId, secondNodeId);
            var secondEdge = new UndirectedEdge(secondNodeId, thirdNodeId);
            var firstPath = new GraphPath(firstNodeId, thirdNodeId, new List<UndirectedEdge> {firstEdge, secondEdge});
            var secondPath = new GraphPath(firstNodeId, thirdNodeId, new List<UndirectedEdge> {firstEdge, secondEdge});
            Assert.IsTrue(firstPath.Equals(secondPath));
            Assert.AreEqual(firstPath.GetHashCode(), secondPath.GetHashCode());
        }

        [Test]
        public void EdgeOrderMattersInGraphPathEquality()
        {
            var firstNodeId = "1";
            var secondNodeId = "2";
            var thirdNodeId = "3";
            var firstEdge = new UndirectedEdge(firstNodeId, secondNodeId);
            var secondEdge = new UndirectedEdge(secondNodeId, thirdNodeId);
            var firstPath = new GraphPath(firstNodeId, thirdNodeId, new List<UndirectedEdge> {firstEdge, secondEdge});
            var secondPath = new GraphPath(firstNodeId, thirdNodeId, new List<UndirectedEdge> {secondEdge, firstEdge});
            Assert.IsFalse(firstPath.Equals(secondPath));
            Assert.AreNotEqual(firstPath.GetHashCode(), secondPath.GetHashCode());
        }
    }
}