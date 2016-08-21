using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using GraphShared.DataContracts;
using GraphVisualizationClient.Extensions;
using GraphVisualizationClient.GraphOperations;
using GraphVisualizationClient.GraphParts;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace GraphVisualizationClient
{
    /// <summary>
    /// View model for the main window of the application with graph display and buttons.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGraphLoader graphLoader;
        private readonly IGraphAnalyser graphAnalyser;

        public MainWindowViewModel()
        {
            string hostAddress = null;
            try
            {
                hostAddress = ConfigurationManager.AppSettings["ServiceHostAddress"];
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Failed to read setting 'ServiceHostAddress' from config file:\n {ex.Message}");
                Environment.Exit(-1);
            }
            graphLoader = new GraphLoader(hostAddress);
            graphAnalyser = new GraphAnalyser(hostAddress);
            ReloadGraphCommand = new RelayCommand(ReloadGraphAsync, () => !IsBusy);
            ComputeShortestPathCommand = new RelayCommand(ComputeShortestPathAsync,
                () => CanComputeShortestPath && !IsBusy);
            ClearAllCommand = new RelayCommand(ClearAll, () => !IsBusy);
            ReloadGraphAsync();
        }
        
        public RelayCommand ReloadGraphCommand { get; set; }

        public RelayCommand ComputeShortestPathCommand { get; set; }

        public RelayCommand ClearAllCommand { get; set; }

        /// <summary>
        /// Deselects all nodes and removes all edge highlights.
        /// </summary>
        private void ClearAll()
        {
            IsBusy = true;
            DeselectAllNodes();
            ClearEdgeHighlights();
            IsBusy = false;
        }

        /// <summary>
        /// Reloads the graph asynchronously.
        /// </summary>
        private async void ReloadGraphAsync()
        {
            IsBusy = true;
            try
            {
                var g = await Task.Run(() => graphLoader.LoadGraph());
                Graph = g;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            IsBusy = false;
        }

        /// <summary>
        /// Computes the shortest path between two nodes asynchronously and highlights it in the graph.
        /// </summary>
        private async void ComputeShortestPathAsync()
        {
            if (Graph == null) return;
            var selectedNodes = Graph.Vertices.Where(x => x.IsSelected).ToList();
            if (selectedNodes.Count != 2) return;
            IsBusy = true;
            var firstSelectedNode = selectedNodes.First();
            var secondSelectedNode = selectedNodes.Last();
            var g = Graph.ToDataContract();
            try
            {
                var shortestPath = await
                    Task.Run(
                        () => graphAnalyser.GetShortestPathInGraph(g, firstSelectedNode.Id, secondSelectedNode.Id));
                HighlightThePathInGraph(shortestPath);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            IsBusy = false;
        }

        /// <summary>
        /// Shows given error message to user.
        /// </summary>
        /// <param name="message"></param>
        private static void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows given info message to user.
        /// </summary>
        /// <param name="message"></param>
        private static void ShowInfoMessage(string message)
        {
            ShowMessage(message, MessageBoxImage.Information);
        }

        /// <summary>
        /// Shows given error message to user.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="image"></param>
        private static void ShowMessage(string message, MessageBoxImage image)
        {
            MessageBox.Show(message, "Graph Visualization", MessageBoxButton.OK, image);
        }

        /// <summary>
        /// Highlights the given path in graph.
        /// </summary>
        /// <param name="shortestPath"></param>
        private void HighlightThePathInGraph(GraphPath shortestPath)
        {
            if (shortestPath == null) return;
            if (!shortestPath.PathExists)
            {
                ShowInfoMessage("Path between the selected nodes doesn't exist.");
                return;
            }
            foreach (var edge in Graph.Edges)
            {
                var undirectedEdge = new UndirectedEdge(edge.Source.Id, edge.Target.Id);
                edge.IsHighlighted = shortestPath.EdgeSequence.Contains(undirectedEdge);
            }
        }

        /// <summary>
        /// Deselects all nodes.
        /// </summary>
        private void DeselectAllNodes()
        {
            if (Graph == null) return;
            foreach (var node in Graph.Vertices)
            {
                node.IsSelected = false;
            }
            NotifyPropertyChanged(nameof(CanComputeShortestPath));
            ComputeShortestPathCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Clears all edge highlights.
        /// </summary>
        private void ClearEdgeHighlights()
        {
            if (Graph == null) return;
            foreach (var node in Graph.Edges)
            {
                node.IsHighlighted = false;
            }
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

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            private set
            {
                isBusy = value;
                NotifyPropertyChanged(nameof(IsBusy));
                ReloadGraphCommand.RaiseCanExecuteChanged();
                ClearAllCommand.RaiseCanExecuteChanged();
                ComputeShortestPathCommand.RaiseCanExecuteChanged();
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
            ClearEdgeHighlights();
            NotifyPropertyChanged(nameof(CanComputeShortestPath));
            ComputeShortestPathCommand.RaiseCanExecuteChanged();
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