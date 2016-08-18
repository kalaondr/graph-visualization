using System.Collections.Generic;

namespace GraphDataStorageCore.Entities
{
    /// <summary>
    /// Represents a single node in a graph, with a label and a list of adjacent nodes.
    /// </summary>
    public class Node
    {
        public Node(string id, string label, HashSet<string> adjacentNodes = null)
        {
            Id = id;
            Label = label;
            AdjacentNodeIds = adjacentNodes ?? new HashSet<string>();
        }

        /// <summary>
        /// ID of the node in scope of graph
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Label of the node for display purposes
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// List of ids of nodes adjacent to this node
        /// </summary>
        public HashSet<string> AdjacentNodeIds { get; private set; }
        
        protected bool Equals(Node other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}
