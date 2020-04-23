using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S52_HPGL_Editor
{

    enum GeometryType
    {
        LINE,
        CIRCLE,
        POLYGON,
        POINT,
        NONE
    }
    class HGeometry
    {
        public GeometryType type;
        public string color = "CHBLK"; // default color
        public int penWidth = 1;
        public int transparency = 0;
        public bool selected = false;
        public List<Point> points = new List<Point>();
        public Transform transform = null;
       
        

        public HGeometry Copy()
        {


            if (type != GeometryType.CIRCLE)
            {

                var new_geom = new HGeometry();
                new_geom.type = this.type;
                new_geom.penWidth = this.penWidth;
                new_geom.transparency = this.transparency;
                new_geom.color = this.color;
                new_geom.points = new List<Point>(this.points);
                return new_geom;
            }
            else
            {
                var new_geom = new HCircle();
                new_geom.type = this.type;
                new_geom.penWidth = this.penWidth;
                new_geom.transparency = this.transparency;
                new_geom.color = this.color;
                new_geom.points = new List<Point>(this.points);
                new_geom.radius = (this as HCircle).radius;
                new_geom.filled = (this as HCircle).filled;
                return new_geom;
                

            }


        }

        public Point getCenterPoint()
        {


            if (points.Count() == 0)
                return points[0];
           
            var rect = calculateBoundingBox();

            return new Point(rect.Left + rect.Width / 2,
                     rect.Top + rect.Height / 2); ;

        }

        

        public void setRotationTransform(int rotate, Point origin = new Point())
        {
          
            if (transform == null)
            {
                transform = new Transform();
                transform.rotate_angle = rotate;
                transform.rotate_origin = origin;
                transform.origin_points = new List<Point>(points);
            }
            else
            {

                transform.rotate_angle = rotate;
                transform.rotate_origin = origin;

            }

            applyTransform();
        }


        public void setScaleTransform(int scale)
        {

            if (transform == null)
            {
                transform = new Transform();
                transform.scale = scale;
                transform.origin_points = new List<Point>(points);
            }
            else
            {

                transform.scale = scale;

            }

            applyTransform();
        }



        public void movePoint(int idx, int dx, int dy)
        {
            var p = points[idx];
            p.X += dx;
            p.Y += dy;

            points[idx] = p;

            if (transform != null)
            {

                p = transform.origin_points[idx];
                p.X += dx;
                p.Y += dy;

                transform.origin_points[idx] = p;
            }

        }

        public void removePoint(int idx)
        {
            points.RemoveAt(idx);
            if (transform != null)
            {

                transform.origin_points.RemoveAt(idx);
            }

        }

        public void addPoint(Point p)
        {
            points.Add(p);
            if (transform != null)
            {

                transform.origin_points.Add(p);
            }

        }

        public void move(int dx, int dy)
        {
            for (int i = 0; i < points.Count(); i++)
            {

                var point = points[i];

                point.X += dx;
                point.Y += dy;


                points[i] = point;


                if (transform != null)
                {
                    point = transform.origin_points[i];

                    point.X += dx;
                    point.Y += dy;


                    transform.origin_points[i] = point;

                    transform.rotate_origin.X += dx;
                    transform.rotate_origin.Y += dy;

                }
            }

        }

        private void applyTransform()
        {

            double sin_rot = Math.Sin(transform.rotate_angle * Math.PI / 180);
            double cos_rot = Math.Cos(transform.rotate_angle * Math.PI / 180);

            var center = getCenterPoint();

            for (int i = 0; i < points.Count(); i++)
            {

                // if (angle == 0) return;

                var point = transform.origin_points[i];


                double xp = ((point.X - transform.rotate_origin.X) * cos_rot) - ((point.Y - transform.rotate_origin.Y) * sin_rot);
                double yp = ((point.X - transform.rotate_origin.X) * sin_rot) + ((point.Y - transform.rotate_origin.Y) * cos_rot);


                point.X = (int)((xp + transform.rotate_origin.X));
                point.Y = (int)((yp + transform.rotate_origin.Y));



                if (transform.scale != 100)
                {


                    // Scale around center point
                    point.X -= center.X;
                    point.Y -= center.Y;

                    point.X = (int)(point.X * (transform.scale / 100.0));
                    point.Y = (int)(point.Y * (transform.scale / 100.0));

                    point.X += center.X;
                    point.Y += center.Y;


                }

                points[i] = point;
            }
        }

        private Rectangle calculateBoundingBox()
        {

            var bounding_box = new Rectangle();

            if (points.Count() < 2)
            {
                bounding_box.X = points[0].X;
                bounding_box.Y = points[0].Y;
                return bounding_box;
            }

            int maxX = -1;
            int maxY = -1;
            int minX = 32767;
            int minY = 32767;

            foreach (var p in points) {

                if (p.X > maxX)
                    maxX = p.X;
                if (p.Y > maxY)
                    maxY = p.Y;

                if (p.X < minX)
                    minX = p.X;
                if (p.Y < minY)
                    minY = p.Y;

            }
            bounding_box.X = minX;
            bounding_box.Y = minY;
            bounding_box.Width = maxX - minX;
            bounding_box.Height = maxY - minY;
            return bounding_box;

        }


    }



    class HLineString : HGeometry
    {
        public HLineString() { type = GeometryType.LINE; }
        public HLineString(HLineString other)
        {
            type = GeometryType.LINE;

            this.color = other.color;
            this.penWidth = other.penWidth;
            this.transparency = other.transparency;
            this.points = new List<Point>(other.points);


        }



    }
    class HCircle : HGeometry
    {
        public HCircle() { type = GeometryType.CIRCLE; }
        public int radius = 1;
        public bool filled = false;

    }

    class HPoint : HGeometry
    {
        public HPoint() { type = GeometryType.POINT; }


    }
    class HPolygon : HGeometry
    {
        public HPolygon() { type = GeometryType.POLYGON; }

    }
}
