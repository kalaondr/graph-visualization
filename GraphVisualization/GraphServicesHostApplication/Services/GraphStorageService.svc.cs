using GraphAnalysisCore.Normalization;
using GraphDataStorageCore.Extensions;
using GraphDataStorageCore.Repositories;
using GraphServicesHostApplication.Unity;
using GraphShared.DataContracts;
using GraphShared.ServiceContracts;
using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication.Services
{
    /// <summary>
    /// Service for storing graphs using repository, normalizing them first using normalizer. 
    /// </summary>
    public class GraphStorageService : IGraphStorageService
    {
        private IGraphRepository graphRepository;
        private IGraphNormalizer graphNormalizer;

        public GraphStorageService()
        {
            graphRepository = Container.Instance.Resolve<IGraphRepository>();
            graphNormalizer = Container.Instance.Resolve<IGraphNormalizer>();
        }

        /// <summary>
        /// Inserts a new graph or replaces existing
        /// </summary>
        /// <param name="graph"></param>
        public void SaveOrUpdateGraph(Graph graph)
        {
            graphNormalizer.Normalize(graph);
            var convertedGraph = graph.ToEntity();
            graphRepository.SaveOrUpdateGraph(convertedGraph);
        }
    }
}