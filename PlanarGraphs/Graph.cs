namespace PlanarGraphs;

public class Graph
{
    public Dictionary<int, HashSet<int>> AdjacencyList { get; set; } = new();
    public int[,] Matrix { get; set; }
    public int VertexCount => AdjacencyList.Count;

    public Graph()
    {
    }

    public Graph(int vCount, double density)
    {
        Matrix = new int[vCount, vCount];
        Random random = new Random();

        int maxEdges = vCount * (vCount - 1) / 2;
        int requiredEdges = (int)(density * maxEdges);

        for (int i = 1; i < vCount; i++)
        {
            AddVertex(i+1);
        }
        
        HashSet<(int, int)> edgeSet = new HashSet<(int, int)>();

        while (edgeSet.Count < requiredEdges)
        {
            int u = random.Next(vCount)+1;
            int v = random.Next(vCount)+1;

            if (u == v)
                continue; 

            var edge = (Math.Min(u, v), Math.Max(u, v));

            if (edgeSet.Add(edge))
            {
                AddEdge(u,v);

            }


        }
        AdjacencyList = AdjacencyList.OrderBy(v => v.Key).ToDictionary();
        GenerateMatrixFromList();
    }

    public void GenerateMatrixFromList()
    {

        foreach (var pair in AdjacencyList)
        {
            int startV = pair.Key;
            foreach (int endV in pair.Value)
            {
                Matrix[startV-1, endV-1] = 1;
                Matrix[endV-1, startV-1] = 1;
            }
        }

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

   
    

    public void PrintMatrix()
    {
        int maxLabelWidth = VertexCount.ToString().Length + 1; 

        Console.Write(new string(' ', maxLabelWidth));
        for (int i = 1; i <= VertexCount; i++)
        {
            Console.Write(i.ToString().PadLeft(maxLabelWidth));
        }
        Console.WriteLine();

        Console.WriteLine(new string('-', maxLabelWidth + VertexCount * maxLabelWidth));
        
        for (int i = 0; i < VertexCount; i++)
        {
            Console.Write((i + 1).ToString().PadLeft(maxLabelWidth - 1) + "|");
            for (int j = 0; j < VertexCount; j++)
            {
                Console.Write(Matrix[i, j].ToString().PadLeft(maxLabelWidth));
            }
            Console.WriteLine();
        }
    
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