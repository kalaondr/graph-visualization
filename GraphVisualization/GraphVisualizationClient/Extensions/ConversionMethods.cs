﻿using System.Collections.Generic;
using System.Linq;
using GraphShared.DataContracts;
using GraphVisualizationClient.GraphParts;

namespace GraphVisualizationClient.Extensions
{
    /// <summary>
    /// Provides conversion extension methods.
    /// </summary>
    public static class ConversionMethods
    {
        /// <summary>
        /// Converts data contract graph with edges to GraphSharp graph
        /// </summary>
        /// <param name="graphWithEdges"></param>
        /// <returns></returns>
        public static ShortestPathGraph ToShortestPathGraph(this GraphWithEdges graphWithEdges)
        {
            if (graphWithEdges == null) return null;
            var graph = new ShortestPathGraph(graphWithEdges.Id);
            var nodes = new Dictionary<string, SelectableNode>();
            foreach (var simpleNode in graphWithEdges.Nodes)
            {
                nodes.Add(simpleNode.Id, new SelectableNode(simpleNode.Id, simpleNode.Label));
            }
            foreach (var selectableNode in nodes.Values)
            {
                graph.AddVertex(selectableNode);
            }
            foreach (var edge in graphWithEdges.Edges)
            {
                var firstNode = nodes[edge.FirstNodeId];
                var secondNode = nodes[edge.SecondNodeId];
                graph.AddEdge(new HighlightableEdge(firstNode, secondNode));
                // GraphSharp undirected graph doesn't work, use bidirectional with two edges instead
                graph.AddEdge(new HighlightableEdge(secondNode, firstNode));
            }
            return graph;
        }

        /// <summary>
        /// Converts GraphSharp graph to data contract graph.
        /// </summary>
        /// <param name="shortestPathGraph"></param>
        /// <returns></returns>
        public static Graph ToDataContract(this ShortestPathGraph shortestPathGraph)
        {
            if (shortestPathGraph == null) return null;
            var nodes = shortestPathGraph.Vertices.Select(x => new Node(x.Id, x.Label)).ToDictionary(x => x.Id);
            foreach (var edge in shortestPathGraph.Edges)
            {
                nodes[edge.Source.Id].AdjacentNodeIds.Add(edge.Target.Id);
            }
            var graph = new Graph(shortestPathGraph.Id, new HashSet<Node>(nodes.Values));
            return graph;
        }
    }
}