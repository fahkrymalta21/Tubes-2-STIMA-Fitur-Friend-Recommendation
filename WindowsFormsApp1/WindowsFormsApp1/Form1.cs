using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string namafile;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            openFileDialog1.Filter = "*.txt|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Title = "Masukkan file eksternal";

            // Show file dialog
            DialogResult result = openFileDialog1.ShowDialog();
            button1.Text = openFileDialog1.SafeFileName;
            namafile = openFileDialog1.FileName;
            StrategiAlgoritma S = new StrategiAlgoritma(openFileDialog1.FileName);

            List<string> semua = new List<string>();
            using (StreamReader sr = new StreamReader(openFileDialog1.OpenFile()))
            {
                string line = sr.ReadLine();
                if (line == null || line == "0")
                {
                    MessageBox.Show("File is empty", "Warning!!!");
                }
                else
                {
                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();
                    while (sr.Peek() >= 0)
                    {
                        line = sr.ReadLine(); // Read file line by line
                        string[] cur_line = line.Split(' ');
                        graph.AddEdge(cur_line[0], cur_line[1]).Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        int i = 0;
                        bool sama = false;
                        while(i < semua.Count && sama == false)
                        {
                            if (semua.Contains(cur_line[0]))
                            {
                                sama = true;
                            } i++;
                        }
                        if (sama == false)
                        {
                            semua.Add(cur_line[0]);
                            
                        }
                    }
                    List<string> ListCombo = S.GetGraf();
                    foreach(string text in ListCombo)
                    {
                        comboBox1.Items.Add(text);
                        comboBox2.Items.Add(text);
                    }
                    

                }
            }
            viewer.Graph = graph;
            panel1.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);
            panel1.ResumeLayout();

            // Masukkan Daftar Akun A
            this.Controls.Add(comboBox1);

            // Masukkan Daftar Akun B
            this.Controls.Add(comboBox2);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.ForeColor = System.Drawing.Color.Blue;
        }
            

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Text = "Close form";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                listView1.Items.Clear();
                button2.ForeColor = System.Drawing.Color.Green;
                StrategiAlgoritma S = new StrategiAlgoritma(namafile);
                label5.Text = "Explore Friend " + comboBox1.SelectedItem.ToString() + " with " + comboBox2.SelectedItem.ToString();
                label6.Text = "Friend Recommendation " + comboBox1.SelectedItem.ToString();
                textBox3.Text = S.ExploreFriendsBFS(comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
                List<string> listteman = S.FriendRecomBFS(comboBox1.SelectedItem.ToString());
                foreach (string text in listteman)
                {
                    listView1.Items.Add(text);

                }
                textBox3.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                listView1.Visible = true;
                
                
                

            }
            
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
