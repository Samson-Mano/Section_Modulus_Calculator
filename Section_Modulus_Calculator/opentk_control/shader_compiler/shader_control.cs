using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_Modulus_Calculator.opentk_control.shader_compiler
{
  public  class shader_control
    {
        public enum shader_type
        {
            br_shader,
            dr_shader,
            txt_shader
        }

        #region "Vertex Shaders"
        public string br_vert_shader()
        {
            return "#version 330 core\r\n" +
            "\r\n" +
            "layout(location = 0) in vec3 aPosition;\r\n" +
            "layout(location = 1) in vec4 vertexColor;\r\n" +
            "\r\n" +
            "uniform float gScale = 1.0f;\r\n" +
            "\r\n" +
            "uniform mat4 gTranslation = mat4(1.0, 0.0, 0.0, 0.0,  // 1. column\r\n" +
                                             "0.0, 1.0, 0.0, 0.0,  // 2. column\r\n" +
                                             "0.0, 0.0, 1.0, 0.0,  // 3. column\r\n" +
                                             "0.0, 0.0, 0.0, 1.0); // 4. column\r\n" +
            "\r\n" +
            "\r\n" +
            "out vec4 v_Color;\r\n" +
            "\r\n" +
            "void main()\r\n" +
            "{\r\n" +
                "v_Color = vertexColor;\r\n" +
                 "\r\n" +
                "gl_Position = gTranslation * vec4(gScale * aPosition, 1.0);\r\n" +
            "}";
        }

        public string txt_vert_shader()
        {
            
            return "#version 460\r\n" +
            "\r\n" +
            "layout(location = 0) in vec2 in_pos;\r\n" +
            "layout(location = 1) in vec2 in_uv;\r\n" +
            "\r\n" +
            "out vec2 vUV;\r\n" +
            "\r\n" +
            "layout (location = 0) uniform mat4 model;\r\n" +
            "layout (location = 1) uniform mat4 projection;\r\n" +
            "\r\n" +
            "uniform float gScale = 1.0f;\r\n" +
            "\r\n" +
            "uniform mat4 gTranslation = mat4(1.0, 0.0, 0.0, 0.0,  // 1. column\r\n" +
                                             "0.0, 1.0, 0.0, 0.0,  // 2. column\r\n" +
                                             "0.0, 0.0, 1.0, 0.0,  // 3. column\r\n" +
                                             "0.0, 0.0, 0.0, 1.0); // 4. column\r\n" +
            "\r\n" +
            "void main()\r\n" +
            "{\r\n" +
                "vUV         = in_uv.xy;\r\n" +
                 "\r\n" +
                "gl_Position = gTranslation * projection * model  * vec4(gScale * in_pos.xy, 0.0, 1.0);\r\n" +
            "}";

           
        /*
              return "#version 460\r\n" +
            "\r\n" +
            "layout(location = 0) in vec2 in_pos;\r\n" +
            "layout(location = 1) in vec2 in_uv;\r\n" +
            "\r\n" +
            "out vec2 vUV;\r\n" +
            "\r\n" +
            "layout (location = 0) uniform mat4 model;\r\n" +
            "layout (location = 1) uniform mat4 projection;\r\n" +
            "\r\n" +
            "void main()\r\n" +
            "{\r\n" +
                "vUV         = in_uv.xy;\r\n" +
                 "\r\n" +
                "gl_Position = projection * model * vec4(in_pos.xy, 0.0, 1.0);\r\n" +
            "}";

           */

            /*
            @"#version 460
            layout (location = 0) in vec2 in_pos;
            layout (location = 1) in vec2 in_uv;
            out vec2 vUV;
            layout (location = 0) uniform mat4 model;
            layout (location = 1) uniform mat4 projection;
            
            void main()
            {
                vUV         = in_uv.xy;
		        gl_Position = projection * model * vec4(in_pos.xy, 0.0, 1.0);
            }";

            */
        }
        #endregion

        #region "Fragment shaders"
        public string br_frag_shader()
        {
            return "#version 330 core\r\n" +
                "\r\n" +
                "in vec4 v_Color;\r\n" +
                "out vec4 f_Color; // fragment's final color (out to the fragment shader)\r\n" +
                "\r\n" +
                "void main()\r\n" +
                "{\r\n" +
                "f_Color = v_Color;\r\n" +
                "}";
        }

        public string txt_frag_shader()
        {
            return "#version 460\r\n" +
                "\r\n" +
                "in vec2 vUV;\r\n" +
                "\r\n" +
                "layout (binding = 0) uniform sampler2D u_texture;\r\n" +
                "layout (location = 2) uniform vec3 textColor;\r\n" +
                "out vec4 f_Color; // fragment's final color (out to the fragment shader)\r\n" +
                "\r\n" +
                "void main()\r\n" +
                "{\r\n" +
                "vec2 uv = vUV.xy;\r\n" +
                "float text = texture(u_texture, uv).r;\r\n" +
                "f_Color = vec4(textColor.rgb*text, text);\r\n" +
                "}";

            /*
             @"#version 460
            in vec2 vUV;
            layout (binding=0) uniform sampler2D u_texture;
             layout (location = 2) uniform vec3 textColor;
            out vec4 fragColor;
            void main()
            {
                vec2 uv = vUV.xy;
                float text = texture(u_texture, uv).r;
                fragColor = vec4(textColor.rgb*text, text);
            }";

             */
        }
        #endregion

        public shader_control()
        {
            // Empty cconstructor
        }

        public string get_vertex_shader(shader_type stype)
        {
            // Returns the vertex shader
            string vert_out = "";
            if (stype == shader_type.br_shader)
            {
                vert_out = br_vert_shader();
            }
            else if(stype == shader_type.txt_shader)
            {
                vert_out = txt_vert_shader();
            }
            return vert_out;
        }

        public string get_fragment_shader(shader_type stype)
        {
            // Returns the fragment shader
            string frag_out = "";
            if (stype == shader_type.br_shader)
            {
                frag_out = br_frag_shader();
            }
            else if(stype == shader_type.txt_shader)
            {
                frag_out = txt_frag_shader();
            }
            return frag_out;
        }
    }
}
