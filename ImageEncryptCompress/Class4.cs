using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ImageEncryptCompress
{
    internal class Class4
    {
        public static string encodedB = "", encodedG = "", encodedR = ""; // Initialize encodedB, encodedG, and encodedR
        public static byte[] fileBytes;
        public static Node blueRoot, greenRoot, redRoot;
        public static RGBPixel[,] leila(string freak)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(freak)))
            {

                int cntbSize = reader.ReadInt32();

                StringBuilder bBuilder = new StringBuilder();
                for (int i = 0; i < cntbSize; i++)
                {
                    byte intValue = reader.ReadByte();
                    string binaryString = Convert.ToString(intValue, 2).PadLeft(8, '0');
                    bBuilder.Append(binaryString);
                }

                // Convert binary string to original string
                encodedB = bBuilder.ToString();
                int cntrSize = reader.ReadInt32();

                StringBuilder rBuilder = new StringBuilder();
                for (int i = 0; i < cntrSize; i++)
                {
                    byte intValue = reader.ReadByte();
                    // Convert intValue to binary string and append to StringBuilder
                    string binaryString = Convert.ToString(intValue, 2).PadLeft(8, '0');
                    rBuilder.Append(binaryString);
                }

                // Convert binary string to original string
                encodedR = rBuilder.ToString();

                int cntgSize = reader.ReadInt32();

                StringBuilder gBuilder = new StringBuilder();
                for (int i = 0; i < cntgSize; i++)
                {
                    byte intValue = reader.ReadByte();
                    // Convert intValue to binary string and append to StringBuilder
                    string binaryString = Convert.ToString(intValue, 2).PadLeft(8, '0');
                    gBuilder.Append(binaryString);
                }

                encodedG = gBuilder.ToString();

                blueRoot = BuildTree(reader);
                greenRoot = BuildTree(reader);
                redRoot = BuildTree(reader);
                //while (reader.BaseStream.Position != reader.BaseStream.Length)
                //{
                //    reader.ReadInt32(); // Read a value
                //    cnt++;
                //}

            }
            Console.WriteLine(cnt);
            Console.WriteLine(com(redRoot, Globals.com3));
            Console.WriteLine(com(greenRoot, Globals.com2));
            Console.WriteLine(com(blueRoot, Globals.com1));
            //blueRoot = Globals.com1;
            //redRoot = Globals.com3;
            //greenRoot= Globals.com2;
            RGBPixel[,] decompressedImage = new RGBPixel[Globals.height, Globals.width];
            int row = 0, col = 0;
            Node tmp1 = blueRoot;
            for (int i = 0; i < encodedB.Length; i++)
            {
                if (tmp1.data != -1)
                {
                    decompressedImage[row, col].blue = (byte)tmp1.data;
                    col++;
                    if (col == Globals.width)
                    {
                        col = 0;
                        row++;
                    }
                    if (row == Globals.height)
                    {
                        break;
                    }
                    tmp1 = blueRoot;
                    i--;
                    continue;
                }
                if (encodedB[i] == '0') tmp1 = tmp1.left;
                else tmp1 = tmp1.right;
            }
            tmp1 = greenRoot;
            row = col = 0;
            for (int i = 0; i < encodedG.Length; i++)
            {
                if (tmp1.data != -1)
                {
                    decompressedImage[row, col].green = (byte)tmp1.data;
                    col++;
                    if (col == Globals.width)
                    {
                        col = 0;
                        row++;
                    }
                    if (row == Globals.height)
                    {
                        break;
                    }
                    tmp1 = greenRoot;
                    i--;
                    continue;
                }
                if (encodedG[i] == '0') tmp1 = tmp1.left;
                else tmp1 = tmp1.right;
            }
            row = col = 0;
            tmp1 = redRoot;
            for (int i = 0; i < encodedR.Length; i++)
            {
                if (tmp1.data != -1)
                {
                    decompressedImage[row, col].red = (byte)tmp1.data;
                    col++;
                    if (col == Globals.width)
                    {
                        col = 0;
                        row++;
                    }
                    if (row == Globals.height)
                    {
                        break;
                    }
                    tmp1 = redRoot;
                    i--;
                    continue;
                }
                if (encodedR[i] == '0') tmp1 = tmp1.left;
                else tmp1 = tmp1.right;
            }

           
            return decompressedImage;
        }
        public static int cnt = 0;
        public static Node BuildTree(BinaryReader reader)
        {
            Node root = null;
            bool stopReading = false;

            while (!stopReading && reader.BaseStream.Position != reader.BaseStream.Length)
            {
                int val = reader.ReadInt32();
                if (val == -2)
                {
                    stopReading = true;
                    break;
                }

                int data = reader.ReadInt32();

                if (root == null)
                {
                    root = new Node(val, data);
                }
                else
                {
                    Insert(root, val, data);
                }
            }

            return root;
        }

        public static void Insert(Node root, int val, int data)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                if (current.left == null)
                {
                    current.left = new Node(val, data);
                    return;
                }
                else
                {
                    queue.Enqueue(current.left);
                }

                if (current.right == null)
                {
                    current.right = new Node(val, data);
                    return;
                }
                else
                {
                    queue.Enqueue(current.right);
                }
            }
        }

    
        public static bool com(Node root1, Node root2)
        {
            if (root1 == null && root2 == null)
                return true;

            if (root1 == null || root2 == null)
            {
                Console.WriteLine("hhhhhhhhhhhh");
                return false;
            }

            if (root1.val != root2.val)
            {
                Console.WriteLine(root1.val + " " + root2.val);
                return false;
            }

            if (root1.data != root2.data)
            {
                Console.WriteLine(root1.data + " " + root2.data);

                return false;
            }
            // Recursively compare the left and right subtrees
            return com(root1.left, root2.left) && com(root1.right, root2.right);
        }


    }
}
