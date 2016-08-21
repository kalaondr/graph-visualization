using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphShared.DataContracts
{
    /// <summary>
    /// Represents a path through an undirected graph.
    /// </summary>
    [DataContract]
    public class GraphPath
    {
        public GraphPath(string fromNodeId, string toNodeId, List<UndirectedEdge> edgeSequence, bool pathExists = true)
        {
            FromNodeId = fromNodeId;
            ToNodeId = toNodeId;
            EdgeSequence = edgeSequence ?? new List<UndirectedEdge>();
            PathExists = pathExists;
        }

        /// <summary>
        /// Id of the starting node.
        /// </summary>
        [DataMember]
        public string FromNodeId { get; private set; }

        /// <summary>
        /// Id of the destination node.
        /// </summary>
        [DataMember]
        public string ToNodeId { get; private set; }

        /// <summary>
        /// Specifies if path between the nodes exists.
        /// </summary>
        [DataMember]
        public bool PathExists { get; private set; }

        /// <summary>
        /// Ordered sequence of used from start node to destination node representing the shortest path. Empty if path doesn't exist or start node and destination node are equal.
        /// </summary>
        [DataMember]
        public List<UndirectedEdge> EdgeSequence { get; private set; }

        protected bool Equals(GraphPath other)
        {
            return string.Equals(FromNodeId, other.FromNodeId) && string.Equals(ToNodeId, other.ToNodeId) &&
                   PathExists == other.PathExists && EdgeSequence.SequenceEqual(other.EdgeSequence);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GraphPath) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FromNodeId?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (ToNodeId?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ PathExists.GetHashCode();
                return EdgeSequence.Aggregate(hashCode,
                    (current, undirectedEdge) => (current*397) ^ (undirectedEdge?.GetHashCode() ?? 0));
                // would need to be changed for extremely large lists
            }
        }
    }
}