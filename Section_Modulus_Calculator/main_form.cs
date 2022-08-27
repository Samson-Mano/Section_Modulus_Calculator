using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
// This app class structure
using Section_Modulus_Calculator.opentk_control;
using Section_Modulus_Calculator.global_variables;
using Section_Modulus_Calculator.txt_input_reader;
using Section_Modulus_Calculator.drawing_objects_store;

namespace Section_Modulus_Calculator
{
    public partial class main_form : Form
    {
        public geometry_store geom_obj { get; private set; }

        // Variables to control openTK GLControl
        // glControl wrapper class
        private opentk_main_control g_control;

        // Cursor point on the GLControl
        private PointF click_pt;

        public main_form()
        {
            InitializeComponent();
        }

        #region "Main Form Control"
        private void main_form_Load(object sender, EventArgs e)
        {
            form_size_control();
            geom_obj = new geometry_store();
        }

        private void main_form_SizeChanged(object sender, EventArgs e)
        {
            form_size_control();
        }

        private void form_size_control()
        {
            // Control the Location and size of controls
            int width = this.Width;
            int height = this.Height;

            // Location of the GLControl
            glControl_main_panel.Location = new Point(10, 35);
            glControl_main_panel.Size = new Size((int)(width * 0.6) - 20, (int)(height - 110));

            // Button location 
            int w_x = (int)(width - (width * 0.6) - 20);
            button_import.Location = new Point((int)((width * 0.6) + (w_x * 0.5) - 75), 35);
            button_calculate.Location = new Point((int)((width * 0.6) + (w_x * 0.5) - 75), 80);

            // Location of Result of textbox
            richTextBox_result.Location = new Point((int)(width * 0.6), 140);
            richTextBox_result.Size = new Size((int)(width * 0.4) - 30, height - 140 - 75);
        }
        #endregion

