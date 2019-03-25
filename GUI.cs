using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GrafoEDA
{
    class GUI
    {
        public static Bitmap DesenhaLabirinto(List<Point> buracos, List<Point> lamas, Point player, Point saida)
        {
            Bitmap b = new Bitmap(502, 502);
            Graphics g = Graphics.FromImage(b);
            // Desenhando espaços do labirinto
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.Gray), i * 25, j * 25, 25, 25);
                }
            }

            // Agora os buracos
            foreach (Point p in buracos)
            {
                g.FillRectangle(Brushes.DarkGray, p.X * 25 + 1, p.Y * 25 + 1, 24, 24);
            }

            // Agora os buracos
            foreach (Point p in lamas)
            {
                g.FillRectangle(Brushes.Chocolate, p.X * 25 + 1, p.Y * 25 + 1, 24, 24);
            }

            // Player e saída
            g.DrawString("P", new Font("Arial", 20), Brushes.Black, player.X * 25 - 1, player.Y * 25 - 1);
            g.DrawString("S", new Font("Arial", 20), Brushes.Black, saida.X * 25 - 1, saida.Y * 25 - 1);

            // Retorno
            return b;

        }
    }
}
