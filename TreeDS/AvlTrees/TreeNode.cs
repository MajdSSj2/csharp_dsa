class TreeNode
{
    public int Value;
    public TreeNode? Left;
    public TreeNode? Right;

    public int Height { get; set; } = 1;

    public TreeNode(int value)
    {
        Value = value;
    }
}