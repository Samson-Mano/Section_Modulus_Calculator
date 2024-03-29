﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// OpenTK library
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
// This app class structure
using Section_Modulus_Calculator.opentk_control.opentk_buffer;
using Section_Modulus_Calculator.opentk_control.opentk_bgdraw;
using Section_Modulus_Calculator.opentk_control.shader_compiler;
using Section_Modulus_Calculator.global_variables;

namespace Section_Modulus_Calculator.opentk_control
{
    public class opentk_main_control
    {
        // variable stores all the shader information
        private shader_control all_shaders = new shader_control();
        // variable to control the boundary rectangle
        private boundary_rectangle_store boundary_rect = new boundary_rectangle_store(false, null);
        // Shader variable
        // Boundary shader
        private Shader _br_shader;
        // Drawing shader
        private Shader _dr_shader;
        // Text shader
        private Shader _txt_shader;

        // Imported drawing scale
        public float _zoom_val { get; private set; }

        private float _dr_d_scale = 1.0f;

        public opentk_main_control()
        {
            // main constructor
            // Set the Background color 
            Color clr_bg = gvariables_static.glcontrol_background_color;
            GL.ClearColor(((float)clr_bg.R / 255.0f),
                ((float)clr_bg.G / 255.0f),
                ((float)clr_bg.B / 255.0f),
                ((float)clr_bg.A / 255.0f));

            // create the shaders
            this._br_shader = new Shader(all_shaders.get_vertex_shader(shader_control.shader_type.br_shader),
                 all_shaders.get_fragment_shader(shader_control.shader_type.br_shader));

            this._dr_shader = new Shader(all_shaders.get_vertex_shader(shader_control.shader_type.br_shader),
                 all_shaders.get_fragment_shader(shader_control.shader_type.br_shader));

            this._txt_shader = new Shader(all_shaders.get_vertex_shader(shader_control.shader_type.txt_shader),
                 all_shaders.get_fragment_shader(shader_control.shader_type.txt_shader));

            // create the boundary
            boundary_rect = new boundary_rectangle_store(true, this._br_shader);
        }

        public void paint_opengl_control_background()
        {
            // OPen GL works as state machine (select buffer & select the shader)
            // Vertex Buffer (Buffer memory in GPU VRAM)
            // Shader (program which runs on GPU to paint in the screen)
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // paint the boundary border
            boundary_rect.paint_boundary_rectangle();
        }

        public void set_opengl_shader(int s_type)
        {
            // Bind the shader
            if (s_type == 1)
            {
                _br_shader.Use();
            }
            else if (s_type == 2)
            {
                _dr_shader.Use();
            }
            else if(s_type ==3)
            {
                _txt_shader.Use();
            }
        }


        public void update_drawing_area_size(int width, int height)
        {
            // update the drawing area size
            this._br_shader.update_shader.update_primary_scale(this._br_shader, width, height);
            this._dr_shader.update_shader.update_primary_scale(this._dr_shader, width, height);
            // this._txt_shader.update_shader.update_primary_scale(this._txt_shader, width, height);

            // Update the drawing scale and translation
            update_drawing_scale_and_translation(this._dr_d_scale, 0.0f, 0.0f,false);

            // Update the graphics drawing area
            GL.Viewport(this._br_shader.update_shader.drawing_area_details.drawing_area_center_x,
                this._br_shader.update_shader.drawing_area_details.drawing_areas_center_y,
                this._br_shader.update_shader.drawing_area_details.max_drawing_area_size,
                this._br_shader.update_shader.drawing_area_details.max_drawing_area_size);

        }

        public void update_drawing_scale_and_translation(float d_scale, float t_trans_x, float t_trans_y,bool set_origin)
        {
            // Update the drawing size
            this._dr_d_scale = d_scale;
    
             this._dr_shader.update_shader.update_scale_and_orgintranslation(this._dr_shader,d_scale, t_trans_x, t_trans_y, set_origin);
             // this._txt_shader.update_shader.update_scale_and_orgintranslation(this._txt_shader, d_scale, t_trans_x, t_trans_y, set_origin);
        }

        public void intelli_zoom_operation(double e_Delta, int e_X, int e_Y)
        {
            // Intelli zoom all the vertex shaders
            this._br_shader.update_shader.intelli_zoom(this._br_shader, e_Delta, e_X, e_Y);
            this._dr_shader.update_shader.intelli_zoom(this._dr_shader, e_Delta, e_X, e_Y);
            // this._txt_shader.update_shader.intelli_zoom(this._txt_shader, e_Delta, e_X, e_Y);

            this._zoom_val = this._dr_shader.update_shader._zm_scale;
        }

        public void pan_operation(float et_X, float et_Y)
        {
            // Pan the vertex shader
            this._br_shader.update_shader.pan_operation(this._br_shader, et_X, et_Y);
            this._dr_shader.update_shader.pan_operation(this._dr_shader, et_X, et_Y);
            // this._txt_shader.update_shader.pan_operation(this._txt_shader, et_X, et_Y);
        }

        public void pan_operation_complete()
        {
            // End the pan operation saving translate
            this._br_shader.update_shader.save_translate_transform();
            this._dr_shader.update_shader.save_translate_transform();
            // this._txt_shader.update_shader.save_translate_transform();
        }

        public void zoom_to_fit(ref GLControl this_Gcntrl)
        {
            // Zoom to fit the vertex shader
            this._br_shader.update_shader.zoom_to_fit_operation(this._br_shader, ref this_Gcntrl);
            this._dr_shader.update_shader.zoom_to_fit_operation(this._dr_shader, ref this_Gcntrl);
            // this._txt_shader.update_shader.zoom_to_fit_operation(this._txt_shader, ref this_Gcntrl);
        }
    }
}
