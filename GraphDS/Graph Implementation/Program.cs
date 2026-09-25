//Adjacency List

var graphAdjacencyList = new Dictionary<string, HashSet<string>>
{
    ["A"] = new() { "B", "C" },
    ["B"] = new() { "A", "D" },
    ["C"] = new() { "A", "D" },
    ["D"] = new() { "B", "C" },
};

// usage
Console.WriteLine("Adjacency List");

foreach (var node in graphAdjacencyList)
{
    Console.WriteLine($"{node.Key} -> {string.Join(", ", node.Value)}");
}

Console.WriteLine($"A connected to B ? {graphAdjacencyList["A"].Contains("B")}");
Console.WriteLine();

// Adjacency Matrix (array)

string[] nodes = { "A", "B", "C", "D" };
int[,] graphArray =
{
    { 0, 1, 1, 0 },
    { 1, 0, 0, 1 },
    { 1, 0, 0, 1 },
    { 0, 1, 1, 0 },
};

Console.WriteLine("Adjacency Matrix");

for (int i = 0; i < nodes.Length; i++)
{
    for (int j = 0; j < nodes.Length; j++)
    {
        Console.Write(graphArray[i, j] + " ");
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine($"A connected to B ? {graphArray[0, 1] == 1}");

void AddNode(Dictionary<string, HashSet<string>> graph, string node)
{
    if (!graph.ContainsKey(node))
        graph[node] = new HashSet<string>();
}

void AddEdge(Dictionary<string, HashSet<string>> graph, string from, string to)
{
    AddNode(graph, from);
    AddNode(graph, to);

    graph[from].Add(to);
    graph[to].Add(from);
}

void RemoveEdge(Dictionary<string, HashSet<string>> graph, string from, string to)
{
    if (graph[from].Contains(to))
        graph[from].Remove(to);

    if (graph[to].Contains(from))
        graph[to].Remove(from);
}

void RemoveNode(Dictionary<string, HashSet<string>> graph, string node)
{
    if (!graph.ContainsKey(node))
        return;

    foreach (var neighbour in graph[node])
    {
        graph[neighbour].Remove(node);
    }

    graph.Remove(node);
}

bool HasEdge(Dictionary<string, HashSet<string>> graph, string from, string to)
{
    return graph.ContainsKey(from) && graph[from].Contains(to);
}
