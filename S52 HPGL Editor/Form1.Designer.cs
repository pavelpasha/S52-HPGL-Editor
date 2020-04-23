namespace S52_HPGL_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.Panel();
            this.status_string = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToCpilboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPoints_menu_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.ed_sym_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.color_prop_combo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.width_prop = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.transp_prop = new System.Windows.Forms.NumericUpDown();
            this.geom_explorer = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radius_prop = new System.Windows.Forms.NumericUpDown();
            this.delete_itm_btn = new System.Windows.Forms.Button();
            this.copy_itm_btn = new System.Windows.Forms.Button();
            this.transform_group = new System.Windows.Forms.GroupBox();
            this.scale_value = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.rot_origin_y = new System.Windows.Forms.NumericUpDown();
            this.rotate_value = new System.Windows.Forms.NumericUpDown();
            this.rot_origin_x = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.current_point_label = new System.Windows.Forms.Label();
            this.point_x_val = new System.Windows.Forms.NumericUpDown();
            this.point_y_val = new System.Windows.Forms.NumericUpDown();
            this.circle_filled_prop = new System.Windows.Forms.CheckBox();
            this.z_minus = new System.Windows.Forms.Button();
            this.z_plus = new System.Windows.Forms.Button();
            this.sym_name_textbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sym_expo_textbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonOutlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.width_prop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transp_prop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radius_prop)).BeginInit();
            this.transform_group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scale_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rot_origin_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotate_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rot_origin_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point_x_val)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point_y_val)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AccessibleName = "";
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.SystemColors.Control;
            this.canvas.Location = new System.Drawing.Point(12, 28);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(585, 456);
            this.canvas.TabIndex = 0;
            this.canvas.SizeChanged += new System.EventHandler(this.canvas_SizeChanged);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            // 
            // status_string
            // 
            this.status_string.AutoSize = true;
            this.status_string.Location = new System.Drawing.Point(12, 498);
            this.status_string.Name = "status_string";
            this.status_string.Size = new System.Drawing.Size(0, 13);
            this.status_string.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.addToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(763, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.copyToCpilboardToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // copyToCpilboardToolStripMenuItem
            // 
            this.copyToCpilboardToolStripMenuItem.Name = "copyToCpilboardToolStripMenuItem";
            this.copyToCpilboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToCpilboardToolStripMenuItem.Text = "Copy to cpilboard";
            this.copyToCpilboardToolStripMenuItem.Click += new System.EventHandler(this.copyToCpilboardToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripMenuItem,
            this.polygonToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.pointToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.addToolStripMenuItem.Text = "Add geometry";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolStripMenuItem_Click);
            // 
            // polygonToolStripMenuItem
            // 
            this.polygonToolStripMenuItem.Name = "polygonToolStripMenuItem";
            this.polygonToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.polygonToolStripMenuItem.Text = "Polygon";
            this.polygonToolStripMenuItem.Click += new System.EventHandler(this.polygonToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.circleToolStripMenuItem_Click);
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.pointToolStripMenuItem.Text = "Point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.pointToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPoints_menu_btn,
            this.ed_sym_mode});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.editToolStripMenuItem.Text = "Edit Mode";
            // 
            // addPoints_menu_btn
            // 
            this.addPoints_menu_btn.Name = "addPoints_menu_btn";
            this.addPoints_menu_btn.Size = new System.Drawing.Size(136, 22);
            this.addPoints_menu_btn.Text = "Add Points";
            this.addPoints_menu_btn.Click += new System.EventHandler(this.addPoints_menu_btn_Click);
            // 
            // ed_sym_mode
            // 
            this.ed_sym_mode.Name = "ed_sym_mode";
            this.ed_sym_mode.Size = new System.Drawing.Size(136, 22);
            this.ed_sym_mode.Text = "Edit symbol";
            this.ed_sym_mode.Click += new System.EventHandler(this.editSymbolToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Color:";
            // 
            // color_prop_combo
            // 
            this.color_prop_combo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.color_prop_combo.Enabled = false;
            this.color_prop_combo.FormattingEnabled = true;
            this.color_prop_combo.Location = new System.Drawing.Point(649, 30);
            this.color_prop_combo.Name = "color_prop_combo";
            this.color_prop_combo.Size = new System.Drawing.Size(68, 21);
            this.color_prop_combo.TabIndex = 5;
            this.color_prop_combo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.color_prop_combo_DrawItem);
            this.color_prop_combo.SelectedIndexChanged += new System.EventHandler(this.color_prop_combo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(603, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width:";
            // 
            // width_prop
            // 
            this.width_prop.Enabled = false;
            this.width_prop.Location = new System.Drawing.Point(649, 58);
            this.width_prop.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.width_prop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.width_prop.Name = "width_prop";
            this.width_prop.Size = new System.Drawing.Size(68, 20);
            this.width_prop.TabIndex = 6;
            this.width_prop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.width_prop.ValueChanged += new System.EventHandler(this.width_prop_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Transparency:";
            // 
            // transp_prop
            // 
            this.transp_prop.Enabled = false;
            this.transp_prop.Location = new System.Drawing.Point(683, 82);
            this.transp_prop.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.transp_prop.Name = "transp_prop";
            this.transp_prop.Size = new System.Drawing.Size(68, 20);
            this.transp_prop.TabIndex = 6;
            this.transp_prop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.transp_prop.ValueChanged += new System.EventHandler(this.transp_prop_ValueChanged);
            // 
            // geom_explorer
            // 
            this.geom_explorer.FormattingEnabled = true;
            this.geom_explorer.Location = new System.Drawing.Point(606, 323);
            this.geom_explorer.Name = "geom_explorer";
            this.geom_explorer.Size = new System.Drawing.Size(145, 160);
            this.geom_explorer.TabIndex = 7;
            this.geom_explorer.SelectedIndexChanged += new System.EventHandler(this.geom_explorer_SelectedIndexChanged);
            this.geom_explorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.geom_explorer_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(598, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Radius";
            // 
            // radius_prop
            // 
            this.radius_prop.Enabled = false;
            this.radius_prop.Location = new System.Drawing.Point(683, 109);
            this.radius_prop.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.radius_prop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.radius_prop.Name = "radius_prop";
            this.radius_prop.Size = new System.Drawing.Size(68, 20);
            this.radius_prop.TabIndex = 6;
            this.radius_prop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.radius_prop.ValueChanged += new System.EventHandler(this.radius_prop_ValueChanged);
            // 
            // delete_itm_btn
            // 
            this.delete_itm_btn.Enabled = false;
            this.delete_itm_btn.Location = new System.Drawing.Point(603, 298);
            this.delete_itm_btn.Name = "delete_itm_btn";
            this.delete_itm_btn.Size = new System.Drawing.Size(75, 23);
            this.delete_itm_btn.TabIndex = 8;
            this.delete_itm_btn.Text = "delete";
            this.delete_itm_btn.UseVisualStyleBackColor = true;
            this.delete_itm_btn.Click += new System.EventHandler(this.delete_itm_btn_Click);
            // 
            // copy_itm_btn
            // 
            this.copy_itm_btn.Enabled = false;
            this.copy_itm_btn.Location = new System.Drawing.Point(676, 298);
            this.copy_itm_btn.Name = "copy_itm_btn";
            this.copy_itm_btn.Size = new System.Drawing.Size(75, 23);
            this.copy_itm_btn.TabIndex = 8;
            this.copy_itm_btn.Text = "copy";
            this.copy_itm_btn.UseVisualStyleBackColor = true;
            this.copy_itm_btn.Click += new System.EventHandler(this.copy_itm_btn_Click);
            // 
            // transform_group
            // 
            this.transform_group.Controls.Add(this.scale_value);
            this.transform_group.Controls.Add(this.label6);
            this.transform_group.Controls.Add(this.rot_origin_y);
            this.transform_group.Controls.Add(this.rotate_value);
            this.transform_group.Controls.Add(this.rot_origin_x);
            this.transform_group.Controls.Add(this.label8);
            this.transform_group.Controls.Add(this.label5);
            this.transform_group.Enabled = false;
            this.transform_group.Location = new System.Drawing.Point(606, 163);
            this.transform_group.Name = "transform_group";
            this.transform_group.Size = new System.Drawing.Size(145, 127);
            this.transform_group.TabIndex = 10;
            this.transform_group.TabStop = false;
            this.transform_group.Text = "Transform";
            // 
            // scale_value
            // 
            this.scale_value.Location = new System.Drawing.Point(67, 92);
            this.scale_value.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.scale_value.Name = "scale_value";
            this.scale_value.Size = new System.Drawing.Size(69, 20);
            this.scale_value.TabIndex = 1;
            this.scale_value.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.scale_value.ValueChanged += new System.EventHandler(this.scale_value_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Scale:";
            // 
            // rot_origin_y
            // 
            this.rot_origin_y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rot_origin_y.Location = new System.Drawing.Point(77, 62);
            this.rot_origin_y.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.rot_origin_y.Name = "rot_origin_y";
            this.rot_origin_y.Size = new System.Drawing.Size(59, 20);
            this.rot_origin_y.TabIndex = 12;
            this.rot_origin_y.ValueChanged += new System.EventHandler(this.rot_origin_y_ValueChanged);
            // 
            // rotate_value
            // 
            this.rotate_value.Location = new System.Drawing.Point(67, 16);
            this.rotate_value.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.rotate_value.Name = "rotate_value";
            this.rotate_value.Size = new System.Drawing.Size(69, 20);
            this.rotate_value.TabIndex = 1;
            this.rotate_value.ValueChanged += new System.EventHandler(this.rotate_value_ValueChanged);
            // 
            // rot_origin_x
            // 
            this.rot_origin_x.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rot_origin_x.Location = new System.Drawing.Point(11, 62);
            this.rot_origin_x.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.rot_origin_x.Name = "rot_origin_x";
            this.rot_origin_x.Size = new System.Drawing.Size(59, 20);
            this.rot_origin_x.TabIndex = 12;
            this.rot_origin_x.ValueChanged += new System.EventHandler(this.rot_origin_x_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Rotation origin:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Rotate:";
            // 
            // current_point_label
            // 
            this.current_point_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.current_point_label.AutoSize = true;
            this.current_point_label.Location = new System.Drawing.Point(159, 497);
            this.current_point_label.Name = "current_point_label";
            this.current_point_label.Size = new System.Drawing.Size(78, 13);
            this.current_point_label.TabIndex = 11;
            this.current_point_label.Text = "Selected point:";
            // 
            // point_x_val
            // 
            this.point_x_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.point_x_val.Enabled = false;
            this.point_x_val.Location = new System.Drawing.Point(243, 495);
            this.point_x_val.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.point_x_val.Name = "point_x_val";
            this.point_x_val.Size = new System.Drawing.Size(71, 20);
            this.point_x_val.TabIndex = 12;
            this.point_x_val.ValueChanged += new System.EventHandler(this.point_x_val_ValueChanged);
            // 
            // point_y_val
            // 
            this.point_y_val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.point_y_val.Enabled = false;
            this.point_y_val.Location = new System.Drawing.Point(320, 495);
            this.point_y_val.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.point_y_val.Name = "point_y_val";
            this.point_y_val.Size = new System.Drawing.Size(71, 20);
            this.point_y_val.TabIndex = 12;
            this.point_y_val.ValueChanged += new System.EventHandler(this.point_y_val_ValueChanged);
            // 
            // circle_filled_prop
            // 
            this.circle_filled_prop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.circle_filled_prop.AutoSize = true;
            this.circle_filled_prop.Enabled = false;
            this.circle_filled_prop.Location = new System.Drawing.Point(701, 135);
            this.circle_filled_prop.Name = "circle_filled_prop";
            this.circle_filled_prop.Size = new System.Drawing.Size(50, 17);
            this.circle_filled_prop.TabIndex = 13;
            this.circle_filled_prop.Text = "Filled";
            this.circle_filled_prop.UseVisualStyleBackColor = true;
            this.circle_filled_prop.CheckedChanged += new System.EventHandler(this.circle_filled_prop_CheckedChanged);
            // 
            // z_minus
            // 
            this.z_minus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.z_minus.Location = new System.Drawing.Point(609, 487);
            this.z_minus.Name = "z_minus";
            this.z_minus.Size = new System.Drawing.Size(64, 23);
            this.z_minus.TabIndex = 14;
            this.z_minus.Text = "Z -";
            this.z_minus.UseVisualStyleBackColor = true;
            this.z_minus.Click += new System.EventHandler(this.z_minus_Click);
            // 
            // z_plus
            // 
            this.z_plus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.z_plus.Location = new System.Drawing.Point(683, 487);
            this.z_plus.Name = "z_plus";
            this.z_plus.Size = new System.Drawing.Size(67, 23);
            this.z_plus.TabIndex = 14;
            this.z_plus.Text = "Z +";
            this.z_plus.UseVisualStyleBackColor = true;
            this.z_plus.Click += new System.EventHandler(this.z_plus_Click);
            // 
            // sym_name_textbox
            // 
            this.sym_name_textbox.Location = new System.Drawing.Point(329, 2);
            this.sym_name_textbox.MaxLength = 8;
            this.sym_name_textbox.Name = "sym_name_textbox";
            this.sym_name_textbox.Size = new System.Drawing.Size(79, 20);
            this.sym_name_textbox.TabIndex = 15;
            this.sym_name_textbox.TextChanged += new System.EventHandler(this.sym_name_textbox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Menu;
            this.label9.Location = new System.Drawing.Point(285, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Name:";
            // 
            // sym_expo_textbox
            // 
            this.sym_expo_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sym_expo_textbox.Location = new System.Drawing.Point(465, 2);
            this.sym_expo_textbox.MaxLength = 150;
            this.sym_expo_textbox.Name = "sym_expo_textbox";
            this.sym_expo_textbox.Size = new System.Drawing.Size(286, 20);
            this.sym_expo_textbox.TabIndex = 15;
            this.sym_expo_textbox.TextChanged += new System.EventHandler(this.sym_expo_textbox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(420, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Expo:";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewToolStripMenuItem,
            this.polygonOutlineToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // polygonOutlineToolStripMenuItem
            // 
            this.polygonOutlineToolStripMenuItem.Checked = true;
            this.polygonOutlineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.polygonOutlineToolStripMenuItem.Name = "polygonOutlineToolStripMenuItem";
            this.polygonOutlineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.polygonOutlineToolStripMenuItem.Text = "Polygon outline";
            this.polygonOutlineToolStripMenuItem.Click += new System.EventHandler(this.polygonOutlineToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(763, 520);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sym_expo_textbox);
            this.Controls.Add(this.sym_name_textbox);
            this.Controls.Add(this.z_plus);
            this.Controls.Add(this.z_minus);
            this.Controls.Add(this.transform_group);
            this.Controls.Add(this.circle_filled_prop);
            this.Controls.Add(this.point_y_val);
            this.Controls.Add(this.point_x_val);
            this.Controls.Add(this.current_point_label);
            this.Controls.Add(this.copy_itm_btn);
            this.Controls.Add(this.delete_itm_btn);
            this.Controls.Add(this.geom_explorer);
            this.Controls.Add(this.radius_prop);
            this.Controls.Add(this.transp_prop);
            this.Controls.Add(this.width_prop);
            this.Controls.Add(this.color_prop_combo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.status_string);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "S52 HPGL Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.width_prop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transp_prop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radius_prop)).EndInit();
            this.transform_group.ResumeLayout(false);
            this.transform_group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scale_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rot_origin_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotate_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rot_origin_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point_x_val)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point_y_val)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.Label status_string;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox color_prop_combo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown width_prop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown transp_prop;
        private System.Windows.Forms.ListBox geom_explorer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown radius_prop;
        private System.Windows.Forms.Button delete_itm_btn;
        private System.Windows.Forms.Button copy_itm_btn;
        private System.Windows.Forms.GroupBox transform_group;
        private System.Windows.Forms.NumericUpDown scale_value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown rotate_value;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label current_point_label;
        private System.Windows.Forms.NumericUpDown point_x_val;
        private System.Windows.Forms.NumericUpDown point_y_val;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.CheckBox circle_filled_prop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown rot_origin_y;
        private System.Windows.Forms.NumericUpDown rot_origin_x;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPoints_menu_btn;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button z_minus;
        private System.Windows.Forms.Button z_plus;
        private System.Windows.Forms.ToolStripMenuItem ed_sym_mode;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToCpilboardToolStripMenuItem;
        private System.Windows.Forms.TextBox sym_name_textbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox sym_expo_textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonOutlineToolStripMenuItem;
    }
}

