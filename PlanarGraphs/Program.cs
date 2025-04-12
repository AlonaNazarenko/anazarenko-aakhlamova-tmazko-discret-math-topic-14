// See https://aka.ms/new-console-template for more information

using PlanarGraphs;using PlanarGraphs.PlanaryTesting;

Graph graph=new Graph();
graph.AddEdge(1,2);
graph.AddEdge(1,3);
graph.AddEdge(3,2);
graph.AddEdge(4,2);
graph.AddEdge(3,5);
graph.AddEdge(2,5);
graph.AddEdge(2,4);
graph.AddVertex(13);
Console.WriteLine(graph);
var vertexes = graph.GetVertices();
foreach (var v in vertexes)
{
    Console.WriteLine($"v: {v}");
}
Console.WriteLine($"edges: {graph.CountEdges()}");
//new PlanaryTester(graph).BoyerMyrvoldPlanarity();


Graph graph2=new Graph();
graph2.GenerateGraph(10);
Console.WriteLine(graph2);
Console.WriteLine($"edges: {graph2.CountEdges()}");

Graph graph3=new Graph(20,0.6);
Console.WriteLine(graph3);