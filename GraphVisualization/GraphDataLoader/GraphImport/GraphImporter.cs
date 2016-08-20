using System;
using GraphDataLoaderCore.Loaders;
using GraphShared.DataContracts;
using GraphShared.Helpers;
using GraphShared.ServiceContracts;

namespace GraphDataLoader.GraphImport
{
    /// <summary>
    /// Class that loads a graph using given graph loader and saves it to a graph storage service on given host address.
    /// </summary>
    public class GraphImporter : IGraphImporter
    {
        private readonly IGraphLoader graphLoader;
        private readonly string hostAddress;

        public GraphImporter(IGraphLoader graphLoader, string hostAddress)
        {
            this.graphLoader = graphLoader;
            this.hostAddress = hostAddress;
        }

        /// <summary>
        /// Imports the graph using given loader and saving it 
        /// </summary>
        public void ImportGraph()
        {
            Graph graph;
            if (!TryLoadGraph(out graph)) return;
            SaveGraph(graph);
        }

        /// <summary>
        /// Tries to load a graph using given graph loader.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        private bool TryLoadGraph(out Graph graph)
        {
            try
            {
                graph = graphLoader.LoadGraph("1");
                // working with only one graph, uses always this same id, could be easily extended
                Console.WriteLine($"Loaded graph with {graph.Nodes.Count} nodes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loading of graph has failed:\n {ex.Message}");
                graph = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves graph using GraphStorageService on host address.
        /// </summary>
        /// <param name="graph"></param>
        private void SaveGraph(Graph graph)
        {
            var address = $"http://{hostAddress}/Services/GraphStorageService.svc";
            Console.WriteLine($"Saving graph using service on address {address}");
            var factory = ChannelHelper.GetWsHttpChannelFactory<IGraphStorageService>(address);
            try
            {
                var channel = factory.CreateChannel();
                channel.SaveOrUpdateGraph(graph);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Saving of graph has failed:\n {ex.Message}");
                return;
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
            Console.WriteLine("Save successful.");
        }
    }
}