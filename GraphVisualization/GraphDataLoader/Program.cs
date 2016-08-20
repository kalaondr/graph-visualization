using GraphDataLoader.GraphImport;
using GraphDataLoader.Helpers;
using GraphDataLoaderCore.Loaders;

namespace GraphDataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ArgumentHelper.ValidateArguments(args)) return;
            string hostAddress;
            if (!ConfigurationHelper.TryReadHostAddress(out hostAddress)) return;
            IGraphImporter graphImporter = new GraphImporter(new XmlGraphLoader(args[0]), hostAddress);
            graphImporter.ImportGraph();
        }
    }
}