using Kursovaya;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Teleport : IImpactPoint
    {
        public static Random rnd = new Random();
        public int Radius; // радиус частицы
        public int X = 50; // X координата положения частицы в пространстве
        public int Y = 50; // Y координата положения частицы в пространстве
        public int Xt = 0;
        public int Yt = 0;
        public bool In;
        public int DerectionXY; // Угол вылета

        public Teleport(int k)
        {
            Radius = 80;
            if (k == 1)
            {
                In = true;
            }
            else
            {
                In = false;

            }
            DerectionXY = 90;
        }
        public override void ImpactParticle(Particle particle)
        {

            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);
            double r = Math.Sqrt(gX * gX + gY * gY);
            if (In)
            {
                if (r + particle.Radius < 50)
                {
                    particle.X = Xt + rnd.Next(50);
                    particle.Y = Yt - 30 + rnd.Next(60);
                }
            }

        }
        public override void Render(Graphics g)
        {
            if (In)
            {
                g.DrawEllipse(
                        new Pen(Color.Blue),
                        X - Radius / 2,
                        Y - Radius / 2,
                        Radius,
                        Radius
                    );
            }
            else
            {
                g.DrawEllipse(
                        new Pen(Color.Orange),
                        X - Radius / 2,
                        Y - Radius / 2,
                        Radius,
                        Radius
                    );
            }
        }
    }
}
