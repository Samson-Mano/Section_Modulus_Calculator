using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace Section_Modulus_Calculator
{
    public partial class main_form : Form
    {
        // Variables to control openTK GLControl
        // glControl wrapper class
        private opentk_main_control g_control;

        // Main zoom variables
        private float zm = 1.0f;

        // Cursor point on the GLControl
        private PointF click_pt;

        // Temporary variables to initiate the zoom to fit animation
        private float param_t = 0.0f;
        private float temp_zm = 1.0f;
        private float temp_tx, temp_ty;

        public main_form()
        {
            InitializeComponent();
        }

        #region "Main Form Control"
        private void main_form_Load(object sender, EventArgs e)
        {
            form_size_control();
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
            button_referenceaxis.Location = new Point((int)((width * 0.6) + (w_x * 0.5) - 75), 80);
            button_calculate.Location = new Point((int)((width * 0.6) + (w_x * 0.5) - 75), 125);

            // Location of Result of textbox
            richTextBox_result.Location = new Point((int)(width * 0.6), 180);
            richTextBox_result.Size = new Size((int)(width * 0.4) - 30, height - 180 - 75);
        }
        #endregion

        #region "Button Click Events"
        private void button_import_Click(object sender, EventArgs e)
        {
            // Import Geometry
            MessageBox.Show("Import");

        }

        private void button_referenceaxis_Click(object sender, EventArgs e)
        {
            // Modify Reference Axis
            MessageBox.Show("Reference");

        }

        private void button_calculate_Click(object sender, EventArgs e)
        {
            // Calculate Section Modulus
            MessageBox.Show("Calculate");

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

            // Paint the background
            g_control.paint_opengl_control_background();

            // Display the model using OpenGL
            // bz_heatmap.paint_heatmap();

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

                // Get the screen pt before scaling
                PointF screen_pt_b4_scale = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

                if (e.Delta > 0)
                {
                    if (zm < 1000)
                    {
                        zm = zm + 0.1f;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (zm > 0.101)
                    {
                        zm = zm - 0.1f;
                    }
                }

                // Get the screen pt after scaling
                PointF screen_pt_a4_scale = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

                float tx = (-1.0f) * zm * 0.5f * (screen_pt_b4_scale.X - screen_pt_a4_scale.X);
                float ty = (-1.0f) * zm * 0.5f * (screen_pt_b4_scale.Y - screen_pt_a4_scale.Y);

                // Scale the view with intellizoom (translate and scale)
                g_control.scale_intelli_zoom_Transform(zm, tx, ty);

                // Update the zoom value in tool strip status bar
                toolStripStatusLabel_zoom_value.Text = "Zoom: " + (gvariables_static.RoundOff((int)(zm * 100))).ToString() + "%";
                // Refresh the painting area
                glControl_main_panel.Refresh();
            }
        }

        private void glControl_main_panel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PointF temp = g_control.drawing_area_details.get_normalized_screen_pt(e.X, e.Y, zm, g_control.previous_translation.X, g_control.previous_translation.Y);

            toolStripStatusLabel_sidepanel_coord.Text = temp.X.ToString() + ", " + temp.Y.ToString();

            if (gvariables_static.Is_panflg == true)
            {
                // Pan operation is in progress
                float tx = (float)((e.X - click_pt.X) / (g_control.drawing_area_details.max_drawing_area_size * 0.5f));
                float ty = (float)((e.Y - click_pt.Y) / (g_control.drawing_area_details.max_drawing_area_size * 0.5f));

                // Translate the drawing area
                g_control.translate_Transform(tx, -1.0f * ty);

                // Refresh the painting area
                glControl_main_panel.Invalidate();
            }
        }

        private void glControl_main_panel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Pan operation ends once the Mouse Right Button is released
            if (gvariables_static.Is_panflg == true)
            {
                gvariables_static.Is_panflg = false;

                // Pan operation ends (save the translate transformation)
                g_control.save_translate_transform();

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
                    // Set view to Fit to View (Default view) 
                    param_t = 0.0f;
                    timer1.Interval = 10;

                    // Save the current zoom and translation values to temporary variables (for the animation)
                    temp_zm = zm;
                    temp_tx = g_control.previous_translation.X;
                    temp_ty = g_control.previous_translation.Y;

                    // start the scale to fit animation
                    timer1.Start();
                }
            }
        }

        private void glControl_main_panel_KeyUp(object sender, KeyEventArgs e)
        {
            // Keyup event
            gvariables_static.Is_cntrldown = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Scale to Fit animation
            param_t = param_t + 0.05f;

            if (param_t > 1.0f)
            {
                // Set the zoom value to 1.0f
                zm = 1.0f;
                // Scale transformation (Scale to fit)
                g_control.scale_Transform(1.0f);

                param_t = 0.0f;
                // End the animation
                timer1.Stop();

                // Translate transformation (Translate back to the original)
                g_control.translate_Transform(-temp_tx, -temp_ty);
                g_control.save_translate_transform();

                // Refresh the painting area
                glControl_main_panel.Invalidate();
                toolStripStatusLabel_zoom_value.Text = "Zoom: " + (gvariables_static.RoundOff((int)(zm * 100))).ToString() + "%";
                return;
            }
            else
            {
                // Animate the translation & zoom value
                float anim_zm = temp_zm * (1 - param_t) + (1.0f * param_t);
                float anim_tx = (0.0f * (1 - param_t) - (temp_tx * param_t));
                float anim_ty = (0.0f * (1 - param_t) - (temp_ty * param_t));

                // Scale transformation intermediate
                g_control.scale_Transform(anim_zm);

                // Translate transformation intermediate
                g_control.translate_Transform(anim_tx, anim_ty);

                // Refresh the painting area
                glControl_main_panel.Invalidate();
            }
        }

        #endregion
        #endregion
    }
}