        #region "Button Click Events"
        private void button_import_Click(object sender, EventArgs e)
        {
            // Import Geometry
            OpenFileDialog ow = new OpenFileDialog();
            ow.DefaultExt = "*.txt";
            ow.Filter = "Samson Mano's Varai2D raw data - txt Files (*.txt)|*.txt";
            ow.ShowDialog();

            if (File.Exists(ow.FileName) == true)
            {
                txt_rd_reader txt_rd = new txt_rd_reader(ow.FileName);
                txt_to_surface_conversion surf_conv = new txt_to_surface_conversion(txt_rd);


                if (surf_conv.all_surface.Count != 0)
                {
                    // Re-initialize the geometry
                    geom_obj = new geometry_store();
                    geom_obj.add_geometry(surf_conv.all_surface, surf_conv.all_ellipses);
                    geom_obj.set_openTK_objects();

                    g_control.update_drawing_scale_and_translation(surf_conv.dr_scale, surf_conv.dr_tx, surf_conv.dr_ty, true);

                    richTextBox_result.Clear();
                    richTextBox_result.Text = txt_rd.txt_reader_ouput();
                    richTextBox_result.Text = richTextBox_result.Text + Environment.NewLine;
                    richTextBox_result.Text = richTextBox_result.Text + "Scale = " + surf_conv.dr_scale.ToString() + Environment.NewLine;
                    richTextBox_result.Text = richTextBox_result.Text + "Tx = " + surf_conv.dr_tx.ToString() + ", Ty = " + surf_conv.dr_ty.ToString() + Environment.NewLine;
                    // richTextBox_result.Clear();

                    glControl_main_panel.Invalidate();
                }
            }
        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            if (geom_obj.is_geometry_set == false)
                return;

            richTextBox_result.Clear();

            // Calculate geometric parameters
            geom_obj.set_geometric_parameter();
            // Step 1 Calculate cross sectional area
            double cross_section_area = 0.0;
            cross_section_area = geom_obj.geometry_cs_area;

            richTextBox_result.Text = richTextBox_result.Text + "Cross sectional area = " + cross_section_area.ToString("F4") + Environment.NewLine + Environment.NewLine;

            // Step 2 Calculate centroid
            double centroid_x = 0.0;
            double centroid_y = 0.0;

            centroid_x = geom_obj.geometry_x_center;
            centroid_y = geom_obj.geometry_y_center;

            richTextBox_result.Text = richTextBox_result.Text + "Geometric center (minimum x = " + geom_obj.geometry_xmin.ToString("F4") + ", minimum y = " + geom_obj.geometry_ymin.ToString("F4") + ")" + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "x centroid = " + centroid_x.ToString("F4") + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "y centroid = " + centroid_y.ToString("F4") + Environment.NewLine + Environment.NewLine;

            // Step 3 Calculate the moment of inertia about the centroid
            double moi_x = 0.0;
            double moi_y = 0.0;
            double moi_xy = 0.0;

            moi_x = geom_obj.geometry_x_moi;
            moi_y = geom_obj.geometry_y_moi;
            moi_xy = geom_obj.geometry_xy_moi;

            richTextBox_result.Text = richTextBox_result.Text + "Moments and product of inertia about centroid" + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "Ixx = " + moi_x.ToString("F4") + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "Iyy = " + moi_y.ToString("F4") + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "Ixy = " + moi_xy.ToString("F4") + Environment.NewLine + Environment.NewLine;

            // Step 4 Principal moment of inertia
            double moi_p1 = 0.0;
            double moi_p2 = 0.0;
            double moi_theta = 0.0;

            moi_p1 = geom_obj.geometry_p1_moi;
            moi_p2 = geom_obj.geometry_p2_moi;
            moi_theta = geom_obj.geometry_theta_moi;

            richTextBox_result.Text = richTextBox_result.Text + "Principal moments of inertia and direction" + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "I1 = " + moi_p1.ToString("F4") + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "I2 = " + moi_p2.ToString("F4") + Environment.NewLine;
            richTextBox_result.Text = richTextBox_result.Text + "Theta = " + (moi_theta * (180 / Math.PI)).ToString("F4") + Environment.NewLine + Environment.NewLine;


            geom_obj.set_openTK_objects();
            glControl_main_panel.Invalidate();

        }

        private void toolStripMenuItem_close_ItemClicked(object sender, EventArgs e)
        {
            // Exit application
            this.Close();
        }
        #endregion

        #region "glControl Main Panel Events"
        private void glControl_main_panel_Load(object sender, EventArgs e)
        {
            // Load the wrapper class to control the openTK glcontrol
            g_control = new opentk_main_control();

            // Update the size of the drawing area
            g_control.update_drawing_area_size(glControl_main_panel.Width,
                glControl_main_panel.Height);

            // Refresh the controller (doesnt do much.. nothing to draw)
            glControl_main_panel.Invalidate();
        }

