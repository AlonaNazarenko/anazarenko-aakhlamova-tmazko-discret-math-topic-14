// See https://aka.ms/new-console-template for more information

using PlanarGraphs;

Graph graph=new Graph();
graph.AddEdge(1,2);
graph.AddEdge(1,3);
graph.AddEdge(3,2);
graph.AddEdge(4,2);
graph.AddEdge(3,5);
graph.AddEdge(2,5);
graph.AddEdge(2,4);
Console.WriteLine(graph);
var vertexes = graph.GetVertices();
foreach (var v in vertexes)
{
    Console.WriteLine($"v: {v}");
}
Console.WriteLine($"edges: {graph.CountEdges()}");


Graph graph2=new Graph();
graph2.GenerateGraph(10);
Console.WriteLine(graph2);
Console.WriteLine($"edges: {graph2.CountEdges()}");