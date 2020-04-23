using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace S52_HPGL_Editor
{


    class ViewPort
    {
        public ViewPort() {

            zoom = 0.5f;
            center = new Point(max_x / 2, max_y / 2);

        }


        public void pan(int dx, int dy)
        {
            center.X += (int) (dx/zoom );
            center.Y += (int)(dy/zoom );
        }


        public Point unproject(Point p)
        {
            int x = p.X;
            int y = p.Y;
            x = x - width/2;
            y = y - height/2;
            x = (int) (x/zoom);
            y = (int)(y / zoom);
            x += center.X;
            y += center.Y;
            return new Point(x, y);
        }

        public Point project(Point p) {


            Point ret = new Point();
            ret.X = (int)(width/2 + (p.X - center.X) * zoom);
            ret.Y = (int)(height/2 - (center.Y - p.Y) * zoom);

            return ret;

        }

        public void resize(int width, int height)
        {

            this.width = width;
            this.height = height;
            
        }

        public Point center;
        public float zoom;
        const int max_x = 32767, max_y = 32767;
        int width, height;
        

    }
}
