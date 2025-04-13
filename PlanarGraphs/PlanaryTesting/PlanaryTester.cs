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
    private readonly int _vertexCount;
    
    public PlanaryTester(Graph graph)
    {
        _testingGraph = graph;
        _adjacencyList = graph.AdjacencyList;
        _allVertex = new HashSet<int>(_adjacencyList.Keys);
        _vertexCount = graph.VertexCount;
    }

    public bool BoyerMyrvoldPlanarity()
    { 
        var components = GetConnectedComponents();
        foreach (var component in components)
        {
            if (!IsPlanar(component))
                return false;
        }
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
        foreach (var VARIABLE in _dfsTree)
        {
            Console.WriteLine($"v:{VARIABLE.Key} tree {VARIABLE.Value}");
        }
        foreach (var VARIABLE in _low)
        {
            Console.WriteLine($"v:{VARIABLE.Key} low {VARIABLE.Value}");
        }

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

    public void Embed(int v)
    {
        if (_conflict) return;

        var pertinent = new List<Tuple<int, int>>();

        foreach (var w in _adjacencyList[v])
        {
            if (_parent[w] == v)
            {
                pertinent.Add(Tuple.Create(w, _low[w]));
            }
            else if (_dfsTree[w] < _dfsTree[v] && w != _parent[v])
            {
                pertinent.Add(Tuple.Create(w, _dfsTree[w]));
            }
        }

        pertinent.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        for (int i = 0; i < pertinent.Count && !_conflict; i++)
        {
            var (w, value) = pertinent[i];
            if (value < _dfsTree[v])
            {
                _stack.Push((v, w));
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
                        if (_dfsTree[y2] < _dfsTree[v] && _dfsTree[y] < _dfsTree[y2])
                        {
                            _conflict = true;
                            Console.WriteLine("Conflict found, graph is not planar.");
                            return;
                        }
                    }
                }
            }
        }
    }
}