using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GrafoEDA;

namespace GrafoEDA
{


    public partial class Principal : Form
    {
        // Buracos
        private List<Point> buracos;
        // Lamas
        private List<Point> lamas;
        // Player 
        private Point player, saida;
        // Grafo
        private static Grafo grafo;
        private static PictureBox pbMaze;

        // Construtor
        public Principal()
        {

            InitializeComponent();

            // Inicialização 
            buracos = new List<Point>();
            lamas = new List<Point>();
            player = new Point(0, 0);
            saida = new Point(14, 14);
            // 
            // pbMaze
            // 
            pbMaze = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pbMaze)).BeginInit();
            pbMaze.Location = new System.Drawing.Point(3, 2);
            pbMaze.Margin = new System.Windows.Forms.Padding(4);
            pbMaze.Name = "pbMaze";
            pbMaze.Size = new System.Drawing.Size(380, 380);
            pbMaze.TabIndex = 0;
            pbMaze.TabStop = false;
            pbMaze.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMaze_MouseDown);
            this.Controls.Add(pbMaze);
            ((System.ComponentModel.ISupportInitialize)(pbMaze)).EndInit();


            // Desenhando o mapa padrão...
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 100; i++)
            {
                int x = r.Next(15);
                int y = r.Next(15);

                if (x != player.X || y != player.Y)
                    if (x != saida.X || y != saida.Y)
                        if (FindHole(x, y) == -1)
                            if (FindLama(x, y) == -1)
                            {
                                if (i % 2 == 0)
                                    buracos.Add(new Point(x, y));
                                else
                                    lamas.Add(new Point(x, y));
                            }
            }

            // Chamando a função que desenha as cargas
            pbMaze.Image = GUI.DesenhaLabirinto(buracos, lamas, player, saida);

        }

        // Desenhando o mapa
        private void pbMaze_MouseDown(object sender, MouseEventArgs e)
        {
            // Tirando um buraco se existe lá...
            int x = e.X / 25;
            int y = e.Y / 25;
            int indiceBuraco = FindHole(x, y);
            if (indiceBuraco >= 0)
                buracos.RemoveAt(indiceBuraco);
            int indiceLama = FindLama(x, y);
            if (indiceLama >= 0)
                lamas.RemoveAt(indiceLama);
            // player
            if (rbPlayer.Checked)
            {
                player.X = x;
                player.Y = y;
            }
            // saida
            if (rbSaida.Checked)
            {
                saida.X = x;
                saida.Y = y;
            }
            // buraco
            if (rbHole.Checked && indiceBuraco < 0)
            {
                if (x != player.X || y != player.Y)
                    if (x != saida.X || y != saida.Y)
                        buracos.Add(new Point(x, y));
            }
            // buraco
            if (rbLama.Checked && indiceLama < 0)
            {
                if (x != player.X || y != player.Y)
                    if (x != saida.X || y != saida.Y)
                        lamas.Add(new Point(x, y));
            }
            // Redesenhando
            pbMaze.Image = GUI.DesenhaLabirinto(buracos, lamas, player, saida);
        }

        // Criando o grafo
        private void btnGrafo_Click(object sender, EventArgs e)
        {
            grafo = new Grafo();

            // Adicionando nós
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    string name = "N" + i.ToString() + "-" + j.ToString();
                    if (FindLama(i, j) >= 0)
                    {
                        grafo.AdicionarNo(name, "L");
                    }
                    else if (FindHole(i, j) < 0)
                    {
                        grafo.AdicionarNo(name, "");
                    }
                }
            }

            // Adicionando arcos
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    string nameFrom = "N" + i.ToString() + "-" + j.ToString();
                    if (FindHole(i, j) < 0)
                    {
                        // Direções
                        Point[] directions = new Point[4];
                        directions[0] = new Point(i - 1, j);
                        directions[1] = new Point(i + 1, j);
                        directions[2] = new Point(i, j - 1);
                        directions[3] = new Point(i, j + 1);
                        foreach (Point p in directions)
                        {
                            if (p.X >= 0 && p.Y >= 0 && p.X < 15 && p.Y < 15 && FindHole(p.X, p.Y) < 0)
                            {
                                string nameTo = "N" + (p.X.ToString() + "-" + p.Y.ToString());
                                if (FindLama(i, j) >= 0)
                                {
                                    grafo.AdicionarArco(nameFrom, nameTo, 5);
                                }
                                else
                                {
                                    grafo.AdicionarArco(nameFrom, nameTo, 1);
                                }
                            }
                        }
                    }
                }
            }

            // Chamando a função que desenha as cargas
            pbMaze.Image = GUI.DesenhaLabirinto(buracos, lamas, player, saida);

        }

        // Encontra um buraco na lista de buracos
        private int FindHole(int x, int y)
        {
            for (int i = buracos.Count - 1; i >= 0; i--)
            {
                if (buracos[i].X == x && buracos[i].Y == y)
                {
                    return i;
                }
            }
            return -1;
        }

        // Encontra um buraco na lista de buracos
        private int FindLama(int x, int y)
        {
            for (int i = lamas.Count - 1; i >= 0; i--)
            {
                if (lamas[i].X == x && lamas[i].Y == y)
                {
                    return i;
                }
            }
            return -1;
        }

        // Método que desenha um nó no mapa...
        public static void DrawNode(string nodeName, Color color, int delay)
        {
            string[] temp = nodeName.Split('-');
            int x = Convert.ToInt32(temp[0].Replace("N", ""));
            int y = Convert.ToInt32(temp[1]);

            Point p = new Point(x, y);

            Bitmap b = pbMaze.Image as Bitmap;
            Graphics g = Graphics.FromImage(b);

            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, p.X * 25 + 5, p.Y * 25 + 5, 15, 15);

            pbMaze.Refresh();
            Thread.Sleep(delay);
        }

        // Passeio em largura
        private void btnLargura_Click(object sender, EventArgs e)
        {
            string nome = "N" + player.X.ToString() + "-" + player.Y.ToString();
            string sai = "N" + saida.X.ToString() + "-" + saida.Y.ToString();
            if (grafo != null)
                DrawNodes(grafo.PasseioEmLargura(nome));
        }

        // Passeio em profundidade
        private void btnProfundidade_Click(object sender, EventArgs e)
        {
            string nome = "N" + player.X.ToString() + "-" + player.Y.ToString();
            if (grafo != null)
                DrawNodes(grafo.PasseioProfundidade(nome));
        }

        // Caminho mínimo
        private void btnMinimo_Click(object sender, EventArgs e)
        {
            string nome = "N" + player.X.ToString() + "-" + player.Y.ToString();
            string sai = "N" + saida.X.ToString() + "-" + saida.Y.ToString();
            if (grafo != null)
                DrawNodes(grafo.CaminhoMinimo(nome, sai));
        }

        // Desenhando o caminho a partir de uma string separada por ponto e virgula
        private void DrawNodes(string p)
        {
            string[] nos = p.Split(',');
            foreach (string t in nos)
            {
                if (t.Trim() != "") DrawNode(t, Color.Red, 100);
            }
        }
    }
}