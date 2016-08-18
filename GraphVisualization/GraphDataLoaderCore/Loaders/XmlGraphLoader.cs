using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GraphDataLoaderCore.Exceptions;
using GraphShared.DataContracts;

namespace GraphDataLoaderCore.Loaders
{
    /// <summary>
    /// A graph loader that loads the graph from a folder with xml files each representing a node.
    /// </summary>
    public class XmlGraphLoader : IGraphLoader
    {
        private readonly string pathToFolderWithXmlFiles;

        public XmlGraphLoader(string pathToFolderWithXmlFiles)
        {
            if (pathToFolderWithXmlFiles == null) throw new ArgumentNullException(nameof(pathToFolderWithXmlFiles));
            if (!Directory.Exists(pathToFolderWithXmlFiles)) throw new ArgumentException($"Folder '{pathToFolderWithXmlFiles}' does not exist.");
            this.pathToFolderWithXmlFiles = pathToFolderWithXmlFiles;
        }

        /// <summary>
        /// Loads the graph from a set of xml files
        /// </summary>
        /// <returns></returns>
        public Graph LoadGraph(string graphId)
        {
            var xmlFiles = Directory.GetFiles(pathToFolderWithXmlFiles, "*.xml");
            var nodes = new HashSet<Node>(xmlFiles.Select(GetNodeFromXmlFile).Distinct());
            var graph = new Graph(graphId, nodes);
            return graph;
        }
        
        /// <summary>
        /// Gets a single node element from an xml file
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        private Node GetNodeFromXmlFile(string xmlFile)
        {
            Node node = null;
            try
            {
                var document = XDocument.Load(xmlFile);
                node = document.Descendants("node")
                    .Select(
                        x =>
                            new Node(x.Element("id").Value, x.Element("label").Value, new HashSet<string>(x.Element("adjacentNodes").Descendants("id").Select(y => y.Value).Distinct())))
                    .FirstOrDefault();
            }
            catch
            {
            }
            if (node == null)
            {
                throw new GraphLoaderException($"XML file '{xmlFile}' has invalid format.");
            }
            return node;
        }
    }
}
