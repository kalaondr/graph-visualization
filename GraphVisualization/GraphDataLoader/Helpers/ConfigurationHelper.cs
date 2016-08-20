using System;
using System.Configuration;

namespace GraphDataLoader.Helpers
{
    /// <summary>
    /// Helper class for reading from config file with exception handling.
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Tries to read service host address from default config file.
        /// </summary>
        /// <param name="hostAddress"></param>
        /// <returns></returns>
        public static bool TryReadHostAddress(out string hostAddress)
        {
            try
            {
                hostAddress = ConfigurationManager.AppSettings["ServiceHostAddress"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read setting 'ServiceHostAddress' from config file:\n {ex.Message}");
                hostAddress = null;
                return false;
            }
            return true;
        }
    }
}