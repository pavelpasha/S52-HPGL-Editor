using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S52_HPGL_Editor
{
    public class Transform
    {

        public int rotate_angle = 0;
        public Point rotate_origin = new Point();
        public int scale = 100;
        public List<Point> origin_points = new List<Point>();

    }
}
