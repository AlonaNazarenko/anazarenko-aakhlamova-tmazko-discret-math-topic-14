using System.Diagnostics;

namespace PlanarGraphs.PlanaryTesting;

public class PlanaryTester
{
    private Graph _testingGraph;
    private readonly Dictionary<int, HashSet<int>> _adjacencyList;
    private readonly HashSet<int> _allVertex;
    private Dictionary<int, int> _dfsTree; 
    private Dictionary<int, int> _low;
    private Dictionary<int, int> _parent;
    private Dictionary<int, List<(int, int)>> _embedding; 
    private Stack<(int, int)> _stack; 
    private HashSet<(int, int)> _visited; 
    private bool _conflict; 
    private int _dfsIndex; 
    
    public PlanaryTester(Graph graph)
    {
        _testingGraph = graph;
        _adjacencyList = graph.AdjacencyList;
        _allVertex = new HashSet<int>(_adjacencyList.Keys);
    }

    public bool BoyerMyrvoldPlanarity()
    {
        Stopwatch timer = new();
        timer.Start();
        var components = GetConnectedComponents();
        foreach (var component in components)
        {
            if (!IsPlanar(component))
            {
                timer.Stop();
                Console.WriteLine($"Time: {timer.ElapsedMilliseconds}ms");
                return false;
            }
        }
        timer.Stop();
        Console.WriteLine($"Time: {timer.ElapsedMilliseconds}ms");
        return true;
    }

    private List<List<int>> GetConnectedComponents()
    {
        List<List<int>> components = new();
        Dictionary<int, bool> visited = new();
        Utilites.FillDictionary(visited, _allVertex, false);
        foreach (int v in _allVertex)
        {
            if (!visited[v])
            {
                List<int> component = new();
                Component(v, visited, component);
                components.Add(component);
            }
        }
        return components;
    }

    private void Component(int v, Dictionary<int, bool> visited, List<int> component)
    {
        visited[v] = true;
        component.Add(v);
        foreach (int neighbourhood in _adjacencyList[v])
            if (!visited[neighbourhood])
                Component(neighbourhood, visited, component);
    }

    private bool IsPlanar(List<int> vertices)
    {
        _dfsTree = new();
        _low = new();
        _parent = new();
        _embedding = new();
        _stack = new();
        _visited = new();
        _conflict = false;
        _dfsIndex = 0;
        Utilites.FillDictionary(_dfsTree, vertices, -1);
        Utilites.FillDictionary(_low, vertices, -1);
        Utilites.FillDictionary(_parent, vertices, -1);

        foreach (var v in vertices)
            _embedding[v] = new List<(int, int)>();
        foreach (var v in vertices)
        {
            if (_dfsTree[v] == -1)
                DFS(v);
        }
        _visited.Clear();
        var postOrder = vertices.OrderByDescending(v => _dfsTree[v]).ToList();

        foreach (var v in postOrder)
        {
            Embed(v);
            if (_conflict)
                return false;
        }

        return true;
    }

    private void DFS(int v)
    {
        _dfsTree[v] = _dfsIndex;
        _low[v] = _dfsIndex;
        _dfsIndex++;

        foreach (var w in _adjacencyList[v])
        {
            var edge = (Math.Min(v, w), Math.Max(v, w));
            if (!_visited.Contains(edge))
            {
                _visited.Add(edge);
                if (_dfsTree[w] == -1)
                {
                    _parent[w] = v;
                    DFS(w);
                    _low[v] = Math.Min(_low[v], _low[w]);
                }
                else if(w != _parent[v])
                {
                    _low[v] = Math.Min(_low[v], _dfsTree[w]);
                }
            }
        }
    }

    private void Embed(int v)
     {
         if (_conflict) return;
 
         _embedding[v].Clear();
         var pertinent = new List<(int w, int value)>();
         
         foreach (var w in _adjacencyList[v])
         {
             if (_parent[w] == v) 
             {
                 pertinent.Add((w, _low[w]));
             }
             else if (_dfsTree[w] < _dfsTree[v] && w != _parent[v]) 
             {
                 pertinent.Add((w, _dfsTree[w]));
             }
         }
         
         pertinent.Sort((a, b) => a.value.CompareTo(b.value));
 
         while (pertinent.Any() && !_conflict)
         {
             var (w, value) = pertinent[pertinent.Count - 1];
             pertinent.RemoveAt(pertinent.Count - 1);
 
             if (value < _dfsTree[v]) 
             {
                 _stack.Push((v, w));
                 _embedding[v].Add((v, w));
             }
             else 
             {
                 while (_stack.Any())
                 {
                     var (x, y) = _stack.Peek();
                     if (_dfsTree[y] >= _dfsTree[v])
                         break;
                     _stack.Pop();
                     if (_stack.Any())
                     {
                         var (x2, y2) = _stack.Peek();
                         if (y2 < _dfsTree[v] && y < y2)
                         {
                             _conflict = true;
                             return;
                         }
                     }
                 }
                 _embedding[v].Add((v, w));
             }
         }
 
         if (_stack.Any())
         {
             var checkY = -1;
             foreach (var (x, y) in _stack)
             {
                 if (_dfsTree[y] < _dfsTree[v])
                 {
                     if (checkY != -1 && y < checkY)
                     {
                         _conflict = true;
                         return;
                     }
                     checkY = y;
                 }
             }
         }
     }
}