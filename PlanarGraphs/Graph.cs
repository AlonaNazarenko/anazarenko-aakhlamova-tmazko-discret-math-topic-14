namespace PlanarGraphs;

public class Graph
{
    public Dictionary<int, HashSet<int>> AdjacencyList { get; set; } = new();
    public int[,] Matrix { get; set; }
    public int VertexCount => AdjacencyList.Count;

    public Graph()
    {
    }

    public void AddEdge(int startV, int endV)
    {
        AddVertex(startV);
        AddVertex(endV);
        AdjacencyList[startV].Add(endV);

        AdjacencyList[endV].Add(startV);
    }

    public void AddVertex(int v)
    {
        if (!AdjacencyList.ContainsKey(v))
            AdjacencyList[v] = new HashSet<int>();
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
                string key = $"{Math.Min(startV, endV)}-{Math.Max(startV, endV)}";


                uniqEdges.Add(key);
            }
        }

        return uniqEdges.Count;
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

                if (j > i) break;
                matrix[i, j] = randomBit;
                matrix[j, i] = randomBit;
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
                if (Matrix[i, j] == 1)
                    AddEdge(i + 1, j + 1);
            }
        }

        AdjacencyList = AdjacencyList.OrderBy(v => v.Key).ToDictionary();
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