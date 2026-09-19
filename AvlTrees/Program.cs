var avl = new AVLTree();

avl.Insert(30);
avl.Insert(10);
avl.Insert(20);

avl.Print();

class AVLTree
{
    private TreeNode? _root;

    // Insert
    public void Insert(int value)
    {
        _root = Insert(_root, value);
    }

    private TreeNode Insert(TreeNode? node, int value)
    {
        if (node is null)
            return new TreeNode(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
        else
            return node;

        return Balance(node);
    }

    public void Delete(int value)
    {
        _root = Delete(_root, value);
    }

    private TreeNode? Delete(TreeNode? node, int value)
    {
        // Value does not exist
        if (node is null)
            return null;

        // Search left
        if (value < node.Value)
        {
            node.Left = Delete(node.Left, value);
        }
        // Search right
        else if (value > node.Value)
        {
            node.Right = Delete(node.Right, value);
        }
        // Found the node
        else
        {
            // Case 1 + Case 2:
            // No left child
            if (node.Left is null)
                return node.Right;

            // No right child
            if (node.Right is null)
                return node.Left;

            // Case 3:
            // Node has TWO children
            var successor = FindMin(node.Right);

            // Copy successor's value into current node
            node.Value = successor.Value;

            // Delete the original successor
            node.Right = Delete(node.Right, successor.Value);
        }

        return Balance(node);
    }

    private TreeNode FindMin(TreeNode node)
    {
        while (node.Left is not null)
        {
            node = node.Left;
        }

        return node;
    }

    // Search
    public bool Contains(int value)
    {
        var current = _root;

        while (current is not null)
        {
            if (current.Value == value)
                return true;

            current = value < current.Value ? current.Left : current.Right;
        }

        return false;
    }

    // Returns the height of a node
    // If the node is null → height = 0 (base case for AVL calculations)
    int GetHeight(TreeNode? node)
    {
        if (node == null)
            return 0;

        return node.Height;
    }

    // Updates the height of the current node
    // Height = max(left subtree, right subtree) + 1 (for the current node)
    void UpdateHeight(TreeNode node)
    {
        node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    // Calculates the Balance Factor of a node
    // Balance Factor = Height(Left) - Height(Right)
    // Used to detect if the node is balanced, left-heavy, or right-heavy
    int BalanceFactor(TreeNode node)
    {
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    // Performs a Right Rotation (LL case fix)
    // y is the unbalanced node (root of the subtree)
    TreeNode RotateRight(TreeNode y)
    {
        var x = y.Left!;
        var t2 = x.Right;

        x.Right = y;
        y.Left = t2;

        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    // Performs a Left Rotation (RR case fix)
    // x is the unbalanced node (root of the subtree)

    TreeNode RotateLeft(TreeNode x)
    {
        var y = x.Right!;
        var t2 = y.Left;

        y.Left = x;
        x.Right = t2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    // Balances the current node after insertion or deletion
    // This is the core of AVL: detect imbalance and fix it using rotations
    TreeNode Balance(TreeNode node)
    {
        UpdateHeight(node);

        int bf = BalanceFactor(node);

        if (bf > 1 && BalanceFactor(node.Left!) >= 0)
        {
            return RotateRight(node);
        }
        else if (bf > 1 && BalanceFactor(node.Left!) < 0)
        {
            node.Left = RotateLeft(node.Left!);

            return RotateRight(node);
        }
        else if (bf < -1 && BalanceFactor(node.Right!) <= 0)
        {
            return RotateLeft(node);
        }
        else if (bf < -1 && BalanceFactor(node.Right!) > 0)
        {
            node.Right = RotateRight(node.Right!);
            return RotateLeft(node);
        }

        return node;
    }

    public void Print()
    {
        if (_root != null)
            _root.Print();
    }
}
