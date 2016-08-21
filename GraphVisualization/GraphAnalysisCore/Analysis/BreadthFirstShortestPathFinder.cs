using System;
using System.Collections.Generic;
using System.Linq;
using GraphAnalysisCore.Normalization;
using GraphShared.DataContracts;

namespace GraphAnalysisCore.Analysis
{
    /// <summary>
    /// Shortest path finder for undirected graphs with equal edge lengths using breadth first search.
    /// </summary>
    public class BreadthFirstShortestPathFinder : IShortestPathFinder
    {
        private readonly IGraphNormalizer normalizer;

        public BreadthFirstShortestPathFinder(IGraphNormalizer normalizer)
        {
            this.normalizer = normalizer;
        }

        /// <summary>
        /// Finds the shortest path from one node to another by doing a breadth search marking nodes with previous node references.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public GraphPath FindShortestPath(Graph graph, string fromNodeId, string toNodeId)
        {
            ValidateArguments(graph, fromNodeId, toNodeId);
            normalizer.Normalize(graph); // normalizes the graph so all edges are valid
            var nodes = ConstructNodeDictionary(graph);
            var fromNode = nodes[fromNodeId];
            fromNode.PreviousNode = fromNode;
            var activeNodes = new List<SearchableNode> {fromNode};
            var nextActiveNodes = new List<SearchableNode>();
            while (activeNodes.Any())
            {
                foreach (var activeNode in activeNodes)
                {
                    if (activeNode.Id == toNodeId)
                    {
                        return ReconstructPath(activeNode, fromNodeId);
                    }
                    AddUnvisitedAdjacentNodesToActive(activeNode, nodes, nextActiveNodes);
                }
                activeNodes = nextActiveNodes;
                nextActiveNodes = new List<SearchableNode>();
            }
            return new GraphPath(fromNodeId, toNodeId, new List<UndirectedEdge>(), false);
        }

        /// <summary>
        /// Constructs a dictionary with node ids as keys and nodes as values for simple quick access to any node.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        private static Dictionary<string, SearchableNode> ConstructNodeDictionary(Graph graph)
        {
            var nodes = new Dictionary<string, SearchableNode>();
            foreach (var node in graph.Nodes)
            {
                var searchableNode = new SearchableNode(node.Id, node.AdjacentNodeIds.ToList());
                nodes.Add(node.Id, searchableNode);
            }
            return nodes;
        }

        /// <summary>
        /// Adds unvisited (previous node == null) adjacent nodes of given node to the list of active nodes.
        /// </summary>
        /// <param name="activeNode"></param>
        /// <param name="nodes"></param>
        /// <param name="nextActiveNodes"></param>
        private static void AddUnvisitedAdjacentNodesToActive(SearchableNode activeNode,
            Dictionary<string, SearchableNode> nodes, List<SearchableNode> nextActiveNodes)
        {
            foreach (
                var searchableNode in
                    activeNode.AdjacentNodeIds.Where(x => x != activeNode.Id && nodes.ContainsKey(x))
                        .Select(adjacentNodeId => nodes[adjacentNodeId])
                        .Where(searchableNode => searchableNode.PreviousNode == null))
            {
                searchableNode.PreviousNode = activeNode;
                nextActiveNodes.Add(searchableNode);
            }
        }

        /// <summary>
        /// Reconstructs the path from start node to destination node by traversing via previous node references from destination node.
        /// </summary>
        /// <param name="destinationNode"></param>
        /// <param name="fromNodeId"></param>
        /// <returns></returns>
        private GraphPath ReconstructPath(SearchableNode destinationNode, string fromNodeId)
        {
            var edges = new List<UndirectedEdge>();
            var currentNode = destinationNode;
            while (!ReferenceEquals(currentNode.PreviousNode, currentNode))
            {
                edges.Add(new UndirectedEdge(currentNode.PreviousNode.Id, currentNode.Id));
                currentNode = currentNode.PreviousNode;
            }
            edges.Reverse();
            return new GraphPath(fromNodeId, destinationNode.Id, edges);
        }

        /// <summary>
        /// Validates arguments for nulls a not existing node ids.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        private static void ValidateArguments(Graph graph, string fromNodeId, string toNodeId)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (fromNodeId == null) throw new ArgumentNullException(nameof(fromNodeId));
            if (toNodeId == null) throw new ArgumentNullException(nameof(toNodeId));
            if (graph.Nodes.All(x => x.Id != fromNodeId))
                throw new ArgumentException($"Node with id {fromNodeId} does not exist in the graph.");
            if (graph.Nodes.All(x => x.Id != toNodeId))
                throw new ArgumentException($"Node with id {toNodeId} does not exist in the graph.");
        }
    }
}