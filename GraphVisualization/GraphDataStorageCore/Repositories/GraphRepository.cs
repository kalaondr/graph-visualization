using System;
using GraphDataStorageCore.Context;
using GraphDataStorageCore.Entities;
using MongoDB.Driver;

namespace GraphDataStorageCore.Repositories
{
    /// <summary>
    /// Graph repository using a MongoDb database
    /// </summary>
    public class GraphRepository : IGraphRepository
    {
        private IMongoCollection<Graph> graphs;

        public GraphRepository(IGraphDbContext graphDbContext)
        {
            if (graphDbContext == null) throw new ArgumentNullException(nameof(graphDbContext));
            graphs = graphDbContext.Database.GetCollection<Graph>("Graphs");
        }

        /// <summary>
        /// Inserts a new graph or replaces existing
        /// </summary>
        /// <param name="graph"></param>
        public void SaveOrUpdateGraph(Graph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            graphs.DeleteOne(x => x.Id == graph.Id);
            graphs.InsertOne(graph);
        }

        /// <summary>
        /// Gets a graph with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Graph GetGraph(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var graph = graphs.Find(x => x.Id == id).FirstOrDefault();
            return graph;
        }
    }
}