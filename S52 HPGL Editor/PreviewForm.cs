using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S52_HPGL_Editor
{
    public partial class PreviewForm : Form
    {

        private Symbol symbol = null;
        private float ppm;   // Pixels per mm
        private int scale = 1;
        private string bg_color_name = "DEVPS";
        public PreviewForm(ref Symbol symbol)
        {
            this.symbol = symbol;
            InitializeComponent();
            ppm = this.CreateGraphics().DpiX/25.4f;

            bg_color.DataSource = new BindingSource(Style.S52colors, null);
            bg_color.DisplayMember = "Key";
            bg_color.ValueMember = "Value";
            bg_color.SelectedIndex = 30;
        }
    

        private void scale_numeric_ValueChanged(object sender, EventArgs e)
        {
            scale = scale = (int)scale_numeric.Value;
            canvas.Refresh();
        }

        private void antialiasing_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }


        private Point projectPoint(Point p) {

            int width = canvas.Width;
            int height = canvas.Height;
            int c_x = width / 2;
            int c_y = height / 2;
            int k = (int)(100 / ppm) / scale;
   
            p.X = (int)(c_x + (p.X - c_x) / k) - symbol.offset_x/k;
            p.Y = (int)(c_y - (c_y - p.Y) / k) - symbol.offset_y/k;

            return p;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            bool antialising = antialiasing_checkbox.Checked;
          
            var g = e.Graphics;


            g.FillRectangle(new SolidBrush(Style.S52colors[bg_color_name]),0,0,canvas.Width, canvas.Height);


            if(antialising)
                g.SmoothingMode = SmoothingMode.AntiAlias;

            Point p = new Point();
            int n_points = 0;

            float k = (100 / ppm) / scale; // Line width coefficient. (S52 docs says :"The coordinates are within the range of 0 to 32767 units. Each unit represents 0.01 mm")

            foreach (var geom in symbol.geometry)
            {

                int transparency = (4 - geom.transparency) * 64;
                transparency = Math.Min(transparency, 255);
                transparency = Math.Max(0, transparency);

                var color = Color.FromArgb(transparency, Style.S52colors[geom.color]);
                Pen pen = new Pen(color, geom.penWidth *scale);



                switch (geom.type)
                {
                    case GeometryType.LINE:
                        if (geom.points.Count() < 2)
                            continue;

                        n_points = geom.points.Count();

                        var projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = projectPoint(geom.points[i]);

                        }


                        g.DrawLines(pen, projPnts);


                        break;
                    case GeometryType.CIRCLE:
                        var circle = geom as HCircle;
                        p = projectPoint(circle.points[0]);
                        var radius = circle.radius / k;
                        if (circle.filled)
                            g.FillEllipse(new SolidBrush(color), p.X - radius, p.Y - radius, radius * 2, radius * 2);
                        else
                            g.DrawEllipse(pen, p.X - radius, p.Y - radius, radius * 2, radius * 2);

                        break;

                    case GeometryType.POINT:

                        p = projectPoint(geom.points[0]);
                        int size = (int)(geom.penWidth * k);
                        g.FillRectangle(new SolidBrush(color), p.X - size / 2, p.Y - size / 2, size, size);

                        break;

                    case GeometryType.POLYGON:

                        if (geom.points.Count() < 2)
                            continue;

                        n_points = geom.points.Count();
                        projPnts = new Point[n_points];

                        for (int i = 0; i < n_points; i++)
                        {

                            projPnts[i] = projectPoint(geom.points[i]);

                        }

                        g.FillPolygon(new SolidBrush(color), projPnts);

                        break;
                }

            }
        }

        private void bg_color_SelectedIndexChanged(object sender, EventArgs e)
        {

            var s = bg_color.Items[bg_color.SelectedIndex];
            IList<PropertyInfo> props = new List<PropertyInfo>(s.GetType().GetProperties());

            bg_color_name = props[0].GetValue(s) as string;
            
            canvas.Refresh();
        }

        private void bg_color_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var s = bg_color.Items[e.Index];
            IList<PropertyInfo> props = new List<PropertyInfo>(s.GetType().GetProperties());

            string col_name = props[0].GetValue(s) as string;
            Color col = (Color)props[1].GetValue(s);
            var tex_col = createContrastColor(col);
            e.Graphics.FillRectangle(new SolidBrush(col), e.Bounds);
            e.Graphics.DrawString(col_name, e.Font, new SolidBrush(tex_col), e.Bounds);
            e.DrawFocusRectangle();
        }

        public Color createContrastColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        private void canvas_SizeChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }
    }
}