        private void glControl_main_panel_Paint(object sender, PaintEventArgs e)
        {
            // Paint the drawing area (glControl_main)
            // Tell OpenGL to use MyGLControl
            glControl_main_panel.MakeCurrent();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(0, BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // Paint the background
            g_control.set_opengl_shader(1);
            GL.LineWidth(1.0f);
            g_control.paint_opengl_control_background();

            // Display the model using OpenGL
            g_control.set_opengl_shader(2);
            GL.LineWidth(3.0f);
            geom_obj.paint_geometry();

            // Display the text
            // g_control.set_opengl_shader(3);
            //Matrix4 projectionM = Matrix4.CreateScale(new Vector3(1f / glControl_main_panel.Width,
            //    1f / glControl_main_panel.Height, 1.0f));
            // Matrix4 projectionM = Matrix4.CreateOrthographicOffCenter(0.0f, glControl_main_panel.Width, glControl_main_panel.Height, 0.0f, -1.0f, 1.0f);

            //float a1 = 1f / glControl_main_panel.Width;
            //float a2 = 1f / glControl_main_panel.Height;

            //Matrix4 projectionM = new Matrix4((2*a1), 0.0f, 0.0f, 0.0f,
            //    0.0f, (-2*a2) , 0.0f, 0.0f,
            //    0.0f, 0.0f, -1.0f , 0.0f,
            //    0.0f, 0.0f, 0.0f, 1.0f );

            // GL.UniformMatrix4(1, false, ref projectionM);
            // GL.Uniform3(2, new Vector3(0.5f, 0.8f, 0.2f));

            // geom_obj.paint_text();

            // OpenTK windows are what's known as "double-buffered". In essence, the window manages two buffers.
            // One is rendered to while the other is currently displayed by the window.
            // This avoids screen tearing, a visual artifact that can happen if the buffer is modified while being displayed.
            // After drawing, call this function to swap the buffers. If you don't, it won't display what you've rendered.
            glControl_main_panel.SwapBuffers();
        }

        private void glControl_main_panel_SizeChanged(object sender, EventArgs e)
        {
            // glControl size changed
            // Update the size of the drawing area
            g_control.update_drawing_area_size(glControl_main_panel.Width,
                glControl_main_panel.Height);

            // Refresh the painting area
            glControl_main_panel.Invalidate();
        }

        private void glControl_main_panel_MouseEnter(object sender, EventArgs e)
        {
            // set the focus to enable zoom/ pan & zoom to fit
            glControl_main_panel.Focus();
        }

        private void glControl_main_panel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Pan operation starts if Ctrl + Mouse Right Button is pressed
            if (gvariables_static.Is_cntrldown == true && e.Button == MouseButtons.Right)
            {
                // save the current cursor point
                click_pt = new PointF(e.X, e.Y);
                // Set the variable to indicate pan operation begins
                gvariables_static.Is_panflg = true;
            }
        }

        private void glControl_main_panel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // User press hold Cntrl key and mouse wheel
            if (gvariables_static.Is_cntrldown == true)
            {
                // Zoom operation commences
                glControl_main_panel.Focus();

                g_control.intelli_zoom_operation(e.Delta, e.X, e.Y);

                // Update the zoom value in tool strip status bar
                toolStripStatusLabel_zoom_value.Text = "Zoom: " + (gvariables_static.RoundOff((int)(100f * g_control._zoom_val))).ToString() + "%";
                // Refresh the painting area
                glControl_main_panel.Refresh();
            }
        }

        private void glControl_main_panel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //PointF temp = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

            //toolStripStatusLabel_sidepanel_coord.Text = temp.X.ToString() + ", " + temp.Y.ToString();

            if (gvariables_static.Is_panflg == true)
            {
                g_control.pan_operation(e.X - click_pt.X, e.Y - click_pt.Y);

                // Refresh the painting area
                glControl_main_panel.Refresh();
            }
        }

        private void glControl_main_panel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Pan operation ends once the Mouse Right Button is released
            if (gvariables_static.Is_panflg == true)
            {
                gvariables_static.Is_panflg = false;

                // Pan operation ends (save the translate transformation)
                g_control.pan_operation_complete();

                // Refresh the painting area
                glControl_main_panel.Invalidate();
            }
        }

        #region "GLControl Keyboard events"
        private void glControl_main_panel_KeyDown(object sender, KeyEventArgs e)
        {
            // Keydown event
            if (e.Control == true)
            {
                // User press and hold Cntrl key
                gvariables_static.Is_cntrldown = true;

                if (e.KeyCode == Keys.F)
                {
                    // (Ctrl + F) --> Zoom to fit
                    g_control.zoom_to_fit(ref glControl_main_panel);

                    toolStripStatusLabel_zoom_value.Text = "Zoom: " + (gvariables_static.RoundOff((int)(1.0f * 100))).ToString() + "%";
                    toolStripStatusLabel_zoom_value.Invalidate();
                }
            }
        }

        private void glControl_main_panel_KeyUp(object sender, KeyEventArgs e)
        {
            // Keyup event
            gvariables_static.Is_cntrldown = false;
        }

        #endregion
        #endregion
    }
}
