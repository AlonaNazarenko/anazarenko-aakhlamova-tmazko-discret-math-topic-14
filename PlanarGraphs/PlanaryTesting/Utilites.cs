namespace PlanarGraphs;

public class Utilites
{
    public static void FillDictionary(Dictionary<int, int> dic, HashSet<int> what, int value)
    {
        foreach (var vertex in what) 
            dic[vertex] = value;
    }
    
    public static void FillDictionary(Dictionary<int, bool> dic, HashSet<int> what, bool value)
    {
        foreach (var vertex in what) 
            dic[vertex] = value;
    }
    
    public static void FillDictionary(Dictionary<int, int> dic, List<int> what, int value)
    {
        foreach (var vertex in what) 
            dic[vertex] = value;
    }
}