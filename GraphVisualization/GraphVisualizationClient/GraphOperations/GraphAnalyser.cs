using System;
using GraphShared.DataContracts;
using GraphShared.Helpers;
using GraphShared.ServiceContracts;
using GraphVisualizationClient.Exceptions;

namespace GraphVisualizationClient.GraphOperations
{
    /// <summary>
    /// Graph analyser using GraphAnalysisService.
    /// </summary>
    public class GraphAnalyser : IGraphAnalyser
    {
        private readonly string hostAddress;

        public GraphAnalyser(string hostAddress)
        {
            this.hostAddress = hostAddress;
        }

        /// <summary>
        /// Gets the shortest path in graph from start node to destination node by calling a web service on given address.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromNodeId"></param>
        /// <param name="toNodeId"></param>
        /// <returns></returns>
        public GraphPath GetShortestPathInGraph(Graph graph, string fromNodeId, string toNodeId)
        {
            var address = $"http://{hostAddress}/Services/GraphAnalysisService.svc";
            var factory = ChannelHelper.GetWsHttpChannelFactory<IGraphAnalysisService>(address);
            GraphPath grapPath;
            try
            {
                var channel = factory.CreateChannel();
                grapPath = channel.GetShortestPathInGraph(graph, fromNodeId, toNodeId);
            }
            catch (Exception ex)
            {
                throw new ServiceCallException(
                    $"Failed to compute the shortest path using web service on address {address}:\n {ex.Message}");
            }
            finally
            {
                try
                {
                    factory.Close();
                }
                catch
                {
                }
            }
            return grapPath;
        }
    }
}