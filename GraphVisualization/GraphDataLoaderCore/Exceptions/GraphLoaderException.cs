using System;

namespace GraphDataLoaderCore.Exceptions
{
    /// <summary>
    /// Represents an exception during graph loading
    /// </summary>
    public class GraphLoaderException : Exception
    {
        public GraphLoaderException(string message) : base(message)
        {
            
        }
    }
}
