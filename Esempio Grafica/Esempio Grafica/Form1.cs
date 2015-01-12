using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esempio_Grafica
{
    public partial class Form1 : Form
    {
        bool curveVisible = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curveVisible = !curveVisible;
            Invalidate(); //invalida la form che l'utente sta vedendo
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            if (curveVisible)
                g.DrawBezier(Pens.Red, new Point(10, 10), new Point(30, 5), new Point(50, 30), new Point(100, 100));
        }
    }
}