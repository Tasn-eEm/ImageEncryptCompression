using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start();
            //RGBPixel[,] image = Class5.EncryptImage(ImageMatrix, textBox2.Text, Convert.ToInt32(textBox1.Text));
            Class1.yomen(ImageMatrix);
            //ImageOperations.DisplayImage(image, pictureBox2);
            stopwatch1.Stop();
           // MessageBox.Show("Time: " + stopwatch1.Elapsed.ToString(@"mm\:ss\:fff"));
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();
            stopwatch2.Start();
            //RGBPixel[,] deenc;
            RGBPixel[,] decompress = Class4.leila(Globals.tsnem);
           // deenc = Class5.EncryptImage(decompress, textBox2.Text, Convert.ToInt32(textBox1.Text));
            ImageOperations.DisplayImage(decompress, pictureBox2);
            stopwatch2.Stop();
           // MessageBox.Show("Time: " + stopwatch2.Elapsed.ToString(@"mm\:ss\:fff"));
        }
    }
}