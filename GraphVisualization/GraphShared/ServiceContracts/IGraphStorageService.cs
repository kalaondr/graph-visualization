using System.ServiceModel;
using GraphShared.DataContracts;

namespace GraphShared.ServiceContracts
{
    /// <summary>
    /// Interface for a service storing graphs
    /// </summary>
    [ServiceContract]
    public interface IGraphStorageService
    {
        /// <summary>
        /// Inserts a new graph or replaces existing
        /// </summary>
        /// <param name="graph"></param>
        [OperationContract]
        void SaveOrUpdateGraph(Graph graph);
    }
}