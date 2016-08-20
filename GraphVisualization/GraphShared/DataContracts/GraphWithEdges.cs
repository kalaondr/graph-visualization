using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphShared.DataContracts
{
    /// <summary>
    /// Represents a graph with simple nodes without adjacency information. Adjacency information is stored in the form of edges.
    /// </summary>
    [DataContract]
    public class GraphWithEdges
    {
        public GraphWithEdges(string id, HashSet<SimpleNode> nodes = null, HashSet<UndirectedEdge> edges = null)
        {
            Id = id;
            Nodes = nodes ?? new HashSet<SimpleNode>();
            Edges = edges ?? new HashSet<UndirectedEdge>();
        }

        /// <summary>
        /// ID of the graph
        /// </summary>
        [DataMember]
        public string Id { get; private set; }

        /// <summary>
        /// List of nodes
        /// </summary>
        [DataMember]
        public HashSet<SimpleNode> Nodes { get; private set; }

        /// <summary>
        /// List of nodes
        /// </summary>
        [DataMember]
        public HashSet<UndirectedEdge> Edges { get; private set; }

        protected bool Equals(GraphWithEdges other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GraphWithEdges) obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}