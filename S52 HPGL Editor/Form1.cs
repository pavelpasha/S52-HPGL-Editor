using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S52_HPGL_Editor
{



   


    public partial class Form1 : Form
    {

        ViewPort vp;
        Symbol current_sym;
        private readonly ContextMenuStrip collectionRoundMenuStrip;

        int _mouse_startX, _mouse_startY;
        bool _fired_by_user = false; // If value changed by user

        GeometryType _addMode = GeometryType.NONE;
        Edit_mode _edit_mode = Edit_mode.EDIT_GEOMETRY;

        int selected_geometry_idx = -1; // index of geometry wich is under editing
        int selected_point_idx = -1;

        enum Edit_mode {
            ADD_GEOM,
            ADD_POINT,
            EDIT_SYMBOL,
            EDIT_GEOMETRY      
        }

        
        private void setEditiMode(Edit_mode mode) {

            // Set things to default
            ed_sym_mode.Checked = false;
            current_point_label.Text = "Selected point:";
            //

            _edit_mode = mode;
            if (mode == Edit_mode.ADD_GEOM || mode == Edit_mode.ADD_POINT)
                Cursor = Cursors.Cross;
            else
                Cursor = Cursors.Default;

            if (mode == Edit_mode.ADD_POINT)
                addPoints_menu_btn.Checked = true;              
            else
                addPoints_menu_btn.Checked = false;

            if (mode == Edit_mode.EDIT_SYMBOL)
            {
                ed_sym_mode.Checked = true;
                current_sym.unselectAll();
                selected_geometry_idx = -1;
                selected_point_idx = -1;

                point_x_val.Enabled = true;
                point_y_val.Enabled = true;

                point_x_val.Value = current_sym.pivot_x;
                point_y_val.Value = current_sym.pivot_y;

                current_point_label.Text = "Pivot point:";
            }


        }

        private void moveSelectedPoint(int dx, int dy)
        {

            current_sym.geometry[selected_geometry_idx].movePoint(selected_point_idx, dx, dy);
           

        }

        public Form1()
        {
            InitializeComponent();
            vp = new ViewPort();
            current_sym = new Symbol();
            vp.resize(canvas.Width, canvas.Height);
      
            canvas.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);
            Style.init();
            status_string.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            geom_explorer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            color_prop_combo.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            width_prop.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            transp_prop.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            radius_prop.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label3.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label4.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            delete_itm_btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            copy_itm_btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            transform_group.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            color_prop_combo.DataSource = new BindingSource(Style.S52colors, null);
            color_prop_combo.DisplayMember = "Key";
            color_prop_combo.ValueMember = "Value";

            
            collectionRoundMenuStrip = new ContextMenuStrip();
                   

        }

       
        private void canvas_SizeChanged(object sender, EventArgs e)
        {
            vp.resize(canvas.Width, canvas.Height);
            canvas.Refresh();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {

            var g = e.Graphics;

            // Draw working area bounding box
            var leftTop = vp.project(new Point(0, 0));
            var rect_size = 32767 * vp.projection_scale;
            g.DrawRectangle(new Pen(Color.Black), leftTop.X, leftTop.Y, rect_size, rect_size);

            // Draw symbol itself
            
            current_sym.paint(ref g, ref vp, polygonOutlineToolStripMenuItem.Checked);


            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                // Draw symbol pivot point

                var origin = vp.project(new Point(current_sym.pivot_x, current_sym.pivot_y));

                g.DrawLine(new Pen(Color.Black, 3), origin.X, origin.Y - 5, origin.X, origin.Y + 5);
                g.DrawLine(new Pen(Color.Black, 3), origin.X - 5, origin.Y, origin.X + 5, origin.Y);

                g.DrawLine(new Pen(Color.Yellow), origin.X, origin.Y - 5, origin.X, origin.Y + 5);
                g.DrawLine(new Pen(Color.Yellow), origin.X - 5, origin.Y, origin.X + 5, origin.Y);

            }
            // If geometry selected -  its points and rotate origin
            else
            {
                if (selected_geometry_idx != -1)
                {
                    if (selected_point_idx != -1)
                    {
                        var p = current_sym.geometry[selected_geometry_idx].points[selected_point_idx];

                        p = vp.project(p);

                        g.DrawRectangle(new Pen(Color.Yellow), p.X - 5, p.Y - 5, 10, 10);
                    }
                    // Draw rotate origin
                    var origin = new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value);
                    origin = vp.project(origin);

                    g.DrawLine(new Pen(Color.Black, 3), origin.X, origin.Y - 5, origin.X, origin.Y + 5);
                    g.DrawLine(new Pen(Color.Black, 3), origin.X - 5, origin.Y, origin.X + 5, origin.Y);

                    g.DrawLine(new Pen(Color.Yellow), origin.X, origin.Y - 5, origin.X, origin.Y + 5);
                    g.DrawLine(new Pen(Color.Yellow), origin.X - 5, origin.Y, origin.X + 5, origin.Y);

                }
            }


        }


        // Mouse events

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

            
            _mouse_startX = e.X;
            _mouse_startY = e.Y;


            if (e.Button == MouseButtons.Left)
            {


                if (_edit_mode == Edit_mode.ADD_GEOM) //Add new geometry of chosen type in selected point
                {

                    HGeometry new_geometry = null;

                    if (_addMode == GeometryType.CIRCLE)
                    {                   
                        new_geometry = new HCircle();
                        setEditiMode(Edit_mode.EDIT_GEOMETRY);
                    }
                    else if (_addMode == GeometryType.POINT)
                    {
                        new_geometry = new HPoint();
                        setEditiMode(Edit_mode.EDIT_GEOMETRY);
                        
                    }
                    else if (_addMode == GeometryType.LINE)
                    {
                        new_geometry = new HLineString();
                        setEditiMode(Edit_mode.ADD_POINT);
                    }
                    else if (_addMode == GeometryType.POLYGON)
                    {
                        new_geometry = new HPolygon();
                        setEditiMode(Edit_mode.ADD_POINT);
                    }
                    var p = vp.unproject(new Point(e.X, e.Y));
                    new_geometry.addPoint(p);
                    current_sym.geometry.Add(new_geometry);
                    updateExplorerList();
                    geom_explorer.SelectedIndex = current_sym.geometry.Count() - 1;
                    selectGeometry(current_sym.geometry.Count() - 1);
                    canvas.Refresh();
                }
                else if (_edit_mode == Edit_mode.ADD_POINT) // Append point to selected geometry
                {

                    current_sym.geometry[selected_geometry_idx].points.Add(vp.unproject(new Point(e.X, e.Y)));

                    canvas.Refresh();
                }
                else
                {

                    if (selected_geometry_idx != -1)
                    {

                        var s_geom = current_sym.geometry[selected_geometry_idx];

                        for (int i = 0; i < s_geom.points.Count(); i++)
                        {

                            var p = vp.project(s_geom.points[i]);

                            if (Math.Abs(p.X - e.X) < 6 && Math.Abs(p.Y - e.Y) < 6)
                            {


                                if (e.Button == MouseButtons.Left)
                                {
                                    selected_point_idx = i;

                                    p = current_sym.geometry[selected_geometry_idx].points[selected_point_idx];

                                    point_x_val.Enabled = true;
                                    point_y_val.Enabled = true;

                                    point_x_val.Value = p.X;
                                    point_y_val.Value = p.Y;

                                    canvas.Refresh();

                                }

                            }


                        }

                    }

                }

            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = vp.unproject(new Point(e.X, e.Y));
            status_string.Text = string.Format("X: {0}, Y: {1}", pos.X, pos.Y);

            this.Cursor = Cursors.Default;

            if (selected_geometry_idx != -1 && selected_point_idx != -1)
            {
                
                var s_geom = current_sym.geometry[selected_geometry_idx];

                for (int i = 0; i < s_geom.points.Count(); i++)
                {

                    var p = vp.project(s_geom.points[i]);

                    if (Math.Abs(p.X - e.X) < 6 && Math.Abs(p.Y - e.Y) < 6)
                    {

                        this.Cursor = Cursors.SizeAll;

                        //if (e.Button == MouseButtons.Left)
                        //{

                        //    // Move a point with a mouse. Works shitty. 
                        //    p = vp.unproject(new Point(e.X, e.Y));

                        //    current_sym.geometry[selected_geometry_idx].points[selected_point_idx] = p;

                        //    point_x_val.Value = p.X;
                        //    point_y_val.Value = p.Y;

                        //    Refresh();
                        //}


                    }


                }

            }
   
            if (e.Button == MouseButtons.Middle)
            {

                int dX = _mouse_startX - e.X;
                int dY = _mouse_startY - e.Y;


                vp.pan(dX, dY);
                _mouse_startX = e.X;
                _mouse_startY = e.Y;
                canvas.Refresh();

            }


        }

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {

            //
            var zoom_mouse = new Point(e.X, e.Y);

            var zoom_mouse_proj = vp.unproject(zoom_mouse);


            float k = 2f;
            if (Control.ModifierKeys == Keys.Alt)
                k = 1.2f;

            if (e.Delta > 0)
            {
                vp.projection_scale *= k;
                // Lest zoom to cursor
                var r = vp.project(zoom_mouse_proj);

                int dx =  r.X - zoom_mouse.X;
                int dy = r.Y - zoom_mouse.Y;
                vp.pan(dx, dy);
            }
            else
            {
                vp.projection_scale /= k;
            }
 
            canvas.Refresh();

        }
        //
     

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (sym_expo_textbox.Focused) return;
            e.Handled = true;
            switch (e.KeyCode)
            {

                case Keys.Oemplus:
                    vp.projection_scale *= 2;
                    Refresh();
                    break;
                case Keys.OemMinus:
                    vp.projection_scale /= 2;
                    Refresh();
                    break;
                case Keys.Delete: // Delete slected point 
                    if (selected_geometry_idx != -1 && selected_point_idx != -1)
                    {
                        if (current_sym.geometry[selected_geometry_idx].points.Count() != 1) // if only one point  - not need to delete it
                        {
                            current_sym.geometry[selected_geometry_idx].removePoint(selected_point_idx);

                            selected_point_idx = -1;
                            Refresh();
                        }
                        
                    }

                    break;

                case Keys.Left:

                    if (selected_geometry_idx != -1 )
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(-1, 0);
                        }
                        else
                        { // else move whole geometry

                            current_sym.geometry[selected_geometry_idx].move(-1, 0);

                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        current_sym.move(-1, 0);
                    }

                    Refresh();

                    break;
                case Keys.Right:
                    if (selected_geometry_idx != -1 )
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(1, 0);
                        }
                        else
                        { // else move whole geometry

                            current_sym.geometry[selected_geometry_idx].move(1, 0);

                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        current_sym.move(1, 0);
                    }

                    Refresh();

                    break;
                case Keys.Up:
                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(0, -1);
                        }
                        else
                        { // else move whole geometry

                            current_sym.geometry[selected_geometry_idx].move(0, -1);

                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        current_sym.move(0, -1);
                    }

                    Refresh();

                    break;
                case Keys.Down: 
                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(0, 1);
                        }
                        else { // else move whole geometry

                            current_sym.geometry[selected_geometry_idx].move(0,1);

                        }

                        

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL) {
                        current_sym.move(0,1);
                    }
                    
                    Refresh();
                    break;

                default:
                    e.Handled = false;
                    break;
                    

            }

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
     
            this.KeyPreview = true;

        }


       private void updateExplorerList()
        {


            geom_explorer.Items.Clear();

            foreach (var g in current_sym.geometry)
            {
                string desctiption = "lineString";

                if (g.type == GeometryType.CIRCLE)
                    desctiption = "circle";
                else if (g.type == GeometryType.POLYGON)
                    desctiption = "polygon";
                else if (g.type == GeometryType.POINT)
                    desctiption = "point";


                geom_explorer.Items.Add(desctiption);
            }

            if (selected_geometry_idx != -1) {
                geom_explorer.SelectedIndex = selected_geometry_idx;
            }


        }

       private void selectGeometry(int idx)
        {

            _fired_by_user = false;
            selected_geometry_idx = idx;

            // Set things to default
            foreach (var g in current_sym.geometry)
                g.selected = false;

            

            color_prop_combo.Enabled = false;
            width_prop.Enabled = false;
            transp_prop.Enabled = false;
            radius_prop.Enabled = false;
            point_x_val.Enabled = false;
            point_y_val.Enabled = false;
            circle_filled_prop.Enabled = false;




            //


            var s_geom = current_sym.geometry[selected_geometry_idx];
            s_geom.selected = true;

            color_prop_combo.Enabled = true;
            transp_prop.Enabled = true;
            color_prop_combo.SelectedIndex = color_prop_combo.FindStringExact(s_geom.color);
            transp_prop.Value = s_geom.transparency;

            if (s_geom.type == GeometryType.LINE || s_geom.type == GeometryType.POINT)
            {

                width_prop.Enabled = true;
                width_prop.Value = current_sym.geometry[selected_geometry_idx].penWidth;
            }
            else if (s_geom.type == GeometryType.CIRCLE)
            {
                var ci = s_geom as HCircle;

                radius_prop.Enabled = true;
                circle_filled_prop.Enabled = true;
                radius_prop.Value = ci.radius;
                circle_filled_prop.Checked = ci.filled;
                if (!ci.filled)
                {
                    width_prop.Enabled = true;
                    width_prop.Value = current_sym.geometry[selected_geometry_idx].penWidth;
                    
                }

            }

            delete_itm_btn.Enabled = true;
            copy_itm_btn.Enabled = true;
            transform_group.Enabled = true;

            if (s_geom.transform != null)
            {

                rotate_value.Value = s_geom.transform.rotate_angle;
                scale_value.Value = s_geom.transform.scale;
                rot_origin_x.Value = s_geom.transform.rotate_origin.X;
                rot_origin_y.Value = s_geom.transform.rotate_origin.Y;

            }
            else {
                rotate_value.Value = 0;
                scale_value.Value = 100;
                var c = s_geom.getCenterPoint();
                rot_origin_x.Value = c.X;
                rot_origin_y.Value = c.Y;
            }
            _fired_by_user = true;

            setEditiMode(Edit_mode.EDIT_GEOMETRY);
            canvas.Refresh();

        }


        private void color_prop_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                current_sym.geometry[selected_geometry_idx].color = color_prop_combo.Text;
                canvas.Refresh();
            }

        }

        private void width_prop_ValueChanged(object sender, EventArgs e)
        {
            current_sym.geometry[selected_geometry_idx].penWidth = (int)width_prop.Value;
            canvas.Refresh();
        }

        private void transp_prop_ValueChanged(object sender, EventArgs e)
        {
            current_sym.geometry[selected_geometry_idx].transparency = (int)transp_prop.Value;
            canvas.Refresh();
        }

        private void geom_explorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_point_idx = -1;
            selectGeometry(geom_explorer.SelectedIndex);
            canvas.Refresh();
        }

        private void radius_prop_ValueChanged(object sender, EventArgs e)
        {

            (current_sym.geometry[selected_geometry_idx] as HCircle).radius = (int)radius_prop.Value;
            canvas.Refresh();
        }

        private void circle_filled_prop_CheckedChanged(object sender, EventArgs e)
        {
            (current_sym.geometry[selected_geometry_idx] as HCircle).filled = circle_filled_prop.Checked;
            canvas.Refresh();
        }

        private void delete_itm_btn_Click(object sender, EventArgs e)
        {

            current_sym.geometry.RemoveAt(geom_explorer.SelectedIndex);
            selected_geometry_idx = -1;
            updateExplorerList();
            delete_itm_btn.Enabled = false;
            
          
            canvas.Refresh();
        }
        // Copy geometry
        private void copy_itm_btn_Click(object sender, EventArgs e)
        {
            current_sym.geometry.Add(current_sym.geometry[geom_explorer.SelectedIndex].Copy());
            updateExplorerList();
            geom_explorer.SelectedIndex = current_sym.geometry.Count() - 1;
            selectGeometry(current_sym.geometry.Count() - 1);
            canvas.Refresh();
            
        }

        private void rotate_val_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {


                current_sym.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));

                canvas.Refresh();
            }
        }
        // Scale changed
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                current_sym.geometry[selected_geometry_idx].setScaleTransform((int)scale_value.Value);
                canvas.Refresh();
            }
        }

        private void point_x_val_ValueChanged(object sender, EventArgs e)
        {

            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                current_sym.pivot_x = (int)point_x_val.Value;

            }
            else
            {
                var p = current_sym.geometry[selected_geometry_idx].points[selected_point_idx];
                p.X = (int)point_x_val.Value;
                current_sym.geometry[selected_geometry_idx].points[selected_point_idx] = p;
            }
            canvas.Refresh();
        }

        private void point_y_val_ValueChanged(object sender, EventArgs e)
        {
            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                current_sym.pivot_y = (int)point_y_val.Value;

            }
            else
            {
                var p = current_sym.geometry[selected_geometry_idx].points[selected_point_idx];
                p.Y = (int)point_y_val.Value;
                current_sym.geometry[selected_geometry_idx].points[selected_point_idx] = p;        
            }
            canvas.Refresh();
        }

      

        


        /// Strip menu handlers

        // Open file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();

                        HPGL.parseSymbol(fileContent, ref current_sym);

                        HPGL.parseGeometry(ref current_sym);

                        current_sym.filename = filePath;

                        vp.center = new Point(current_sym.pivot_x, current_sym.pivot_y);

                        updateExplorerList();

                        setEditiMode(Edit_mode.EDIT_SYMBOL); 

                        sym_name_textbox.Text = current_sym.name;
                  
                        sym_expo_textbox.Text = current_sym.expo;

                        vp.projection_scale = 1;
                        this.Refresh();
                        this.Focus();
                    }
                }
            }
        }
        // Set ADD_POINTS mode
        private void addPoints_menu_btn_Click(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {

                var s_geom = current_sym.geometry[selected_geometry_idx];

                if (s_geom.type != GeometryType.CIRCLE && s_geom.type != GeometryType.POINT) {

                    if (_edit_mode == Edit_mode.ADD_POINT)
                    
                       setEditiMode(Edit_mode.EDIT_GEOMETRY);
                    
                    else 

                        setEditiMode(Edit_mode.ADD_POINT);
                   

                }

            }
        }
        // Set EDIT_SYMBOL mode
        private void editSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEditiMode(Edit_mode.EDIT_SYMBOL);
            canvas.Refresh();
        }
        // Save 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_sym.filename == null) {

                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }
            try
            {
                File.WriteAllText(current_sym.filename, current_sym.serialize());
            }
            catch {

            }

        }
        // Save as
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, current_sym.serialize());
                }

            }
        }
        // Copy symbol to clipboard
        private void copyToCpilboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = current_sym.serialize();
            Console.WriteLine(str);
            Clipboard.SetText(str);
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e) // Add line menu button click handler
        {
            setEditiMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.LINE;


        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e) // Add polygon menu button click handler
        {
            setEditiMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.POLYGON;


        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e) // Add circle menu button click handler
        {
            setEditiMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.CIRCLE;


        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e) // Add point menu button click handler
        {
            setEditiMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.POINT;


        }

        /// Geometry transform handlers

        private void rotate_value_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                current_sym.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));

                canvas.Refresh();
            }
        }

        private void rot_origin_x_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                current_sym.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));
              
                canvas.Refresh();
            }
        }

        private void rot_origin_y_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                current_sym.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));
              
                canvas.Refresh();
            }
        }

        private void z_minus_Click(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                int new_index = selected_geometry_idx-1;

                if (new_index < 0)
                    return;
                var item = current_sym.geometry[selected_geometry_idx];
                current_sym.geometry.RemoveAt(selected_geometry_idx);
                current_sym.geometry.Insert(new_index, item);

                selected_geometry_idx = new_index;

                updateExplorerList();
                canvas.Refresh();
            }
        }

        private void z_plus_Click(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                int new_index = selected_geometry_idx+1;

                if (new_index > current_sym.geometry.Count()-1)
                    return;

                var item = current_sym.geometry[selected_geometry_idx];
                current_sym.geometry.RemoveAt(selected_geometry_idx);
                current_sym.geometry.Insert(new_index, item);

                selected_geometry_idx = new_index;

                updateExplorerList();
                canvas.Refresh();
            }
        }

        private void scale_value_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {
 
                current_sym.geometry[selected_geometry_idx].setScaleTransform((int)scale_value.Value);

                canvas.Refresh();
            }
        }

        // Geometry explorer context menu
        private void geom_explorer_MouseDown(object sender, MouseEventArgs e)
        {
            collectionRoundMenuStrip.Items.Clear();
            collectionRoundMenuStrip.Visible = false;
            if (e.Button != MouseButtons.Right) return;
            var index = geom_explorer.IndexFromPoint(e.Location);    
            if (index != ListBox.NoMatches)
            {
                var selected_text = geom_explorer.Items[index].ToString();
                collectionRoundMenuStrip.Show(Cursor.Position);


                if (selected_text == "lineString")
                {
                    var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Convert to polygon" };
                    toolStripMenuItem1.Click += (sender2, e2) => convertGeometry(index, GeometryType.POLYGON);
                    collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
                    collectionRoundMenuStrip.Visible = true;
                }
                else if (selected_text == "polygon") {
                    var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Convert to line" };
                    toolStripMenuItem1.Click += (sender2, e2) => convertGeometry(index, GeometryType.LINE);
                    collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
                    collectionRoundMenuStrip.Visible = true;

                }

                
            }
           
        }

        private void sym_name_textbox_TextChanged(object sender, EventArgs e)
        {
            current_sym.name = sym_name_textbox.Text;
        }

        private void sym_expo_textbox_TextChanged(object sender, EventArgs e)
        {
            current_sym.expo = sym_expo_textbox.Text;
        }

        private void color_prop_combo_DrawItem(object sender, DrawItemEventArgs e)
        {
            
            if (e.Index < 0) return;
            var s = color_prop_combo.Items[e.Index];
            IList<PropertyInfo> props = new List<PropertyInfo>(s.GetType().GetProperties());

            string col_name = props[0].GetValue(s) as string;
            Color col = (Color)props[1].GetValue(s);
            var tex_col = createContrastColor(col);
            e.Graphics.FillRectangle(new SolidBrush(col), e.Bounds);
            e.Graphics.DrawString(col_name, e.Font, new SolidBrush(tex_col), e.Bounds);
            e.DrawFocusRectangle();
        }

        void convertGeometry(int idx, GeometryType to_type) {

            current_sym.geometry[idx].type = to_type;
            updateExplorerList();
            canvas.Refresh();

        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewForm prf = new PreviewForm(ref current_sym);
            prf.Show();
        }

        private void polygonOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygonOutlineToolStripMenuItem.Checked = !polygonOutlineToolStripMenuItem.Checked;
            canvas.Refresh();
        }

        public Color createContrastColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

    }
}
