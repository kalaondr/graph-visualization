using System.Collections.Generic;

namespace GraphAnalysisCore.Analysis
{
    public class SearchableNode
    {
        public SearchableNode(string id, List<string> adjacentNodeIds)
        {
            Id = id;
            AdjacentNodeIds = adjacentNodeIds;
        }

        public string Id { get; private set; }

        public SearchableNode PreviousNode { get; set; }

        public List<string> AdjacentNodeIds { get; set; }

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