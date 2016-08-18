using GraphDataStorageCore.Entities;

namespace GraphDataStorageCore.Repositories
{
    /// <summary>
    /// Interface for graph repositories.
    /// </summary>
    public interface IGraphRepository
    {
        /// <summary>
        /// Inserts a new graph or replaces existing
        /// </summary>
        /// <param name="graph"></param>
        void SaveOrUpdateGraph(Graph graph);

        /// <summary>
        /// Gets a graph with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Graph GetGraph(string id);
    }
}