using System.ServiceModel;
using GraphShared.DataContracts;

namespace GraphShared.ServiceContracts
{
    /// <summary>
    /// Interface for a service providing data for visualizing graphs
    /// </summary>
    [ServiceContract]
    public interface IGraphVisualizationService
    {
        /// <summary>
        /// Gets a graph including edge information by it's id
        /// </summary>
        /// <param name="graphId"></param>
        /// <returns></returns>
        [OperationContract]
        GraphWithEdges GetGraphWithEdges(string graphId);
    }
}