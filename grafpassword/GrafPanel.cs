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
        Timer _timer = new Timer();
        int _dimension_matrix;
        Node[] _nodes;
        bool _finish;
        string _password;
        int _now_index;
        Form1 _form1;
        Point _location;
        Size _size;
        int[,] _grafmodel;

        public Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (!_finish) return;
                _location = value;
            }
        }

        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (!_finish) return;
                _size = value;
                AcceptChanges();

            }
        }

        public int Node_R
        {
            get
            {
                return Node.R;
            }
            set
            {
                if (!_finish) return;
                Node.R = value;
                AcceptChanges();
            }
        }

        public int Node_r
        {
            get
            {
                return Node.r;
            }
            set
            {
                if (!_finish) return;
                Node.r = value;
            }
        }

        public int DimensionMatrix
        {
            get
            {
                return _dimension_matrix;
            }
            set
            {
                if (!_finish) return;
                _dimension_matrix = value;
                AcceptChanges();
            }
        }

        void DrawModel(Graphics g)
        {
            SolidBrush brush_r = new SolidBrush(Color.Black);
            SolidBrush brush_R = new SolidBrush(Color.Green);
            for (int i = 0; i < _dimension_matrix * _dimension_matrix; i++)
            {
                Point loc_r = new Point(_nodes[i].Location.X - Node_r / 2, _nodes[i].Location.Y - Node_r / 2);
                Point loc_R = new Point(_nodes[i].Location.X - Node_R / 2, _nodes[i].Location.Y - Node_R / 2);
                g.FillEllipse(brush_r, loc_r.X, loc_r.Y, Node_r, Node_r);
                if (_nodes[i].Аctive) g.DrawEllipse(new Pen(brush_R), loc_R.X, loc_R.Y, Node_R, Node_R);
            }
        }

        void DrawGraf(Graphics g)
        {
            for (int i = 0; i < _dimension_matrix * _dimension_matrix; i++)
                for (int j = i + 1; j < _dimension_matrix * _dimension_matrix; j++)
                    if (_grafmodel[i, j] == 1) g.DrawLine(new Pen(Color.Black), _nodes[i].Location, _nodes[j].Location);

        }

        void Drawline(Graphics g)
        {
            g.DrawLine(new Pen(Color.Black), _nodes[_now_index].Location, _form1.MouseLocation);
        }

        public void Show(Graphics g)
        {
            DrawModel(g);
            DrawGraf(g);
            if (!_finish) Drawline(g);
        }


        void AcceptChanges()
        {
            SetNodesLocation();
        }

        int MouseOnNode()
        {
            int index = _dimension_matrix*_dimension_matrix;
            for (int i = 0; i < _dimension_matrix * _dimension_matrix; i++)
            {
                int X = _nodes[i].Location.X - _form1.MouseLocation.X;
                int Y = _nodes[i].Location.Y - _form1.MouseLocation.Y;
                if ((X * X + Y * Y) <= (Node.r * Node.r)) index = i;
            }
            return index;
        }

        void SetNodesLocation()
        {
            int n = _dimension_matrix - 1;
            Point d = new Point(_size.Width / n - Node.R, _size.Height / n - Node.R);
            Point start = new Point(_location.X + Node.R, _location.Y + Node.R);
            for (int i = 0; i < _dimension_matrix * _dimension_matrix; i++)
            {
                int X = start.X + d.X * (i % _dimension_matrix);
                int Y = start.Y + d.Y * (i / _dimension_matrix);
                Point p = new Point(X, Y);
                _nodes[i].Location = p;
            }
        }

        void ConectNodes(int index_A, int index_B)
        {
            if ((_grafmodel[index_A, index_B] == 1) || (index_A == index_B)) return;
            string str = _password;
            bool is_min_a = index_A < index_B;
            int n_mid = 0;
            while (index_A != index_B)
            {
                bool flag = true;
                int index_C = -1;
                int i = index_A;
                while (i != index_B)
                {
                    i = is_min_a ? i + 1 : i - 1;
                    int X_up = (_nodes[i].Location.X - _nodes[index_A].Location.X);
                    int X_down = (_nodes[index_B].Location.X - _nodes[index_A].Location.X);
                    int Y_up = (_nodes[i].Location.Y - _nodes[index_A].Location.Y);
                    int Y_down = (_nodes[index_B].Location.Y - _nodes[index_A].Location.Y);
                    if ((X_up * Y_down) == (Y_up * X_down))
                    {
                        n_mid++;
                        if (flag) index_C = i;
                        flag = false;
                        _grafmodel[index_A, i] = _grafmodel[i, index_A] = 1;
                    }
                }
                _nodes[index_C].Аctive = true;
                _password = _password + "/" + index_C.ToString();
                index_A = index_C;
            }
            _now_index = index_B;
            int last = int.Parse(str.Substring(1 + str.LastIndexOf("/")));
            List<int> _last = new List<int>();
            List<int> _now = new List<int>();
            for (int j = 0; j<_dimension_matrix*_dimension_matrix;j++)
            {
                int X_up = (_nodes[j].Location.X - _nodes[_now_index].Location.X);
                int X_down = (_nodes[last].Location.X - _nodes[_now_index].Location.X);
                int Y_up = (_nodes[j].Location.Y - _nodes[_now_index].Location.Y);
                int Y_down = (_nodes[last].Location.Y - _nodes[_now_index].Location.Y);
                if (((X_up * Y_down) == (Y_up * X_down)) && (_grafmodel[last, j] == 1))
                {
                    _last.Add(j);
                }
                if (((X_up * Y_down) == (Y_up * X_down)) && (_grafmodel[_now_index, j] == 1))
                {
                    _now.Add(j);
                }
            }
            _last.Add(last);
            _now.Add(_now_index);
            for (int i = 0; i < _now.Count; i++)
            {
                for (int j = 0; j < _last.Count; j++)
                {
                    if (_last[j] != _now[i]) _grafmodel[_last[j], _now[i]] = _grafmodel[_now[i], _last[j]] = 1;
                }
            }           
        }

        void Tick(object sender, EventArgs e)
        {
            if (_finish && (MouseOnNode() != _dimension_matrix*_dimension_matrix) && _form1.IsMouseDown) Start();
            if ((MouseOnNode() != _dimension_matrix * _dimension_matrix) && (!_finish || _finish && _form1.IsMouseDown)) ConectNodes(_now_index, MouseOnNode());
            if (!_form1.IsMouseDown && !_finish)
            {
                _finish = true;
                MessageBox.Show(_password);
            }
        }

        void Start()
        {
            _finish = false;
            for (var i = 0; i < _dimension_matrix*_dimension_matrix; i++)
                _nodes[i].Аctive = false;
            for (var i = 0; i < _dimension_matrix * _dimension_matrix; i++)
                for (var j = 0; j < _dimension_matrix * _dimension_matrix; j++)
                    _grafmodel[i, j] = 0;
            _now_index = MouseOnNode();
            _nodes[_now_index].Аctive = true;
            _password = _now_index.ToString();
        }

        public GrafPanel(Form1 term)
        {
            _timer.Tick += new EventHandler(Tick);
            _timer.Enabled = true;
            _timer.Interval = 100;
            _form1 = term;
            _dimension_matrix = 8;
            _grafmodel = new int[_dimension_matrix* _dimension_matrix, _dimension_matrix* _dimension_matrix];
            _nodes = new Node[_dimension_matrix * _dimension_matrix];
            for (var i = 0; i < _dimension_matrix * _dimension_matrix; i++)
            {
                _nodes[i] = new Node();
                _nodes[i].Аctive = false;
                for (var j = 0; j < _dimension_matrix * _dimension_matrix; j++)
                    _grafmodel[i, j] = 0;
            }
                


            _finish = true;
            _password = "";
            _location = new Point(20, 50);
            Node_R = 40;
            Node_r = 10;
            
            Size = new Size(720, 720);
            

            
        }


    }
}
