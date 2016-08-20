using System.Runtime.Serialization;

namespace GraphShared.DataContracts
{
    /// <summary>
    /// Represents a node without information about adjacent nodes.
    /// </summary>
    [DataContract]
    public class SimpleNode
    {
        public SimpleNode(string id, string label)
        {
            Id = id;
            Label = label;
        }

        /// <summary>
        /// ID of the node in scope of graph
        /// </summary>
        [DataMember]
        public string Id { get; private set; }

        /// <summary>
        /// Label of the node for display purposes
        /// </summary>
        [DataMember]
        public string Label { get; private set; }

        protected bool Equals(SimpleNode other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SimpleNode) obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}