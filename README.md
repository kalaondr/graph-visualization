# graph-visualization

Graph visualization application consisting of a WPF client, WCF services hosted in IIS and a console application data loader for loading graphs from xml files.

Targeted .NET framework version: 4.6.1

Installation steps:

1) Download and install MongoDB Community Server: https://www.mongodb.com/download-center#community
2) Start MongoDB server (mongod.exe in the MongoDB\Server\3.2\bin folder).
3) If MongoDB server is started on a different address than localhost:27017, change app setting "MongoDbConnectionString" in GraphServicesHostApplication.dll.config accordingly.
4) Host GraphServicesHostApplication in IIS (IIS 10.0 Express in VS 2015 was used for testing).
5) If GraphServicesHostApplication is hosted on a different address than localhost:58737, change app setting "ServiceHostAddress" in "GraphVisualizationClient.exe.config" and in "GraphDataLoader.exe.config" accordingly.

User manual:

- Start GraphDataLoader <pathToFolderWithXmlFiles> to import a graph.
- Start GraphVisualizationClient to view the imported graph.
- Every edge is displayed as bidirectional, graph is considered as undirected. 
- Select/deselect a node by double clicking it.
- Selecting exactly two nodes will enable "Calculate shortest path" button.
- Shortest path is visualized by red edges between nodes.
- Use the "Clear all" button to deselect all nodes and hide the path visualization.
- Use the "Reload graph" button to reload the graph from the database (use in case of GraphDataLoader reimport).
