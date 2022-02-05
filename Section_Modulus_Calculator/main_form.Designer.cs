
namespace Section_Modulus_Calculator
{
    partial class main_form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_zoom_value = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_sidepanel_coord = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_import = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_modify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_calculate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_close = new System.Windows.Forms.ToolStripMenuItem();
            this.glControl_main_panel = new OpenTK.GLControl();
            this.button_import = new System.Windows.Forms.Button();
            this.button_referenceaxis = new System.Windows.Forms.Button();
            this.button_calculate = new System.Windows.Forms.Button();
            this.richTextBox_result = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_zoom_value,
            this.toolStripStatusLabel_sidepanel_coord});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(782, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_zoom_value
            // 
            this.toolStripStatusLabel_zoom_value.Name = "toolStripStatusLabel_zoom_value";
            this.toolStripStatusLabel_zoom_value.Size = new System.Drawing.Size(92, 20);
            this.toolStripStatusLabel_zoom_value.Text = "Zoom: 100%";
            // 
            // toolStripStatusLabel_sidepanel_coord
            // 
            this.toolStripStatusLabel_sidepanel_coord.Name = "toolStripStatusLabel_sidepanel_coord";
            this.toolStripStatusLabel_sidepanel_coord.Size = new System.Drawing.Size(32, 20);
            this.toolStripStatusLabel_sidepanel_coord.Text = "0, 0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_file});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_file
            // 
            this.toolStripMenuItem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_import,
            this.toolStripMenuItem_modify,
            this.toolStripMenuItem_calculate,
            this.toolStripMenuItem_close});
            this.toolStripMenuItem_file.Name = "toolStripMenuItem_file";
            this.toolStripMenuItem_file.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItem_file.Text = "File";
            // 
            // toolStripMenuItem_import
            // 
            this.toolStripMenuItem_import.Name = "toolStripMenuItem_import";
            this.toolStripMenuItem_import.Size = new System.Drawing.Size(240, 26);
            this.toolStripMenuItem_import.Text = "Import Geometry";
            this.toolStripMenuItem_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // toolStripMenuItem_modify
            // 
            this.toolStripMenuItem_modify.Name = "toolStripMenuItem_modify";
            this.toolStripMenuItem_modify.Size = new System.Drawing.Size(240, 26);
            this.toolStripMenuItem_modify.Text = "Modify Reference Axis";
            this.toolStripMenuItem_modify.Click += new System.EventHandler(this.button_referenceaxis_Click);
            // 
            // toolStripMenuItem_calculate
            // 
            this.toolStripMenuItem_calculate.Name = "toolStripMenuItem_calculate";
            this.toolStripMenuItem_calculate.Size = new System.Drawing.Size(240, 26);
            this.toolStripMenuItem_calculate.Text = "Calculate";
            this.toolStripMenuItem_calculate.Click += new System.EventHandler(this.button_calculate_Click);
            // 
            // toolStripMenuItem_close
            // 
            this.toolStripMenuItem_close.Name = "toolStripMenuItem_close";
            this.toolStripMenuItem_close.Size = new System.Drawing.Size(240, 26);
            this.toolStripMenuItem_close.Text = "Exit";
            this.toolStripMenuItem_close.Click += new System.EventHandler(this.toolStripMenuItem_close_ItemClicked);
            // 
            // glControl_main_panel
            // 
            this.glControl_main_panel.BackColor = System.Drawing.Color.Black;
            this.glControl_main_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl_main_panel.Location = new System.Drawing.Point(13, 33);
            this.glControl_main_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.glControl_main_panel.Name = "glControl_main_panel";
            this.glControl_main_panel.Size = new System.Drawing.Size(48, 40);
            this.glControl_main_panel.TabIndex = 2;
            this.glControl_main_panel.VSync = true;
            this.glControl_main_panel.Load += new System.EventHandler(this.glControl_main_panel_Load);
            this.glControl_main_panel.SizeChanged += new System.EventHandler(this.glControl_main_panel_SizeChanged);
            this.glControl_main_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_main_panel_Paint);
            this.glControl_main_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_main_panel_KeyDown);
            this.glControl_main_panel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_main_panel_KeyUp);
            this.glControl_main_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_main_panel_MouseDown);
            this.glControl_main_panel.MouseEnter += new System.EventHandler(this.glControl_main_panel_MouseEnter);
            this.glControl_main_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_main_panel_MouseMove);
            this.glControl_main_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_main_panel_MouseUp);
            this.glControl_main_panel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl_main_panel_MouseWheel);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(521, 35);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(150, 40);
            this.button_import.TabIndex = 3;
            this.button_import.Text = "Import Geometry";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // button_referenceaxis
            // 
            this.button_referenceaxis.Location = new System.Drawing.Point(521, 80);
            this.button_referenceaxis.Name = "button_referenceaxis";
            this.button_referenceaxis.Size = new System.Drawing.Size(150, 40);
            this.button_referenceaxis.TabIndex = 4;
            this.button_referenceaxis.Text = "Reference Axis";
            this.button_referenceaxis.UseVisualStyleBackColor = true;
            this.button_referenceaxis.Click += new System.EventHandler(this.button_referenceaxis_Click);
            // 
            // button_calculate
            // 
            this.button_calculate.Location = new System.Drawing.Point(521, 125);
            this.button_calculate.Name = "button_calculate";
            this.button_calculate.Size = new System.Drawing.Size(150, 40);
            this.button_calculate.TabIndex = 5;
            this.button_calculate.Text = "Calculate";
            this.button_calculate.UseVisualStyleBackColor = true;
            this.button_calculate.Click += new System.EventHandler(this.button_calculate_Click);
            // 
            // richTextBox_result
            // 
            this.richTextBox_result.Location = new System.Drawing.Point(448, 187);
            this.richTextBox_result.Name = "richTextBox_result";
            this.richTextBox_result.Size = new System.Drawing.Size(322, 241);
            this.richTextBox_result.TabIndex = 6;
            this.richTextBox_result.Text = "XX\nXX\nXX\nXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\nXX\nXX\nXX\nXXXXXXXXXXXXXXXXXXXXXXXXX" +
    "XXXXXXXXXXXX\nXX\nXX\nXX\nXX\nXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\nXX\nXX\nXX\nXX";
            this.richTextBox_result.WordWrap = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.richTextBox_result);
            this.Controls.Add(this.button_calculate);
            this.Controls.Add(this.button_referenceaxis);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.glControl_main_panel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "main_form";
            this.Text = "Section Modulus Calculator";
            this.Load += new System.EventHandler(this.main_form_Load);
            this.SizeChanged += new System.EventHandler(this.main_form_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_file;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_import;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_modify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_calculate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_close;
        private OpenTK.GLControl glControl_main_panel;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Button button_referenceaxis;
        private System.Windows.Forms.Button button_calculate;
        private System.Windows.Forms.RichTextBox richTextBox_result;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_zoom_value;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_sidepanel_coord;
        private System.Windows.Forms.Timer timer1;
    }
}