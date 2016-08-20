using System;

namespace GraphDataLoader.Helpers
{
    /// <summary>
    /// Helper class for validating command line arguments.
    /// </summary>
    public static class ArgumentHelper
    {
        /// <summary>
        /// Validates that program was called with correct number of arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool ValidateArguments(string[] args)
        {
            if (args.Length == 1) return true;
            Console.WriteLine("Wrong number of arguments. Usage: GraphDataLoader <pathToFolderWithXmlFiles>");
            return false;
        }
    }
}