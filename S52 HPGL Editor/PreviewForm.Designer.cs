namespace S52_HPGL_Editor
{
    partial class PreviewForm
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
            this.antialiasing_checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scale_numeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.bg_color = new System.Windows.Forms.ComboBox();
            this.poly_outline_ckeckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.scale_numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.Location = new System.Drawing.Point(12, 45);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(404, 355);
            this.canvas.TabIndex = 0;
            this.canvas.SizeChanged += new System.EventHandler(this.canvas_SizeChanged);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // antialiasing_checkbox
            // 
            this.antialiasing_checkbox.AutoSize = true;
            this.antialiasing_checkbox.Location = new System.Drawing.Point(94, 13);
            this.antialiasing_checkbox.Name = "antialiasing_checkbox";
            this.antialiasing_checkbox.Size = new System.Drawing.Size(79, 17);
            this.antialiasing_checkbox.TabIndex = 1;
            this.antialiasing_checkbox.Text = "Antialiasing";
            this.antialiasing_checkbox.UseVisualStyleBackColor = true;
            this.antialiasing_checkbox.CheckedChanged += new System.EventHandler(this.antialiasing_checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Scale x";
            // 
            // scale_numeric
            // 
            this.scale_numeric.Location = new System.Drawing.Point(53, 11);
            this.scale_numeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.scale_numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scale_numeric.Name = "scale_numeric";
            this.scale_numeric.Size = new System.Drawing.Size(30, 20);
            this.scale_numeric.TabIndex = 3;
            this.scale_numeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scale_numeric.ValueChanged += new System.EventHandler(this.scale_numeric_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Background color";
            // 
            // bg_color
            // 
            this.bg_color.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.bg_color.FormattingEnabled = true;
            this.bg_color.Location = new System.Drawing.Point(355, 9);
            this.bg_color.Name = "bg_color";
            this.bg_color.Size = new System.Drawing.Size(65, 21);
            this.bg_color.TabIndex = 4;
            this.bg_color.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.bg_color_DrawItem);
            this.bg_color.SelectedIndexChanged += new System.EventHandler(this.bg_color_SelectedIndexChanged);
            // 
            // poly_outline_ckeckbox
            // 
            this.poly_outline_ckeckbox.AutoSize = true;
            this.poly_outline_ckeckbox.Location = new System.Drawing.Point(175, 13);
            this.poly_outline_ckeckbox.Name = "poly_outline_ckeckbox";
            this.poly_outline_ckeckbox.Size = new System.Drawing.Size(80, 17);
            this.poly_outline_ckeckbox.TabIndex = 5;
            this.poly_outline_ckeckbox.Text = "Poly outline";
            this.poly_outline_ckeckbox.UseVisualStyleBackColor = true;
            this.poly_outline_ckeckbox.CheckedChanged += new System.EventHandler(this.poly_outline_ckeckbox_CheckedChanged);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 412);
            this.Controls.Add(this.poly_outline_ckeckbox);
            this.Controls.Add(this.bg_color);
            this.Controls.Add(this.scale_numeric);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.antialiasing_checkbox);
            this.Controls.Add(this.canvas);
            this.Name = "PreviewForm";
            this.Text = "PreviewForm";
            ((System.ComponentModel.ISupportInitialize)(this.scale_numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.CheckBox antialiasing_checkbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown scale_numeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox bg_color;
        private System.Windows.Forms.CheckBox poly_outline_ckeckbox;
    }
}