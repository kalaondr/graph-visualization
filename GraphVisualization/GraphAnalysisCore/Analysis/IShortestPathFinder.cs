using GraphShared.DataContracts;

namespace GraphAnalysisCore.Analysis
{
    public interface IShortestPathFinder
    {
        GraphPath FindShortestPath(Graph graph, string fromNodeId, string toNodeId);
    }
}