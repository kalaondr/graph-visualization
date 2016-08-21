using System;

namespace GraphVisualizationClient.Exceptions
{
    /// <summary>
    /// Represents an exception during service call.
    /// </summary>
    public class ServiceCallException : Exception
    {
        public ServiceCallException(string message) : base(message)
        {
        }
    }
}