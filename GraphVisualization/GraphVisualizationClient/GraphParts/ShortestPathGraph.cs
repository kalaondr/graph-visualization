using QuickGraph;

namespace GraphVisualizationClient.GraphParts
{
    /// <summary>
    /// Represents a graph containing selectable nodes with support for showing shortest path by highlighting edges.
    /// </summary>
    public class ShortestPathGraph : BidirectionalGraph<SelectableNode, HighlightableEdge>
    {
        public ShortestPathGraph() : base(false)
        {
        }
    }
}