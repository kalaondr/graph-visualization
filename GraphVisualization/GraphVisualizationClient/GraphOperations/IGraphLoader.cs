using GraphVisualizationClient.GraphParts;

namespace GraphVisualizationClient.GraphOperations
{
    /// <summary>
    /// Interface for loaders of GraphSharp graph representation.
    /// </summary>
    public interface IGraphLoader
    {
        /// <summary>
        /// Loads the graph.
        /// </summary>
        /// <returns></returns>
        ShortestPathGraph LoadGraph();
    }
}