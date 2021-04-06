using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafpassword
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Point mouse_location;
        public bool is_mouse_down;

        public Point MouseLocation
        {
            get
            {
                return mouse_location;
            }
        }

        public bool IsMouseDown
        {
            get
            {
                return is_mouse_down;
            }
        }

        GrafPanel GP1;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            GP1 = new GrafPanel(this);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
