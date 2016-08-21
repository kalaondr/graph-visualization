using System;
using System.Threading.Tasks;
using System.Windows;
using GraphShared.DataContracts;
using GraphShared.Helpers;
using GraphShared.ServiceContracts;
using GraphVisualizationClient.Extensions;
using GraphVisualizationClient.GraphParts;

namespace GraphVisualizationClient.GraphOperations
{
    /// <summary>
    /// Graph loader loading from GraphVisualizationService on given host address.
    /// </summary>
    public class GraphLoader : IGraphLoader
    {
        private readonly string hostAddress;

        public GraphLoader(string hostAddress)
        {
            this.hostAddress = hostAddress;
        }

        /// <summary>
        /// Loads the graph from web service and converts it.
        /// </summary>
        /// <returns></returns>
        public ShortestPathGraph LoadGraph()
        {
            var address = $"http://{hostAddress}/Services/GraphVisualizationService.svc";
            var factory = ChannelHelper.GetWsHttpChannelFactory<IGraphVisualizationService>(address);
            GraphWithEdges graphWithEdges;
            try
            {
                var channel = factory.CreateChannel();
                graphWithEdges = channel.GetGraphWithEdges("1");
                // working with only one graph, uses always this same id, could be easily extended
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load the graph from web service on address {address}:\n {ex.Message}");
                return null;
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
            var graph = graphWithEdges.ToShortestPathGraph();
            return graph;
        }
    }
}