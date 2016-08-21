using GraphShared.DataContracts;

namespace GraphAnalysisCore.Normalization
{
    /// <summary>
    /// Interface for graph normalizers.
    /// </summary>
    public interface IGraphNormalizer
    {
        /// <summary>
        /// Normalizes given graph.
        /// </summary>
        /// <param name="graph"></param>
        void Normalize(Graph graph);
    }
}