var bst = new BST();

bst.Insert(50);
bst.Insert(30);
bst.Insert(70);
bst.Insert(65);

bst.InOrderTraversal();
Console.WriteLine();
bst.PreOrderTraversal();
Console.WriteLine();
bst.PostOrderTraversal();

class BST
{
    private BstNode? _root;

    public void Insert(int value)
    {
        if (_root is null)
        {
            _root = new BstNode(value);
            return;
        }

        var current = _root;

        while (true)
        {
            if (value < current.Value)
            {
                if (current.Left is null)
                {
                    current.Left = new BstNode(value);
                    return;
                }

                current = current.Left;
            }
            else if (value > current.Value)
            {
                if (current.Right is null)
                {
                    current.Right = new BstNode(value);
                    return;
                }
                current = current.Right;
            }
            else
            {
                return;
            }
        }
    }

    public bool Search(int value)
    {
        if (_root is null)
        {
            return false;
        }

        BstNode? current = _root;
        while (current is not null)
        {
            if (current.Value == value)
            {
                return true;
            }

            current = value < current.Value ? current.Left : current.Right;
        }

        return false;
    }

    public void Delete(int value)
    {
        if (_root is null)
        {
            return;
        }

        var current = _root;
        BstNode? parent = null;
        //step #1: Locate the node
        while (current is not null && current.Value != value)
        {
            parent = current;

            current = value < current.Value ? current.Left : current.Right;
        }

        if (current is null)
        {
            return;
        }

        //step #2: two children

        if (current.Left is not null && current.Right is not null)
        {
            var successorParent = current;
            var successor = current.Right;

            while (successor.Left is not null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            current.Value = successor.Value;
            current = successor;
            parent = successorParent;
        }

        //step #3: 1, 0 children
        BstNode? child = current.Left ?? current.Right;

        if (parent is null)
        {
            _root = child;
        }
        else if (parent.Left == current)
        {
            parent.Left = child;
        }
        else
        {
            parent.Right = child;
        }
    }

    // (left -> root -> right)
    public void InOrderTraversal()
    {
        if (_root is null)
            return;

        var current = _root;

        var stack = new Stack<BstNode>();

        while (current is not null || stack.Count > 0)
        {
            while (current is not null)
            {
                stack.Push(current);
                current = current.Left;
            }

            current = stack.Pop();
            Console.Write($"{current.Value} -> ");
            current = current.Right;
        }
    }

    // (root -> left -> right)
    public void PreOrderTraversal()
    {
        if (_root is null)
            return;

        var stack = new Stack<BstNode>();
        var current = _root;
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();
            Console.Write($"{current.Value} -> ");

            if (current.Right is not null)
            {
                stack.Push(current.Right);
            }

            if (current.Left is not null)
            {
                stack.Push(current.Left);
            }
        }
    }

    //(Left -> right -> root)
    public void PostOrderTraversal()
    {
        if (_root is null)
            return;

        var stack1 = new Stack<BstNode>();
        var stack2 = new Stack<BstNode>();

        stack1.Push(_root);

        while (stack1.Count > 0)
        {
            var current = stack1.Pop();
            stack2.Push(current);

            if (current.Left is not null)
            {
                stack1.Push(current.Left);
            }

            if (current.Right is not null)
            {
                stack1.Push(current.Right);
            }
        }

        while (stack2.Count > 0)
        {
            Console.Write($"{stack2.Pop().Value} -> ");
        }
    }
}
