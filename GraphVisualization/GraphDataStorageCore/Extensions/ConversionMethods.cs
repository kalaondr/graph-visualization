using System.Collections.Generic;
using System.Linq;
using GraphShared.DataContracts;

namespace GraphDataStorageCore.Extensions
{
    /// <summary>
    /// Extension methods for graph conversion
    /// </summary>
    public static class ConversionMethods
    {
        /// <summary>
        /// Converts data contract graph to MongoDb entity graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static Entities.Graph ToEntity(this Graph graph)
        {
            if (graph == null) return null;
            return new Entities.Graph(graph.Id,
                new HashSet<Entities.Node>(
                    graph.Nodes.Select(x => new Entities.Node(x.Id, x.Label, x.AdjacentNodeIds))));
        }

        /// <summary>
        /// Converts MongoDb entity graph to data contract graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static Graph ToDataContract(this Entities.Graph graph)
        {
            if (graph == null) return null;
            return new Graph(graph.Id,
                new HashSet<Node>(
                    graph.Nodes.Select(x => new Node(x.Id, x.Label, x.AdjacentNodeIds))));
        }
    }
}