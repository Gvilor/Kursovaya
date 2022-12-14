using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using static Kursovaya.Particle;

namespace Kursovaya
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter; // добавили эмиттер
        GravityPoint point1; // добавил поле под первую точку
        GravityPoint point2; // добавил поле под вторую точку
        Teleport input = new Teleport(1);
        Teleport output = new Teleport(2);


        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся

        }


        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); //каждый тик обнорвляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                
                g.Clear(Color.Black); // добавили очистку
                emitter.Render(g); // рендерим систему
            }

            // обновить picDisplay
            picDisplay.Invalidate();

        }


        // добавляем переменные для хранения положения мыши
        private int MousePositionX = 0;
        private int MousePositionY = 0;

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
           
            
                if (e.Button == MouseButtons.Left)
                {
                    emitter.impactPoints.Remove(input);
                    input = new Teleport(1);

                    input.X = e.X;
                    input.Y = e.Y;

                    input.Xt = output.X;
                    input.Yt = output.Y;

                    output.Xt = input.X;
                    output.Yt = input.Y;

                    emitter.impactPoints.Add(input);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    emitter.impactPoints.Remove(output);
                    output = new Teleport(2);

                    output.X = e.X;
                    output.Y = e.Y;

                    output.Xt = input.X;
                    output.Yt = input.Y;

                    input.Xt = output.X;
                    input.Yt = output.Y;

                    emitter.impactPoints.Add(output);
                }
            

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; // направлению эмиттера присваиваем значение ползунка
            lblDirection.Text = $"{tbDirection.Value}°"; // добавил вывод значения
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblDirection_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpreading.Value;
        }
    }
}
