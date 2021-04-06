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
    class GrafPanel
    {
        int dimension_matrix;
        Node[,] nodes;
        bool finish;
        string password;
        int now_index;
        Form1 form1;
        Point location;
        Size size;
        int[,] grafmodel;
    }
}
