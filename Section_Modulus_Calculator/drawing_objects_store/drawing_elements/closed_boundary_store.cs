using System;
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

namespace Section_Modulus_Calculator.drawing_objects_store.drawing_elements
{
    public class closed_boundary_store
    {
        public int closed_bndry_id { get; private set; }
        public HashSet<curve_store> boundary_curves { get; private set; }

        public closed_boundary_store(int t_closed_bndry_id, HashSet<curve_store> t_boundary_curves)
        {
            // Main constructor
            this.closed_bndry_id = t_closed_bndry_id;
            this.boundary_curves = new HashSet<curve_store>(t_boundary_curves);
        }

        public void set_openTK_objects()
        {
            // Set the curves associated with boundaries openTK 
            foreach ( curve_store bndry_curve in this.boundary_curves)
            {
                bndry_curve.set_openTK_objects();
            }
        }

        public void paint_closed_boundary()
        {
            // Paint the curves
            // Set openTK becore calling this function
            foreach (curve_store bndry_curve in this.boundary_curves)
            {
                bndry_curve.paint_curve();
            }
        }


    }
}
