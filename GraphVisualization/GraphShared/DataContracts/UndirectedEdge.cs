using System.Runtime.Serialization;

namespace GraphShared.DataContracts
{
    /// <summary>
    /// Represents an edge connecting two nodes.
    /// </summary>
    [DataContract]
    public class UndirectedEdge
    {
        public UndirectedEdge(string firstNodeId, string secondNodeid)
        {
            FirstNodeId = firstNodeId;
            SecondNodeId = secondNodeid;
        }

        /// <summary>
        /// ID of the first node
        /// </summary>
        [DataMember]
        public string FirstNodeId { get; private set; }

        /// <summary>
        /// ID of the first node
        /// </summary>
        [DataMember]
        public string SecondNodeId { get; private set; }

        protected bool Equals(UndirectedEdge other)
        {
            return (string.Equals(FirstNodeId, other.FirstNodeId) && string.Equals(SecondNodeId, other.SecondNodeId))
                   || (string.Equals(FirstNodeId, other.SecondNodeId) && string.Equals(SecondNodeId, other.FirstNodeId));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((UndirectedEdge) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var firstHash = FirstNodeId?.GetHashCode() ?? 0;
                var secondHash = SecondNodeId?.GetHashCode() ?? 0;
                // hash function giving same results for instances with FirstNodeId and SecondNodeId values flipped in respect to Equals
                return firstHash < secondHash ? (firstHash*397) ^ secondHash : (secondHash*397) ^ firstHash;
            }
        }
    }
}