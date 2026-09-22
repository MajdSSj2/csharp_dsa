Console.WriteLine("=== AVL Rotation Cases ===\n");

// ================= RR Case =================
Console.WriteLine("RR Case (Insert 1, 2, 3 → RotateLeft)");

/*
Before            After
------            -----
1                 2
 \               / \
  2             1   3
   \
    3
*/

var rbt = new RedBlackTree();
rbt.Insert(1);
rbt.Insert(2);
rbt.Insert(3);

rbt.Print();

Console.WriteLine("\n-------------------------\n");


// ================= LL Case =================
Console.WriteLine("LL Case (Insert 3, 2, 1 → RotateRight)");

/*
Before            After
------            -----
    3             2
   /             / \
  2             1   3
 /
1
*/

rbt = new RedBlackTree();
rbt.Insert(3);
rbt.Insert(2);
rbt.Insert(1);

rbt.Print();

Console.WriteLine("\n-------------------------\n");


// ================= LR Case =================
Console.WriteLine("LR Case (Insert 3, 1, 2 → RotateLeft + RotateRight)");

/*
Before            After
------            -----
    3             2
   /             / \
  1             1   3
   \
    2
*/

rbt = new RedBlackTree();
rbt.Insert(3);
rbt.Insert(1);
rbt.Insert(2);

rbt.Print();

Console.WriteLine("\n-------------------------\n");


// ================= RL Case =================
Console.WriteLine("RL Case (Insert 1, 3, 2 → RotateRight + RotateLeft)");

/*
Before            After
------            -----
1                 2
 \               / \
  3             1   3
 /
2
*/

rbt = new RedBlackTree();
rbt.Insert(1);
rbt.Insert(3);
rbt.Insert(2);

rbt.Print();

Console.WriteLine("\n-------------------------\n");

public class RedBlackTree
{
    private TreeNode? _root;

    // ===================== PUBLIC API =====================
    public void Insert(int value)
    {
        var node = new TreeNode(value);
        _root = Insert(_root, node);
        FixInsert(node);
    }

    // ===================== BST INSERT =====================
    private TreeNode Insert(TreeNode? root, TreeNode node)
    {
        if (root == null)
            return node;

        if (node.Value < root.Value)
        {
            root.Left = Insert(root.Left, node);
            root.Left.Parent = root;
        }
        else if (node.Value > root.Value)
        {
            root.Right = Insert(root.Right, node);
            root.Right.Parent = root;
        }

        return root;
    }

    // ===================== FIX VIOLATIONS =====================
    private void FixInsert(TreeNode node)
    {
        while (node != _root && node.Parent != null && node.Parent.Color == Color.Red)
        {
            var parent = node.Parent;
            var grand = parent.Parent;

            if (grand == null) break;

            if (parent == grand.Left)
            {
                var uncle = grand.Right;

                if (uncle != null && uncle.Color == Color.Red)
                {
                    parent.Color = Color.Black;
                    uncle.Color = Color.Black;
                    grand.Color = Color.Red;
                    node = grand;
                }
                else
                {
                    if (node == parent.Right)
                    {
                        node = parent;
                        RotateLeft(node);
                    }

                    parent.Color = Color.Black;
                    grand.Color = Color.Red;
                    RotateRight(grand);
                }
            }
            else
            {
                var uncle = grand.Left;

                if (uncle != null && uncle.Color == Color.Red)
                {
                    parent.Color = Color.Black;
                    uncle.Color = Color.Black;
                    grand.Color = Color.Red;
                    node = grand;
                }
                else
                {
                    if (node == parent.Left)
                    {
                        node = parent;
                        RotateRight(node);
                    }

                    parent.Color = Color.Black;
                    grand.Color = Color.Red;
                    RotateLeft(grand);
                }
            }
        }

        if (_root != null)
            _root.Color = Color.Black;
    }

    // ===================== ROTATIONS =====================
    private void RotateLeft(TreeNode x)
    {
        var y = x.Right!;
        x.Right = y.Left;

        if (y.Left != null)
            y.Left.Parent = x;

        y.Parent = x.Parent;

        if (x.Parent == null)
            _root = y;
        else if (x == x.Parent.Left)
            x.Parent.Left = y;
        else
            x.Parent.Right = y;

        y.Left = x;
        x.Parent = y;
    }

    private void RotateRight(TreeNode y)
    {
        var x = y.Left!;
        y.Left = x.Right;

        if (x.Right != null)
            x.Right.Parent = y;

        x.Parent = y.Parent;

        if (y.Parent == null)
            _root = x;
        else if (y == y.Parent.Left)
            y.Parent.Left = x;
        else
            y.Parent.Right = x;

        x.Right = y;
        y.Parent = x;
    }

    public void Print()
    {
        _root!.Print();
    }
}