using GraphDataStorageCore.Extensions;
using GraphDataStorageCore.Repositories;
using GraphServicesHostApplication.Unity;
using GraphShared.DataContracts;
using GraphShared.ServiceContracts;
using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication.Services
{
    /// <summary>
    /// Service providing graph data optimized for visualization purposes.
    /// </summary>
    public class GraphVisualizationService : IGraphVisualizationService
    {
        private IGraphRepository graphRepository;

        public GraphVisualizationService()
        {
            graphRepository = Container.Instance.Resolve<IGraphRepository>();
        }

        /// <summary>
        /// Gets a graph including edge information by it's id
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        public GraphWithEdges GetGraphWithEdges(string graphId)
        {
            var graph = graphRepository.GetGraph(graphId);
            var convertedGraph = graph.ToGraphWithEdges();
            return convertedGraph;
        }
    }
}