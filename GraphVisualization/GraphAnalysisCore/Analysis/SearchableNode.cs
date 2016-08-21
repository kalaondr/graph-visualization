using System.Collections.Generic;

namespace GraphAnalysisCore.Analysis
{
    /// <summary>
    /// Internal shortest path finder node representation, with a previous node property for storing the path through graph.
    /// </summary>
    internal class SearchableNode
    {
        public SearchableNode(string id, List<string> adjacentNodeIds)
        {
            Id = id;
            AdjacentNodeIds = adjacentNodeIds;
        }

        /// <summary>
        /// Id of the node
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Reference to previous node, set during search.
        /// </summary>
        public SearchableNode PreviousNode { get; set; }

        /// <summary>
        /// Ids of adjacent nodes.
        /// </summary>
        public List<string> AdjacentNodeIds { get; private set; }

        protected bool Equals(SearchableNode other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SearchableNode) obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}