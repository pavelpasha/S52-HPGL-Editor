using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S52_HPGL_Editor
{
    public class Symbol
    {

        public string instruction = "";
        public string colref;
        public int offset_x, offset_y, pivot_x, pivot_y, height, width;
        public string expo, symb, name;
        public char type;
        public List<HGeometry> geometry = null;
        public string filename = null;
        public Symbol()
        {

            geometry = new List<HGeometry>();
        }



        public void paint(ref System.Drawing.Graphics canvas, ref ViewPort vp, bool polygonOutline = false)
        {

            int point_r = 5; // Point radius
            Point p = new Point();
            int n_points = 0;

            float line_width_k = (100 / (canvas.DpiX / 25.4f))*vp.projection_scale; // Line width coefficient. (S52 docs says :"The coordinates are within the range of 0 to 32767 units. Each unit represents 0.01 mm")

            foreach (var geom in geometry)
            {

                int transparency = (4 - geom.transparency) * 64;
                transparency = Math.Min(transparency, 255);
                transparency = Math.Max(0, transparency);

                var color = Color.FromArgb(transparency, Style.S52colors[geom.color]);
                Pen pen = new Pen(color, geom.penWidth * line_width_k);



                switch (geom.type)
                {
                    case GeometryType.LINE:
                        if (geom.points.Count() < 2)
                            continue;

                        n_points = geom.points.Count();

                        var projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = vp.project(geom.points[i]);

                        }


                        canvas.DrawLines(pen, projPnts);


                        break;
                    case GeometryType.CIRCLE:
                        var circle = geom as HCircle;
                        p = vp.project(circle.points[0]);
                        var radius = circle.radius * vp.projection_scale;
                        if (circle.filled)
                            canvas.FillEllipse(new SolidBrush(color), p.X - radius, p.Y - radius, radius * 2, radius * 2);
                        else
                            canvas.DrawEllipse(pen, p.X - radius, p.Y - radius, radius * 2, radius * 2);

                        break;

                    case GeometryType.POINT:

                        p = vp.project(geom.points[0]);
                        int size = (int)(geom.penWidth * line_width_k);
                        canvas.FillRectangle(new SolidBrush(color), p.X - size / 2, p.Y - size / 2, size, size);

                        break;

                    case GeometryType.POLYGON:

                        if (geom.points.Count() < 2)
                            continue;

                        n_points = geom.points.Count();
                        projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = vp.project(geom.points[i]);

                        }

                        canvas.FillPolygon(new SolidBrush(color), projPnts);
                        if (polygonOutline)
                            canvas.DrawPolygon(pen, projPnts);

                        break;
                }

            }

            foreach (var geom in geometry)
            {
                if (!geom.selected)
                    continue;

                switch (geom.type)
                {
                    case GeometryType.LINE:

                        n_points = geom.points.Count();

                        var projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = vp.project(geom.points[i]);

                        }

                        if (n_points > 1)
                            canvas.DrawLines(new Pen(Color.Red), projPnts);


                        for (int i = 0; i < n_points; i++)
                        {

                            canvas.DrawEllipse(new Pen(Color.Red), projPnts[i].X - point_r, projPnts[i].Y - point_r, point_r * 2, point_r * 2);


                        }

                        break;

                    case GeometryType.CIRCLE:
                        var circle = geom as HCircle;
                        p = vp.project(circle.points[0]);
                        var radius = circle.radius * vp.projection_scale;

                        canvas.DrawEllipse(new Pen(Color.Red), p.X - radius, p.Y - radius, radius * 2, radius * 2);
                        canvas.DrawEllipse(new Pen(Color.Red), p.X - point_r, p.Y - point_r, point_r * 2, point_r * 2);

                        break;

                    case GeometryType.POINT:

                        p = vp.project(geom.points[0]);
                        canvas.DrawEllipse(new Pen(Color.Red), p.X - point_r, p.Y - point_r, point_r * 2, point_r * 2);

                        break;

                    case GeometryType.POLYGON:
                        n_points = geom.points.Count();

                        projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = vp.project(geom.points[i]);

                        }

                        if (n_points > 1)
                            canvas.DrawLines(new Pen(Color.Red), projPnts);


                        for (int i = 0; i < n_points; i++)
                        {

                            canvas.DrawEllipse(new Pen(Color.Red), projPnts[i].X - point_r, projPnts[i].Y - point_r, point_r * 2, point_r * 2);


                        }

                        break;
                }
            }
        }

        public void move(int dx, int dy) {

            foreach (var g in geometry) {

                g.move(dx,dy);

            }

        }

        public void unselectAll()
        {

            foreach (var g in geometry)
            {

                g.selected = false;

            }

        }

        // Update symbol boundingBox (offset_x,y; width, height)
        private void updateSizes()
        {

            int maxX = -1;
            int maxY = -1;
            int minX = 32767;
            int minY = 32767;

            foreach (var g in geometry) {

                foreach (var p in g.points)
                {

                    if (p.X > maxX)
                        maxX = p.X;
                    if (p.Y > maxY)
                        maxY = p.Y;

                    if (p.X < minX)
                        minX = p.X;
                    if (p.Y < minY)
                        minY = p.Y;

                }

            }


            offset_x = minX;
            offset_y = minY;
            width = maxX - minX;
            height = maxY - minY;

        }


        private static char getRandomChar()
        {

            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            Random r = new Random();
            int i = r.Next(chars.Length);
            return chars[i];
        }


        private  Dictionary<string, char> createColRef()
        {

            var colref = new Dictionary<string, char>();

            foreach (var g in geometry)
            {
                if (!colref.ContainsKey(g.color))
                {

                    char COL = getRandomChar();
                    while (colref.ContainsValue(COL))
                    {
                        COL = getRandomChar();
                        Console.WriteLine(COL);
                    }

                    colref[g.color] = COL;

                }

            }

            return colref;

        }

        public  string serialize()
        {

            updateSizes();

            string colref_str = "";
            string definition = "SYMD";
            
            if (type == 'L')          
                definition = "LIND";
                      
            else if (type == 'P')      
                definition = "PATD";

            definition += String.Format("   39{0}V{1,5:00000}{2,5:00000}{3,5:00000}{4,5:00000}{5,5:00000}{6,5:00000}\n", name,pivot_x,pivot_y,width,height,offset_x, offset_y);

            string expo_string = String.Format(type + "XPO{0,5:#####}{1}\n", expo.Length, expo);

            var colref = createColRef();

            foreach (KeyValuePair<string, char> color in colref)
            {
                colref_str += String.Format("{0}{1}", color.Value, color.Key);
            }

            colref_str = String.Format(type+"CRF{0,5:#####}{1}\n", colref_str.Length, colref_str);

            string cur_pen_color = "";
            int cur_pen_width = -1;
            int cur_transp = -1;

            string instr_str = "";

            foreach (var g in geometry)
            {

                if (cur_pen_color != g.color)
                {
                    instr_str += String.Format("SP{0};", colref[g.color]);
                    cur_pen_color = g.color;

                }
                if (cur_pen_width != g.penWidth)
                {
                    instr_str += String.Format("SW{0};", g.penWidth);
                    cur_pen_width = g.penWidth;

                }
                if (cur_transp != g.transparency)
                {
                    instr_str += String.Format("ST{0};", g.transparency);
                    cur_transp = g.transparency;

                }


                switch (g.type)
                {
                    case GeometryType.LINE:
                        instr_str += String.Format("PU{0},{1};", g.points[0].X, g.points[0].Y);

                        if (g.points.Count() < 2) continue;

                        for (int i = 1; i < g.points.Count(); i++)
                        {

                            instr_str += String.Format("PD{0},{1};", g.points[i].X, g.points[i].Y);
                        }

                        break;
                    case GeometryType.CIRCLE:
                        var ci = g as HCircle;

                        instr_str += String.Format("PU{0},{1};", ci.points[0].X, ci.points[0].Y);

                        if (ci.filled)
                        {

                            instr_str += String.Format("PM0;CI{0};PM2;FP;", ci.radius);

                        }
                        else
                        {

                            instr_str += String.Format("CI{0};", ci.radius);
                        }

                        break;

                    case GeometryType.POLYGON:

                        instr_str += String.Format("PU{0},{1};PM0;", g.points[0].X, g.points[0].Y);

                        if (g.points.Count() < 2) continue;
                        instr_str += "PD";
                        string delim = ",";
                        for (int i = 1; i < g.points.Count(); i++)
                        {
                            if (i == g.points.Count() - 1) delim = ";";
                            instr_str += String.Format("{0},{1}{2}", g.points[i].X, g.points[i].Y, delim);
                        }
                        instr_str += "PM2;FP;";
                        break;

                    case GeometryType.POINT:

                        instr_str += String.Format("PU{0},{1};PD;", g.points[0].X, g.points[0].Y);
                        break;
                }

            }


            return String.Format("0001\n{0}\n{1}{2}{3}{4}VCT{5,5:#####}{6}\n****", symb, definition, expo_string, colref_str, type, instr_str.Length,instr_str);

        }

    }





}
