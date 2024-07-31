using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class Node : IComparable<Node>
{
    public int val, data;
    public Node left, right;

    public Node(int val, int data = -1)
    {
        this.val = val;
        this.data = data;
        left = right = null;
    }

    public int CompareTo(Node other)
    {
        return val.CompareTo(other.val);
    }
}

namespace ImageEncryptCompress
{
    internal class Class1
    {
        public static Node redRoot;
        public static Node greenRoot;
        public static Node blueRoot;
        public static int cnntw = 0;
        public static string[] codedgreen = new string[300];
        public static string[] codedred = new string[300];
        public static string[] codedblue = new string[300];


        public static Node huff(PriorityQueue<Node> freq)
        {
            Node par, left, right;

            while (freq.Count != 1)
            {
                right = freq.Dequeue();
                left = freq.Dequeue();

                par = new Node(left.val + right.val);
                par.left = left;
                par.right = right;  
                freq.Enqueue(par);
            }

            return freq.Peek();
        }

        ///read length ==> read left ==> right <summary>
        /// read length ==> read left ==> right
        public static List<int>valr= new List<int>();
        public static List<int> datar = new List<int>();
        public static List<int> valg = new List<int>();
        public static List<int> datag = new List<int>();
        public static List<int> valb = new List<int>();
        public static List<int> datab = new List<int>();

        public static void PrintTree(Node root, BinaryWriter writer)
        {

            if (root == null || writer == null)
            {
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                writer.Write(current.val);
                writer.Write(current.data);

                if (current.left != null)
                {
                    queue.Enqueue(current.left);
                }

                if (current.right != null)
                {
                    queue.Enqueue(current.right);
                }
            }
        }
        public static void dfs(Node root, string huffcode, string[] coded, ref int nodes)
        {
            if (root == null) return;
            nodes++;
            if (root.data != -1)
            {
               
                coded[root.data] = huffcode;
                return;
            }

            if (root.left != null)
            {
                dfs(root.left, huffcode + "0", coded, ref nodes);
            }
            if (root.right != null)
            {
                dfs(root.right, huffcode + "1", coded, ref nodes);
            }
        }
        public static void yomen(RGBPixel[,] ImageMatrix)
        {
            int nodeb = 0, nodeg = 0, noder = 0;
            int[] greenFreq = new int[256];
            int[] blueFreq = new int[256];
            int[] redFreq = new int[256];

            Globals.height = ImageOperations.GetHeight(ImageMatrix);
            Globals.width = ImageOperations.GetWidth(ImageMatrix);
            foreach (var m in ImageMatrix)
            {
                greenFreq[m.green]++;
                blueFreq[m.blue]++;
                redFreq[m.red]++;
            }

            PriorityQueue<Node> freqg = new PriorityQueue<Node>();
            PriorityQueue<Node> freqb = new PriorityQueue<Node>();
            PriorityQueue<Node> freqr = new PriorityQueue<Node>();

            for (int i = 0; i < 256; i++)
            {
                if (greenFreq[i] > 0)
                {
                    Node tmp = new Node(greenFreq[i], i);
                    freqg.Enqueue(tmp);
                }
                if (blueFreq[i] > 0)
                {
                    Node tmp = new Node(blueFreq[i], i);
                    freqb.Enqueue(tmp);
                }
                if (redFreq[i] > 0)
                {
                    Node tmp = new Node(redFreq[i], i);
                    freqr.Enqueue(tmp);
                }
            }

            redRoot = huff(freqr);

            blueRoot = huff(freqb);

            greenRoot = huff(freqg);

            dfs(redRoot, "", codedred,ref noder);
            dfs(blueRoot, "", codedblue,ref nodeb);
            dfs(greenRoot, "", codedgreen,ref nodeg);

            long comsize = 0, orisize = 0;
            int cntb = 0, cntr = 0, cntg = 0;
            for (int i = 0; i < 256; i++)
            {
                if (!string.IsNullOrEmpty(codedgreen[i]))
                {
                    cntg += codedgreen[i].Length * greenFreq[i];
                    comsize += codedgreen[i].Length * greenFreq[i];
                    orisize += greenFreq[i] * 8;
                }
                if (!string.IsNullOrEmpty(codedblue[i]))
                {
                    cntb += codedblue[i].Length * blueFreq[i];
                    comsize += codedblue[i].Length * blueFreq[i];
                    orisize += blueFreq[i] * 8;
                }
                if (!string.IsNullOrEmpty(codedred[i]))
                {
                    cntr += codedred[i].Length * redFreq[i];
                    comsize += codedred[i].Length * redFreq[i];
                    orisize += redFreq[i] * 8;
                }
            }
            using (StreamWriter finalWriter = new StreamWriter(@"C:\Users\Tasneem\OneDrive\Desktop\algo\final.txt"))
            {
                finalWriter.WriteLine("compressed size");
                finalWriter.WriteLine((double)comsize / 8);
                finalWriter.WriteLine("compression ratio");
                finalWriter.WriteLine(((double)comsize / orisize) * 100);
            }

            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);


