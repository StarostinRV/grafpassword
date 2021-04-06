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
    class Node
    {
        private static int _R;
        private static int _r;
        private Point _location;
        private bool _active;

        public static int R
        {
            get
            {
                return _R;
            }

            set
            {
                _R = value;
            }
        }

        public static int r
        {
            get
            {
                return _r;
            }

            set
            {
                _r = value;
            }
        }

        public bool Аctive
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
            }
        }

        public Point Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

    }
}
