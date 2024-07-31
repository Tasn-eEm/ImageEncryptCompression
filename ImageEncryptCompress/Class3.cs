using System;
using System.IO;

public class DataWriter<T>
{
    public void WriteToFile(Node root, string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            WriteNode(writer, root);
        }
    }

    private void WriteNode(BinaryWriter writer, Node node)
    {
        if (node == null)
        {
            // Write a marker for null node
            writer.Write(-1);
            return;
        }

        // Write node value
        writer.Write(node.val);

        // Recursively write left and right nodes
        WriteNode(writer, node.left);
        WriteNode(writer, node.right);
    }
}