            StringBuilder rbuilder = new StringBuilder();
            StringBuilder gbuilder = new StringBuilder();
            StringBuilder bbuilder = new StringBuilder();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bbuilder.Append(codedblue[ImageMatrix[i, j].blue]);
                    rbuilder.Append(codedred[ImageMatrix[i, j].red]);
                    gbuilder.Append(codedgreen[ImageMatrix[i, j].green]);

                }
            }

            string r = rbuilder.ToString();
            string g = gbuilder.ToString();
            string b = bbuilder.ToString();


            using (BinaryWriter writer = new BinaryWriter(File.Open(Globals.tsnem, FileMode.Create)))
            {
                if (cntb % 8 == 0) cntb /= 8;
                else cntb = (cntb / 8) + 1;
                writer.Write(cntb);
                string tmp = "";
                for (int i = 0; i < b.Length; i++)
                {
                    tmp += b[i];
                    if (tmp.Length == 8)
                    {
                        byte firstByte = Convert.ToByte(tmp, 2);
                        writer.Write(firstByte);
                        tmp = "";
                    }
                }
                if (tmp.Length > 0)
                {
                    tmp = tmp.PadRight(8 - tmp.Length, '0');
                    byte lastByte = Convert.ToByte(tmp, 2);
                    writer.Write(lastByte);
                }

                if (cntr % 8 == 0) cntr /= 8;
                else cntr = (cntr / 8) + 1;
                writer.Write(cntr);
                tmp = "";
                for (int i = 0; i < r.Length; i++)
                {
                    tmp += r[i];
                    if (tmp.Length == 8)
                    {
                        byte firstByte = Convert.ToByte(tmp, 2);
                        writer.Write(firstByte);
                        tmp = "";
                    }
                }
                if (tmp.Length > 0)
                {
                    tmp = tmp.PadRight(8 - tmp.Length, '0');
                    byte lastByte = Convert.ToByte(tmp, 2);
                    writer.Write(lastByte);
                }

                if (cntg % 8 == 0) cntg /= 8;
                else cntg = (cntg / 8) + 1;
                writer.Write(cntg);
                tmp = "";
                for (int i = 0; i < g.Length; i++)
                {
                    tmp += g[i];
                    if (tmp.Length == 8)
                    {
                        byte firstByte = Convert.ToByte(tmp, 2);
                        writer.Write(firstByte);
                        tmp = "";
                    }
                }
                if (tmp.Length > 0)
                {
                    tmp = tmp.PadRight(8 - tmp.Length, '0');
                    byte lastByte = Convert.ToByte(tmp, 2);
                    writer.Write(lastByte);
                }


                PrintTree(blueRoot, writer);
                writer.Write(-2);

                PrintTree(greenRoot, writer);
                writer.Write(-2);

                PrintTree(redRoot, writer);

               writer.Write(-2);

            }
            Console.WriteLine(cnntw);
            Globals.com1 = blueRoot;
            Globals.com2 = greenRoot;
            Globals.com3 = redRoot;
        }
      
    }
}


