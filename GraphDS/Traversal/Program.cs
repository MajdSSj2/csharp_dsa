var graph = new Dictionary<string, HashSet<string>>()
{
    ["A"] = new() { "B", "C" },
    ["B"] = new() { "A", "D" },
    ["C"] = new() { "A", "D" },
    ["D"] = new() { "B", "C" },
};

Console.WriteLine("===========DFS===========");
DFS("A");

Console.WriteLine();
Console.WriteLine();

Console.WriteLine("===========BFS===========");
BFS("A");

void DFS(string from)
{
    var visisted = new HashSet<string>();
    var stack = new Stack<string>();

    stack.Push(from);

    while (stack.Count > 0)
    {
        var node = stack.Pop();

        if (!visisted.Add(node))
            continue;

        Console.Write($"node: {node} ");

        foreach (var neighbour in graph[node])
        {
            stack.Push(neighbour);
        }
    }
}

void BFS(string from)
{
    var visisted = new HashSet<string>();
    var queue = new Queue<string>();

    queue.Enqueue(from);
    visisted.Add(from);

    while (queue.Count > 0)
    {
        var node = queue.Dequeue();

        Console.Write($"node: {node} ");

        foreach (var neighbour in graph[node])
        {
            if (visisted.Add(neighbour))
                queue.Enqueue(neighbour);
        }
    }
}
