using GraphDataStorageCore.Extensions;
using GraphDataStorageCore.Repositories;
using GraphServicesHostApplication.Unity;
using GraphShared.DataContracts;
using GraphShared.ServiceContracts;
using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication.Services
{
    /// <summary>
    /// Service for storing graphs into MongoDb database
    /// </summary>
    public class GraphStorageService : IGraphStorageService
    {
        private IGraphRepository graphRepository;

        public GraphStorageService()
        {
            graphRepository = Container.Instance.Resolve<IGraphRepository>();
        }

        /// <summary>
        /// Inserts a new graph or replaces existing
        /// </summary>
        /// <param name="graph"></param>
        public void SaveOrUpdateGraph(Graph graph)
        {
            var convertedGraph = graph.ToEntity();
            graphRepository.SaveOrUpdateGraph(convertedGraph);
        }
    }
}