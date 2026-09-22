class BST
{
    public TreeNode? Root;

    // ---------- Insert (iterative) ----------
    // ===================== PUBLIC API =====================
    public void Insert(int value)
    {
        Root = Insert(Root, value);
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

        return node;
    }


    public void Print()
    {
        Root!.Print();
    }
}