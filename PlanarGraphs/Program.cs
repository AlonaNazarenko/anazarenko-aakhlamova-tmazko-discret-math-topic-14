// See https://aka.ms/new-console-template for more information

using PlanarGraphs;


Graph graph=new Graph();
graph.GenerateGraph(10, true);
Console.WriteLine(graph);
Console.WriteLine($"edges: {graph.CountEdges()}");