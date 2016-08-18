using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphDataStorageCore.Entities
{
    /// <summary>
    /// Represents a graph consisting of labeled nodes.
    /// </summary>
    public class Graph
    {
        public Graph(string id, HashSet<Node> nodes = null)
        {
            Id = id;
            Nodes = nodes ?? new HashSet<Node>();
        }

        /// <summary>
        /// ID of the graph
        /// </summary>
        [BsonId]
        public string Id { get; private set; }

        /// <summary>
        /// List of nodes
        /// </summary>
        public HashSet<Node> Nodes { get; private set; }

        protected bool Equals(Graph other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Graph) obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}
