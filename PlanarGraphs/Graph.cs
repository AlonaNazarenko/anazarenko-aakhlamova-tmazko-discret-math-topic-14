namespace PlanarGraphs;

public class Graph
{
    public Dictionary<int, List<int>> AdjacencyList { get; set; } = new();
    public bool IsDirected { get; set; }
    public int[,] Matrix{ get; set; }
    public int VertexCount => AdjacencyList.Count;
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

    public void GenerateGraph(int vCount)
    {
        Random rand = new Random();
        int[,] matrix = new int[vCount, vCount];
        
        for (int i = 0; i < vCount; i++)
        {
            for (int j = 0; j < vCount; j++)
            {
                int randomBit = rand.Next(2);
                if (!IsDirected)
                {
                    if (j > i) break;
                    matrix[i, j] = randomBit;
                    matrix[j, i] = randomBit;
                }
                else
                {
                    matrix[i, j] = randomBit;
                }
            }
        }

        Matrix = matrix;
        GenerateListFromMatrix(vCount);
    }

    public void GenerateListFromMatrix(int vCount)
    {
        for (int i = 0; i < vCount; i++)
        {
            for (int j = 0; j < vCount; j++)
            {
                if(Matrix[i,j]==1)
                    AddEdge(i + 1, j + 1);
            }
        }

        AdjacencyList=AdjacencyList.OrderBy(v => v.Key).ToDictionary();
    }

    public override string ToString()
    {
        string res = "";
        foreach (var v in AdjacencyList)
        {
            res += $"{v.Key} | {string.Join(", ", v.Value)}\n";
        }

        return res;
    }
}