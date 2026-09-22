static class BTreePrinter
{
    class NodeInfo
    {
        public TreeNode Node = default!;     // assigned immediately
        public string Text = string.Empty;

        public int StartPos;
        public int Size => Text.Length;

        public int EndPos
        {
            get => StartPos + Size;
            set => StartPos = value - Size;
        }

        public NodeInfo? Parent;
        public NodeInfo? Left;
        public NodeInfo? Right;
    }

    public static void Print(this TreeNode root, string textFormat = "0",
        int spacing = 1, int topMargin = 2, int leftMargin = 2)
    {
        if (root == null) return;

        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo>();
        TreeNode? next = root;

        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo
            {
                Node = next,
                Text = next.Value.ToString(textFormat)
            };

            if (level < last.Count)
            {
                item.StartPos = last[level].EndPos + spacing;
                last[level] = item;
            }
            else
            {
                item.StartPos = leftMargin;
                last.Add(item);
            }

            if (level > 0)
            {
                item.Parent = last[level - 1];

                if (next == item.Parent.Node.Left)
                {
                    item.Parent.Left = item;
                    item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                }
                else
                {
                    item.Parent.Right = item;
                    item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                }
            }

            next = next.Left ?? next.Right;

            for (; next == null; item = item.Parent!)
            {
                int top = rootTop + 2 * level;

                Print(item.Text, top, item.StartPos);

                if (item.Left != null)
                {
                    Print("/", top + 1, item.Left.EndPos);
                    Print("_", top, item.Left.EndPos + 1, item.StartPos);
                }

                if (item.Right != null)
                {
                    Print("_", top, item.EndPos, item.Right.StartPos - 1);
                    Print("\\", top + 1, item.Right.StartPos - 1);
                }

                if (--level < 0) break;

                if (item.Parent != null && item == item.Parent.Left)
                {
                    item.Parent.StartPos = item.EndPos + 1;
                    next = item.Parent.Node.Right;
                }
                else if (item.Parent != null)
                {
                    if (item.Parent.Left == null)
                        item.Parent.EndPos = item.StartPos - 1;
                    else
                        item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                }
            }
        }

        int finalTop = rootTop + 2 * last.Count - 1;

        if (finalTop >= 0 && finalTop < Console.BufferHeight)
            Console.SetCursorPosition(0, finalTop);
    }

    private static void Print(string s, int top, int left, int right = -1)
    {
        if (top < 0 || top >= Console.BufferHeight)
            return;

        if (left < 0 || left >= Console.BufferWidth)
            return;

        if (right < 0)
            right = left + s.Length;

        // prevent overflow to the right
        right = Math.Min(right, Console.BufferWidth);

        Console.SetCursorPosition(left, top);

        while (Console.CursorLeft < right)
            Console.Write(s);
    }
}