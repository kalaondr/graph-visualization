using System.ComponentModel;
using QuickGraph;

namespace GraphVisualizationClient.GraphParts
{
    /// <summary>
    /// Represents an edge connecting two selectable nodes, that can be highlighted to show a path.
    /// </summary>
    public class HighlightableEdge : UndirectedEdge<SelectableNode>, INotifyPropertyChanged
    {
        private bool isHighLighted;

        public bool IsHighlighted
        {
            get { return isHighLighted; }
            set
            {
                isHighLighted = value;
                NotifyPropertyChanged(nameof(IsHighlighted));
            }
        }

        public HighlightableEdge(SelectableNode source, SelectableNode target, bool isHighlighted = false)
            : base(source, target)
        {
            IsHighlighted = isHighlighted;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}