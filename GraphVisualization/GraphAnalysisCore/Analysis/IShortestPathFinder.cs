using GraphShared.DataContracts;

namespace GraphAnalysisCore.Analysis
{
    /// <summary>
    /// Interface for classes able to find the shortest path in a graph from start node to destination node.
    /// </summary>
    public interface IShortestPathFinder
    {
        /// <summary>
        /// Finds the shortest path from start node to destination node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        GraphPath FindShortestPath(Graph graph, string fromNodeId, string toNodeId);
    }
}