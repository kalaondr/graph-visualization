using System;
using System.IO;
using System.Linq;
using System.Reflection;
using GraphDataLoaderCore.Exceptions;
using GraphDataLoaderCore.Loaders;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GraphDataLoaderCoreTest.LoaderTestFixtures
{
    [TestFixture]
    public class XmlGraphLoaderTestFixture
    {
        [Test]
        public void NullPathToFolderWithXmlFilesThrows()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new XmlGraphLoader(null));
        }

        [Test]
        public void NotExistingPathToFolderWithXmlFilesThrows()
        {
            const string notExistingFolderPath = "xxx";
            Assert.That(() => new XmlGraphLoader(notExistingFolderPath),
                Throws.ArgumentException.With.Message.EqualTo($"Folder '{notExistingFolderPath}' does not exist."));
        }

        [Test]
        public void FolderWithNoXmlFilesReturnsEmptyGraphWithId()
        {
            var folderPathWithNoXmlFiles = GetLocalPath().Replace("\\Debug", string.Empty);
            var loader = new XmlGraphLoader(folderPathWithNoXmlFiles);
            var graphId = "aaa";
            var graph = loader.LoadGraph(graphId);
            Assert.IsNotNull(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.IsFalse(graph.Nodes.Any());
        }

        [Test]
        public void FolderWithValidXmlFilesReturnsGraphWithNodesAndId()
        {
            var folderPathWithValidXmlFiles = GetLocalPath() + "\\TestFiles";
            var loader = new XmlGraphLoader(folderPathWithValidXmlFiles);
            var graphId = "aaa";
            var graph = loader.LoadGraph(graphId);
            Assert.IsNotNull(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.AreEqual(10, graph.Nodes.Count);
            Assert.IsTrue(graph.Nodes.All(x => !string.IsNullOrWhiteSpace(x.Id)));
            Assert.IsTrue(graph.Nodes.All(x => !string.IsNullOrWhiteSpace(x.Label)));
            Assert.IsTrue(graph.Nodes.All(x => x.AdjacentNodeIds != null));
            Assert.IsTrue(graph.Nodes.Any(x => x.Id == "1" && x.Label == "Apple" && x.AdjacentNodeIds.Count == 2 && x.AdjacentNodeIds.Contains("2") && x.AdjacentNodeIds.Contains("3")));
        }

        [Test]
        public void FolderWithInvalidXmlFileThrows()
        {
            var folderPathWithInvalidFile = GetLocalPath() + "\\InvalidTestFile";
            var invalidFile = $"{folderPathWithInvalidFile}\\xxx.xml"; 
            var loader = new XmlGraphLoader(folderPathWithInvalidFile);
            var graphId = "aaa";
            Assert.That(() => loader.LoadGraph(graphId), Throws.Exception.TypeOf<GraphLoaderException>().With.Message.EqualTo($"XML file '{invalidFile}' has invalid format."));
        }

        [Test]
        public void FolderWithXmlFilesWithDuplicateNodesReturnsGraphWithSingleNode()
        {
            var folderPathWithInvalidFile = GetLocalPath() + "\\DuplicateTestFiles";
            var loader = new XmlGraphLoader(folderPathWithInvalidFile);
            var graphId = "bbb";
            var graph = loader.LoadGraph(graphId);
            Assert.IsNotNull(graph);
            Assert.AreEqual(graphId, graph.Id);
            Assert.IsNotNull(graph.Nodes);
            Assert.AreEqual(1, graph.Nodes.Count);
            Assert.IsTrue(graph.Nodes.Any(x => x.Id == "8" && x.Label == "aaa" && x.AdjacentNodeIds != null && x.AdjacentNodeIds.Count == 1 && x.AdjacentNodeIds.Contains("9")));
        }

        private static string GetLocalPath()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
        }
    }
}
