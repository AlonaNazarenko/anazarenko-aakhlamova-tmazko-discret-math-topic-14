namespace PlanarGraphs;

public class Graph
{
    public Dictionary<int, List<int>> AdjacencyList { get; set; } = new();
    public bool IsDirected { get; set; }

    public Graph(bool isDirected = false)
    {
        IsDirected = isDirected;
    }
    public void AddEdge(int startV, int endV)
    {
        AddVertex(startV);
        AddVertex(endV);
        AdjacencyList[startV].Add(endV);
        if (!IsDirected)
        {
            AdjacencyList[endV].Add(startV);
        }
    }

    public void AddVertex(int v)
    {
        if (!AdjacencyList.ContainsKey(v))
            AdjacencyList[v] = new List<int>();
    }

    public List<int> GetVertices()
    {
        return AdjacencyList.Keys.ToList();
    }

    public int CountEdges()
    {
        int count = 0;
        var uniqEdges = new HashSet<string>();

        foreach (var startV in AdjacencyList.Keys)
        {
            foreach (var endV in AdjacencyList[startV])
            {
                string key = IsDirected
                    ? $"{startV}->{endV}"
                    : $"{Math.Min(startV, endV)}-{Math.Max(startV, endV)}";

                if (!uniqEdges.Contains(key))
                {
                    uniqEdges.Add(key);
                    count++;
                }
            }
        }

        return count;
    }
    
}