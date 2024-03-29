﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace S52_HPGL_Editor
{


    public partial class Form1 : Form
    {

        ViewPort vp;
        Symbol symbol;
        private readonly ContextMenuStrip collectionRoundMenuStrip;

        int _mouse_startX, _mouse_startY;
        bool _fired_by_user = true; // If value changed by user

        GeometryType _addMode = GeometryType.NONE;  // Store geometry type of geometry wich we are going to add
        Edit_mode _edit_mode = Edit_mode.EDIT_GEOMETRY;

        int selected_geometry_idx = -1; // index of geometry wich is under editing
        int selected_point_idx = -1;

        enum Edit_mode
        {
            ADD_GEOM,            // Add new geometry of selected type   
            ADD_POINT,          // Add new points to selected geom (except circle and point)    
            EDIT_SYMBOL,       // Edit whole symbol
            EDIT_GEOMETRY      // Edit whole geometry
        }


        private void setEditMode(Edit_mode mode)
        {

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
                symbol.unselectAll();
                selected_geometry_idx = -1;
                selected_point_idx = -1;

                point_x_val.Enabled = true;
                point_y_val.Enabled = true;

                point_x_val.Value = symbol.pivot_x;
                point_y_val.Value = symbol.pivot_y;

                current_point_label.Text = "Pivot point:";
            }


        }

        private void moveSelectedPoint(int dx, int dy)
        {

            symbol.geometry[selected_geometry_idx].movePoint(selected_point_idx, dx, dy);

        }

        private void selectPoint(int idx)
        {
            selected_point_idx = idx;
            if (idx != -1)
            {
                var p = symbol.geometry[selected_geometry_idx].points[selected_point_idx];

                point_x_val.Enabled = true;
                point_y_val.Enabled = true;

                point_x_val.Value = p.X;
                point_y_val.Value = p.Y;

            }
            else {

                point_x_val.Enabled = false;
                point_y_val.Enabled = false;
                point_x_val.Value = 0;
                point_y_val.Value = 0;
            }

            canvas.Refresh();
        }

        private void selectGeometry(int idx, bool change_mode = true)
        {
            Console.WriteLine(change_mode);
            _fired_by_user = false;
            selected_geometry_idx = idx;
            selected_point_idx = -1;

            // Set things to default
            foreach (var g in symbol.geometry)
                g.selected = false;

            color_prop_combo.Enabled = false;
            width_prop.Enabled = false;
            transp_prop.Enabled = false;
            radius_prop.Enabled = false;
            point_x_val.Enabled = false;
            point_y_val.Enabled = false;
            circle_filled_prop.Enabled = false;
            //

            var s_geom = symbol.geometry[selected_geometry_idx];
            s_geom.selected = true;

            color_prop_combo.Enabled = true;
            transp_prop.Enabled = true;
            color_prop_combo.SelectedIndex = color_prop_combo.FindStringExact(s_geom.color);
            transp_prop.Value = s_geom.transparency;

            width_prop.Enabled = true;
            width_prop.Value = symbol.geometry[selected_geometry_idx].penWidth;

            if (s_geom.type == GeometryType.CIRCLE)
            {
                var ci = s_geom as HCircle;

                radius_prop.Enabled = true;
                circle_filled_prop.Enabled = true;
                radius_prop.Value = ci.radius;
                circle_filled_prop.Checked = ci.filled;
                if (!ci.filled)
                {
                    width_prop.Enabled = true;
                    width_prop.Value = symbol.geometry[selected_geometry_idx].penWidth;

                }

            }

            transform_group.Enabled = true;

            if (s_geom.transform != null)
            {
                // TODO: need to find out why exceptions periodicly throws there
                try
                {
                    rotate_value.Value = s_geom.transform.rotate_angle;
                    scale_value.Value = s_geom.transform.scale;
                    rot_origin_x.Value = s_geom.transform.rotate_origin.X;
                    rot_origin_y.Value = s_geom.transform.rotate_origin.Y;
                }
                catch { }

            }
            else
            {
                rotate_value.Value = 0;
                scale_value.Value = 100;
                var c = s_geom.getCenterPoint();
                rot_origin_x.Value = c.X;
                rot_origin_y.Value = c.Y;
            }
            _fired_by_user = true;

            if (change_mode)
            {
                setEditMode(Edit_mode.EDIT_GEOMETRY);
            }
            canvas.Refresh();

        }

        public Form1()
        {
            InitializeComponent();
            vp = new ViewPort();
            symbol = new Symbol();
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

            symbol.paint(ref g, ref vp, polygonOutlineToolStripMenuItem.Checked);


            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                // Draw symbol pivot point

                var origin = vp.project(new Point(symbol.pivot_x, symbol.pivot_y));

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
                        var p = symbol.geometry[selected_geometry_idx].points[selected_point_idx];

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
                        setEditMode(Edit_mode.EDIT_GEOMETRY);
                    }
                    else if (_addMode == GeometryType.POINT)
                    {
                        new_geometry = new HPoint();
                        setEditMode(Edit_mode.EDIT_GEOMETRY);

                    }
                    else if (_addMode == GeometryType.LINE)
                    {
                        new_geometry = new HLineString();
                        setEditMode(Edit_mode.ADD_POINT);
                    }
                    else if (_addMode == GeometryType.POLYGON)
                    {
                        new_geometry = new HPolygon();
                        setEditMode(Edit_mode.ADD_POINT);
                    }
                    var p = vp.unproject(new Point(e.X, e.Y));
                    new_geometry.addPoint(p);
                    symbol.geometry.Add(new_geometry);
                    selectGeometry(symbol.geometry.Count() - 1, false);
                    updateExplorerList();
                    canvas.Refresh();
                }
                else if (_edit_mode == Edit_mode.ADD_POINT) // Append point to selected geometry
                {

                    symbol.geometry[selected_geometry_idx].points.Add(vp.unproject(new Point(e.X, e.Y)));

                    canvas.Refresh();
                }
                else
                {

                    if (selected_geometry_idx != -1)
                    {

                        var s_geom = symbol.geometry[selected_geometry_idx];

                        for (int i = 0; i < s_geom.points.Count(); i++)
                        {

                            var p = vp.project(s_geom.points[i]);

                            if (Math.Abs(p.X - e.X) < 6 && Math.Abs(p.Y - e.Y) < 6)
                            {

                                if (e.Button == MouseButtons.Left)
                                {
                                   
                                    selectPoint(i);
                                    return;

                                }

                            }


                        }
                        // If no point was selected by this click. Unselect 
                        selectPoint(-1);

                    }

                }

            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = vp.unproject(new Point(e.X, e.Y));
            status_string.Text = string.Format("X: {0}, Y: {1}", pos.X, pos.Y);

            if (_edit_mode == Edit_mode.ADD_POINT) return;

            this.Cursor = Cursors.Default;

            if (e.Button == MouseButtons.Left)
            {
                if (selected_geometry_idx != -1 && selected_point_idx != -1)
                {

                    var s_geom = symbol.geometry[selected_geometry_idx];
                    var cursor_p = vp.unproject(new Point(e.X, e.Y));

                    s_geom.setPoint(selected_point_idx, cursor_p.X, cursor_p.Y);
                    canvas.Refresh();
                    updatePointPosition();
                }
            }
            else if (e.Button == MouseButtons.Middle)
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

                int dx = r.X - zoom_mouse.X;
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
                    break;
                case Keys.OemMinus:
                    vp.projection_scale /= 2;
                    break;
                case Keys.Delete: // Delete geometry or point 
                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1)
                        {
                            if (symbol.geometry[selected_geometry_idx].points.Count() != 1) // if only one point left - not need to delete it
                            {
                                symbol.geometry[selected_geometry_idx].removePoint(selected_point_idx);
                                
                            }
                            else { // when the last point was removed -> remove a whole geometry
                                symbol.geometry.RemoveAt(geom_explorer.SelectedIndex);
                                selected_geometry_idx = -1;
                                updateExplorerList();
                            }
                            selected_point_idx = -1;
                        }
                        else
                        {

                            symbol.geometry.RemoveAt(geom_explorer.SelectedIndex);
                            selected_geometry_idx = -1;
                            updateExplorerList();
                        }

                    }

                    break;

                case Keys.Left:

                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(-1, 0);
                        }
                        else
                        { // else move whole geometry

                            symbol.geometry[selected_geometry_idx].move(-1, 0);


                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        symbol.move(-1, 0);
                    }


                    break;
                case Keys.Right:
                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(1, 0);
                        }
                        else
                        { // else move whole geometry

                            symbol.geometry[selected_geometry_idx].move(1, 0);

                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        symbol.move(1, 0);
                    }

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

                            symbol.geometry[selected_geometry_idx].move(0, -1);


                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        symbol.move(0, -1);
                    }

                    break;
                case Keys.Down:
                    if (selected_geometry_idx != -1)
                    {
                        if (selected_point_idx != -1) // if point selected - move point
                        {
                            moveSelectedPoint(0, 1);
                        }
                        else
                        { // else move whole geometry

                            symbol.geometry[selected_geometry_idx].move(0, 1);

                        }

                    }

                    if (_edit_mode == Edit_mode.EDIT_SYMBOL)
                    {
                        symbol.move(0, 1);
                    }

                    break;
                // Copy
                case Keys.C:

                    if (e.Control && selected_geometry_idx != -1)
                    {

                        var selected_geometry = symbol.geometry[selected_geometry_idx];
                        Clipboard.Clear();

                        // Set data to clipboard

                        Clipboard.SetDataObject(selected_geometry);
                        e.SuppressKeyPress = true;
                    }
                    break;

                //Paste
                case Keys.V:
                    if (e.Control)
                    {
                        // Get data from clipboard
                        HGeometry paste_result = null;

                        IDataObject data_object = Clipboard.GetDataObject();

                        if (Clipboard.ContainsData("S52_HPGL_Editor.HLineString"))
                            paste_result = (HGeometry)Clipboard.GetData("S52_HPGL_Editor.HLineString");
                        else if (Clipboard.ContainsData("S52_HPGL_Editor.HPolygon"))
                            paste_result = (HGeometry)Clipboard.GetData("S52_HPGL_Editor.HPolygon");
                        if (Clipboard.ContainsData("S52_HPGL_Editor.HPoint"))
                            paste_result = (HGeometry)Clipboard.GetData("S52_HPGL_Editor.HPoint");
                        else if (Clipboard.ContainsData("S52_HPGL_Editor.HCircle"))
                            paste_result = (HGeometry)Clipboard.GetData("S52_HPGL_Editor.HCircle");

                        if (paste_result != null)
                        {
                            symbol.geometry.Add(paste_result);
                            updateExplorerList();
                            geom_explorer.SelectedIndex = symbol.geometry.Count() - 1;
                            selectGeometry(symbol.geometry.Count() - 1);

                        }
                        e.SuppressKeyPress = true;


                    }
                    break;
                case Keys.Escape:
                    if (_edit_mode == Edit_mode.ADD_POINT)
                    {
                        setEditMode(Edit_mode.EDIT_GEOMETRY);
                    }
                    break;

                default:
                    e.Handled = false;
                    return;
                    //break;


            }

            updatePointPosition();
            Refresh();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.KeyPreview = true;

        }


        private void updateExplorerList()
        {
            _fired_by_user = false;
            geom_explorer.Items.Clear();

            foreach (var g in symbol.geometry)
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

            if (selected_geometry_idx != -1)
            {
                geom_explorer.SelectedIndex = selected_geometry_idx;
            }
            _fired_by_user = true;

        }


        private void color_prop_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                symbol.geometry[selected_geometry_idx].color = color_prop_combo.Text;
                canvas.Refresh();
            }

        }

        private void width_prop_ValueChanged(object sender, EventArgs e)
        {
            symbol.geometry[selected_geometry_idx].penWidth = (int)width_prop.Value;
            canvas.Refresh();
        }

        private void transp_prop_ValueChanged(object sender, EventArgs e)
        {
            symbol.geometry[selected_geometry_idx].transparency = (int)transp_prop.Value;
            canvas.Refresh();
        }

        private void geom_explorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_fired_by_user)
            selectGeometry(geom_explorer.SelectedIndex);
            canvas.Refresh();
        }

        private void radius_prop_ValueChanged(object sender, EventArgs e)
        {

            (symbol.geometry[selected_geometry_idx] as HCircle).radius = (int)radius_prop.Value;
            canvas.Refresh();
        }

        private void circle_filled_prop_CheckedChanged(object sender, EventArgs e)
        {
            (symbol.geometry[selected_geometry_idx] as HCircle).filled = circle_filled_prop.Checked;
            canvas.Refresh();
        }


        private void point_x_val_ValueChanged(object sender, EventArgs e)
        {

            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                symbol.pivot_x = (int)point_x_val.Value;

            }
            else // Edit point
            {
                if (selected_point_idx == -1) return;
                var p = symbol.geometry[selected_geometry_idx].points[selected_point_idx];
                p.X = (int)point_x_val.Value;
                symbol.geometry[selected_geometry_idx].setPoint(selected_point_idx, p.X, p.Y);

            }
            canvas.Refresh();
        }

        private void point_y_val_ValueChanged(object sender, EventArgs e)
        {
            if (_edit_mode == Edit_mode.EDIT_SYMBOL)
            {

                symbol.pivot_y = (int)point_y_val.Value;

            }
            else
            {
                if (selected_point_idx == -1) return;
                var p = symbol.geometry[selected_geometry_idx].points[selected_point_idx];
                p.Y = (int)point_y_val.Value;
                symbol.geometry[selected_geometry_idx].setPoint(selected_point_idx, p.X, p.Y);

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

                        HPGL.parseSymbol(fileContent, ref symbol);

                        HPGL.parseGeometry(ref symbol);

                        symbol.filename = filePath;

                        vp.center = new Point(symbol.pivot_x, symbol.pivot_y);

                        updateExplorerList();

                        setEditMode(Edit_mode.EDIT_SYMBOL);

                        sym_name_textbox.Text = symbol.name;

                        sym_expo_textbox.Text = symbol.expo;

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

                var s_geom = symbol.geometry[selected_geometry_idx];

                if (s_geom.type != GeometryType.CIRCLE && s_geom.type != GeometryType.POINT)
                {

                    if (_edit_mode == Edit_mode.ADD_POINT)

                        setEditMode(Edit_mode.EDIT_GEOMETRY);

                    else

                        setEditMode(Edit_mode.ADD_POINT);


                }

            }
        }
        // Set EDIT_SYMBOL mode
        private void editSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEditMode(Edit_mode.EDIT_SYMBOL);
            canvas.Refresh();
        }
        // Save 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (symbol.filename == null)
            {

                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }
            try
            {
                File.WriteAllText(symbol.filename, symbol.serialize());
            }
            catch
            {

            }

        }
        // Save as
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.FileName = symbol.name + ".txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, symbol.serialize());
                symbol.filename = saveFileDialog1.FileName;
            }


        }
        // Copy symbol to clipboard
        private void copyToCpilboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = symbol.serialize();
            Console.WriteLine(str);
            Clipboard.SetText(str);
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e) // Add line menu button click handler
        {
            setEditMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.LINE;

        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e) // Add polygon menu button click handler
        {
            setEditMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.POLYGON;


        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e) // Add circle menu button click handler
        {
            setEditMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.CIRCLE;


        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e) // Add point menu button click handler
        {
            setEditMode(Edit_mode.ADD_GEOM);
            _addMode = GeometryType.POINT;


        }

        /// Geometry transform handlers
        private void rotate_value_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                symbol.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));

                canvas.Refresh();
            }
        }

        private void rot_origin_x_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                symbol.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));

                canvas.Refresh();
            }
        }

        private void rot_origin_y_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                symbol.geometry[selected_geometry_idx].setRotationTransform((int)rotate_value.Value, new Point((int)rot_origin_x.Value, (int)rot_origin_y.Value));

                canvas.Refresh();
            }
        }

        // Scale changed
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                symbol.geometry[selected_geometry_idx].setScaleTransform((int)scale_value.Value);
                canvas.Refresh();
            }
        }
        ///

        private void z_minus_Click(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                int new_index = selected_geometry_idx - 1;

                if (new_index < 0)
                    return;
                var item = symbol.geometry[selected_geometry_idx];
                symbol.geometry.RemoveAt(selected_geometry_idx);
                symbol.geometry.Insert(new_index, item);

                selected_geometry_idx = new_index;

                updateExplorerList();
                canvas.Refresh();
            }
        }

        private void z_plus_Click(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1)
            {
                int new_index = selected_geometry_idx + 1;

                if (new_index > symbol.geometry.Count() - 1)
                    return;

                var item = symbol.geometry[selected_geometry_idx];
                symbol.geometry.RemoveAt(selected_geometry_idx);
                symbol.geometry.Insert(new_index, item);

                selected_geometry_idx = new_index;

                updateExplorerList();
                canvas.Refresh();
            }
        }

        private void scale_value_ValueChanged(object sender, EventArgs e)
        {
            if (selected_geometry_idx != -1 && _fired_by_user)
            {

                symbol.geometry[selected_geometry_idx].setScaleTransform((int)scale_value.Value);

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
                else if (selected_text == "polygon")
                {
                    var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Convert to line" };
                    toolStripMenuItem1.Click += (sender2, e2) => convertGeometry(index, GeometryType.LINE);
                    collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
                    collectionRoundMenuStrip.Visible = true;

                }


            }

        }
        //

        private void sym_name_textbox_TextChanged(object sender, EventArgs e)
        {
            symbol.name = sym_name_textbox.Text;
        }

        private void sym_expo_textbox_TextChanged(object sender, EventArgs e)
        {
            symbol.expo = sym_expo_textbox.Text;
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

        void convertGeometry(int idx, GeometryType to_type)
        {

            symbol.geometry[idx].type = to_type;
            updateExplorerList();
            canvas.Refresh();

        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewForm prf = new PreviewForm(ref symbol);
            prf.Show();
        }

        private void polygonOutlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygonOutlineToolStripMenuItem.Checked = !polygonOutlineToolStripMenuItem.Checked;
            canvas.Refresh();
        }

        private Color createContrastColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }



        // Update x and y controls values if selected point was moved.
        private void updatePointPosition()
        {

            if (selected_point_idx != -1)
            {

                var p = symbol.geometry[selected_geometry_idx].points[selected_point_idx];

                _fired_by_user = false;
                point_x_val.Value = p.X;
                point_y_val.Value = p.Y;
                _fired_by_user = true;
            }

        }

    }
}
