using System;
using System.Collections.Generic;
using System.Linq;
using GraphShared.DataContracts;

namespace GraphAnalysisCore.Normalization
{
    /// <summary>
    /// Graph normalizer that removes invalid adjacent node ids (including id of the node itself) and makes all unidirectional edges bidirectional. 
    /// </summary>
    public class UndirectedGraphNormalizer : IGraphNormalizer
    {
        /// <summary>
        /// Normalizes given graph by removing invalid adjacent node ids and making all unidirectional edges bidirectional. 
        /// </summary>
        /// <param name="graph"></param>
        public void Normalize(Graph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var nodes = graph.Nodes.ToDictionary(node => node.Id);
            foreach (var node in graph.Nodes)
            {
                NormalizeNode(node, nodes);
            }
        }

        /// <summary>
        /// Normalizes given node by removing invalid adjacent node ids and making all unidirectional edges bidirectional. 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        private static void NormalizeNode(Node node, Dictionary<string, Node> nodes)
        {
            var adjacentNodeIdsToRemove = new List<string>();
            foreach (var adjacentNodeId in node.AdjacentNodeIds)
            {
                NormalizeAdjacentId(node, nodes, adjacentNodeId, adjacentNodeIdsToRemove);
            }
            foreach (var nodeIdToRemove in adjacentNodeIdsToRemove)
            {
                node.AdjacentNodeIds.Remove(nodeIdToRemove);
            }
        }

        /// <summary>
        /// Checks if adjacent node id points to an existing node other than itself, marks it for removal if not. If yes, ensures that adjacent node points to this node as well.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <param name="adjacentNodeId"></param>
        /// <param name="adjacentNodeIdsToRemove"></param>
        private static void NormalizeAdjacentId(Node node, Dictionary<string, Node> nodes, string adjacentNodeId,
            List<string> adjacentNodeIdsToRemove)
        {
            if (adjacentNodeId == node.Id || !nodes.ContainsKey(adjacentNodeId))
            {
                adjacentNodeIdsToRemove.Add(adjacentNodeId);
                return;
            }
            var adjacentNode = nodes[adjacentNodeId];
            adjacentNode.AdjacentNodeIds.Add(node.Id);
        }
    }
}