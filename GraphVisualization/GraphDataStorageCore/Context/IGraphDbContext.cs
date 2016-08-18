using MongoDB.Driver;

namespace GraphDataStorageCore.Context
{
    /// <summary>
    /// Context for MongoDb database "graphs"
    /// </summary>
    public interface IGraphDbContext
    {
        /// <summary>
        /// The database
        /// </summary>
        IMongoDatabase Database { get; }
    }
}