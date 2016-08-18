using GraphShared.DataContracts;

namespace GraphDataLoaderCore.Loaders
{
    /// <summary>
    /// Interface for classes able to load a graph.
    /// </summary>
    public interface IGraphLoader
    {
        /// <summary>
        /// Loads the graph and assigns give id to it
        /// </summary>
        /// <returns></returns>
        Graph LoadGraph(string graphId);
    }
}