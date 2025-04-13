// See https://aka.ms/new-console-template for more information

using PlanarGraphs;using PlanarGraphs.PlanaryTesting;

Graph graph =new Graph();
graph.AddEdge(1,2);
graph.AddEdge(1,3);
graph.AddEdge(3,2);
graph.AddEdge(4,2);
graph.AddEdge(3,5);
graph.AddEdge(2,5);
graph.AddEdge(2,4);
graph.AddVertex(13);
// Console.WriteLine(graph);
// Граф K_5
Graph k5 = new Graph();
k5.AddEdge(1, 2);
k5.AddEdge(1, 3);
k5.AddEdge(1, 4);
k5.AddEdge(1, 5);
k5.AddEdge(2, 3);
k5.AddEdge(2, 4);
k5.AddEdge(2, 5);
k5.AddEdge(3, 4);
k5.AddEdge(3, 5);
k5.AddEdge(4, 5);

// Граф K_{3,3}
Graph k33 = new();
k33.AddEdge(1, 4);
k33.AddEdge(1, 5);
k33.AddEdge(1, 6);
k33.AddEdge(2, 4);
k33.AddEdge(2, 5);
k33.AddEdge(2, 6);
k33.AddEdge(3, 4);
k33.AddEdge(3, 5);
k33.AddEdge(3, 6);


// var vertexes = graph.GetVertices();
// foreach (var v in vertexes)
// {
//     Console.WriteLine($"v: {v}");
// }
// Console.WriteLine($"edges: {graph.CountEdges()}");

// Utilites.PrintPlanarity(graph);
// Utilites.PrintPlanarity(k5);
// Utilites.PrintPlanarity(k33);


// Graph graph2=new Graph();
// graph2.GenerateGraph(10);
// Console.WriteLine(graph2);
// Console.WriteLine($"edges: {graph2.CountEdges()}");
//
Graph graph3 = new Graph(1000, 0.1);
// Console.WriteLine(graph3);
Utilites.PrintPlanarity(graph3);