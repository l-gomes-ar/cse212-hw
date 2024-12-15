public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        if (value != Data) {
            // Value is not a duplicate
            if (value < Data)
            {
                // Insert to the left
                if (Left is null)
                    Left = new Node(value);
                else
                    Left.Insert(value);
            }
            else
            {
                // Insert to the right
                if (Right is null)
                    Right = new Node(value);
                else
                    Right.Insert(value);
            }
        }
    }

    public bool Contains(int value)
    {
        if (value < Data && Left is not null)
            // Check left side 
            return Left.Contains(value);
        else if (value > Data && Right is not null)
            // Check right side 
            return Right.Contains(value);
        else
            // Return the comparison
            return value == Data;
    }

    public int GetHeight()
    {
        if (Left is null & Right is null)
            // If this is the last node, return height 1
            return 1;

        // Initiate left and right height to 0, and update them, if not null
        var leftHeight = 0;
        var rightHeight = 0;

        if (Left is not null)
            leftHeight = Left.GetHeight();

        if (Right is not null)
            rightHeight = Right.GetHeight();

        // Compare values and return the 1 + greater Height (or either if equal)
        if (leftHeight <= rightHeight)
            return 1 + rightHeight;
        else
            return 1 + leftHeight;
    }
}