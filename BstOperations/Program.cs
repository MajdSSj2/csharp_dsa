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
}
