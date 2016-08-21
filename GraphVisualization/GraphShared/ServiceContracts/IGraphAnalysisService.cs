using System.ServiceModel;
using GraphShared.DataContracts;

namespace GraphShared.ServiceContracts
{
    /// <summary>
    /// Interface for a service providing graph analysis operations.
    /// </summary>
    [ServiceContract]
    public interface IGraphAnalysisService
    {
        /// <summary>
        /// Gets the shortest path in graph from start node to destination node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        [OperationContract]
        GraphPath GetShortestPathInGraph(Graph graph, string fromNodeId, string toNodeId);
    }
}