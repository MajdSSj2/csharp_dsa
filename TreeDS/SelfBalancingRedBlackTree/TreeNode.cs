enum Color { Red, Black }

class TreeNode
{
    public int Value;
    public Color Color;
    public TreeNode? Left;
    public TreeNode? Right;
    public TreeNode? Parent;

    public TreeNode(int value)
    {
        Value = value;
        Color = Color.Red; // new nodes are red
    }
}