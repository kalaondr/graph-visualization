using GraphAnalysisCore.Analysis;
using GraphServicesHostApplication.Unity;
using GraphShared.DataContracts;
using GraphShared.ServiceContracts;
using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication.Services
{
    /// <summary>
    /// Service for analysis of graphs.
    /// </summary>
    public class GraphAnalysisService : IGraphAnalysisService
    {
        private IShortestPathFinder shortestPathFinder;

        public GraphAnalysisService()
        {
            shortestPathFinder = Container.Instance.Resolve<IShortestPathFinder>();
        }

        /// <summary>
        /// Gets the shortest path in graph from start node to destination node.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public GraphPath GetShortestPathInGraph(Graph graph, string fromNodeId, string toNodeId)
        {
            var path = shortestPathFinder.FindShortestPath(graph, fromNodeId, toNodeId);
            return path;
        }
    }
}