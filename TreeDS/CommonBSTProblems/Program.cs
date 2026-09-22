var bst = new BST();

// Build BST
bst.Insert(10);
bst.Insert(5);
bst.Insert(15);
bst.Insert(2);
bst.Insert(7);
bst.Insert(12);
bst.Insert(20);

bst.Print();

// Use bst.Root
Console.WriteLine("Validate BST: " + ValidateBST(bst.Root));

Console.WriteLine("Min: " + FindMin(bst.Root));
Console.WriteLine("Max: " + FindMax(bst.Root));

Console.WriteLine("Height: " + FindHeight(bst.Root));

Console.WriteLine("Kth Smallest (k=3): " + KthSmallest(bst.Root, 3));

var range = RangeQuery(bst.Root, 5, 15);
Console.WriteLine("Range [5,15]: " + string.Join(", ", range));

// =========================================================
// 1) Validate BST (Iterative InOrder → strictly increasing)
// =========================================================

bool ValidateBST(TreeNode? root)
{
    var stack = new Stack<TreeNode>();
    var current = root;

    int? prev = null;

    while (current != null || stack.Count > 0)
    {
        while (current != null)
        {
            stack.Push(current);
            current = current.Left;
        }

        current = stack.Pop();

        if (prev != null && current.Value <= prev)
            return false;

        prev = current.Value;
        current = current.Right;
    }

    return true;
}

// =========================================================
// 2) Find Min (left-most)
// =========================================================
int? FindMin(TreeNode? root)
{
    if (root == null) return null;

    var current = root;

    while (current.Left != null)
        current = current.Left;

    return current.Value;
}

// =========================================================
// 3) Find Max (right-most)
// =========================================================
int? FindMax(TreeNode? root)
{
    if (root == null) return null;

    var current = root;

    while (current.Right != null)
        current = current.Right;

    return current.Value;
}

// =========================================================
// 4) Find Height (BFS Level Order)
// Height = number of edges (root alone = 0)
// =========================================================
int FindHeight(TreeNode? root)
{
    if (root == null) return -1;

    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    int height = -1;

    while (queue.Count > 0)
    {
        int levelSize = queue.Count;
        height++;

        for (int i = 0; i < levelSize; i++)
        {
            var node = queue.Dequeue();

            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
    }

    return height;
}

// =========================================================
// 5) Kth Smallest (Iterative InOrder)
// =========================================================
int? KthSmallest(TreeNode? root, int k)
{
    var stack = new Stack<TreeNode>();
    var current = root;

    int count = 0;

    while (current != null || stack.Count > 0)
    {
        while (current != null)
        {
            stack.Push(current);
            current = current.Left;
        }

        current = stack.Pop();
        count++;

        if (count == k)
            return current.Value;

        current = current.Right;
    }

    return null;
}

// =========================================================
// 6) Range Query [low, high] (Iterative with pruning)
// =========================================================
static List<int> RangeQuery(TreeNode? root, int low, int high)
{
    var result = new List<int>();
    var stack = new Stack<TreeNode>();

    if (root != null)
        stack.Push(root);

    while (stack.Count > 0)
    {
        var node = stack.Pop();

        if (node.Value >= low && node.Value <= high)
            result.Add(node.Value);

        if (node.Value > low && node.Left != null)
            stack.Push(node.Left);

        if (node.Value < high && node.Right != null)
            stack.Push(node.Right);
    }

    return result;
}