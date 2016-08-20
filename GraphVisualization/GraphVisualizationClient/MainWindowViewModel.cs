using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GraphVisualizationClient.GraphOperations;
using GraphVisualizationClient.GraphParts;

namespace GraphVisualizationClient
{
    /// <summary>
    /// View model for the main window of the application with graph display and buttons.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGraphLoader graphLoader;

        public MainWindowViewModel()
        {
            string hostAddress = null;
            try
            {
                hostAddress = ConfigurationManager.AppSettings["ServiceHostAddress"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read setting 'ServiceHostAddress' from config file:\n {ex.Message}");
                Environment.Exit(-1);
            }
            graphLoader = new GraphLoader(hostAddress);
            ReloadGraphCommand = new RelayCommand(ReloadGraphAsync);
            ReloadGraphAsync();
        }

        public RelayCommand ReloadGraphCommand { get; set; }

        /// <summary>
        /// Reloads the graph asynchronously.
        /// </summary>
        public async void ReloadGraphAsync()
        {
            var g = await Task.Run(() => graphLoader.LoadGraph());
            Graph = g;
        }

        private ShortestPathGraph graph;

        public ShortestPathGraph Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged(nameof(Graph));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        /// <summary>
        /// Checks or unchecks the node.
        /// </summary>
        /// <param name="node"></param>
        public void SwitchNodeSelection(SelectableNode node)
        {
            node.IsSelected = !node.IsSelected;
            NotifyPropertyChanged(nameof(CanComputeShortestPath));
        }

        /// <summary>
        /// Returns if exactly two nodes are selected.
        /// </summary>
        public bool CanComputeShortestPath
        {
            get { return graph != null && graph.Vertices.Count(x => x.IsSelected) == 2; }
        }
    }
}