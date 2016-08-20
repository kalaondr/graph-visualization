using System.Windows;
using System.Windows.Input;
using GraphSharp.Controls;
using GraphVisualizationClient.GraphParts;

namespace GraphVisualizationClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        public MainWindow()
        {
            vm = new MainWindowViewModel();
            DataContext = vm;
            InitializeComponent();
        }

        private void EventSetter_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vertex = sender as VertexControl;
            if (vertex == null) return;
            var node = vertex.Vertex as SelectableNode;
            if (node == null) return;
            vm.SwitchNodeSelection(node);
        }
    }
}