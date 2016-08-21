using GraphShared.DataContracts;

namespace GraphVisualizationClient.GraphOperations
{
    /// <summary>
    /// Interface for graph analyser classes.
    /// </summary>
    public interface IGraphAnalyser
    {
        /// <summary>
        /// Gets the shortest path in graph from start node to destination node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        GraphPath GetShortestPathInGraph(Graph graph, string fromNodeId, string toNodeId);
    }
}