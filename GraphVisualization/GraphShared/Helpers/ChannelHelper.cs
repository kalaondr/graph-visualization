using System.Runtime.InteropServices.ComTypes;
using System.ServiceModel;
using GraphShared.ServiceContracts;

namespace GraphShared.Helpers
{
    /// <summary>
    /// Helper providing methods for WCF channels.
    /// </summary>
    public static class ChannelHelper
    {
        /// <summary>
        /// Gets a ChannelFactory using WSHttpBinding for given T and address.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        public static ChannelFactory<T> GetWsHttpChannelFactory<T>(string address)
        {
            var binding = new WSHttpBinding();
            var endpointAddress = new EndpointAddress(address);
            var factory = new ChannelFactory<T>(binding, endpointAddress);
            return factory;
        }
    }
}