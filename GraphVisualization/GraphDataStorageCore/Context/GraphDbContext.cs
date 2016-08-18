using MongoDB.Driver;

namespace GraphDataStorageCore.Context
{
    /// <summary>
    /// Context for MongoDb database "graphs"
    /// </summary>
    public class GraphDbContext : IGraphDbContext
    {
        public GraphDbContext(string connectionString)
        {
            var settings = new MongoClientSettings { Server = MongoServerAddress.Parse(connectionString) };
            var client = new MongoClient(settings);
            Database = client.GetDatabase("graphs");
        }

        /// <summary>
        /// The database
        /// </summary>
        public IMongoDatabase Database { get; }
    }
}
