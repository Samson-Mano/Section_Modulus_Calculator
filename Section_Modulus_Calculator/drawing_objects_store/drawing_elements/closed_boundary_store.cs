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
        public HashSet<point_store> closed_bndry_pts { get; private set; }

        public double bndry_area { get; private set; }

        public double centroid_x { get; private set; }

        public double centroid_y { get; private set; }

        public double moi_x { get; private set; }
        public double moi_y { get; private set; }
        public double moi_xy { get; private set; }

        public closed_boundary_store(int t_closed_bndry_id, HashSet<curve_store> t_boundary_curves)
        {
            // Main constructor
            this.closed_bndry_id = t_closed_bndry_id;
            this.boundary_curves = new HashSet<curve_store>(t_boundary_curves);

            // Add to closed boundary points (each curve is discretized to 100 pts)
            this.closed_bndry_pts = new HashSet<point_store>();
            foreach (curve_store curves in this.boundary_curves)
            {
                foreach (point_store pt in curves.curve_t_pts.all_pts)
                {
                    this.closed_bndry_pts.Add(pt);
                }
            }

        }

        public void set_openTK_objects()
        {
            // Set the curves associated with boundaries openTK 
            foreach (curve_store bndry_curve in this.boundary_curves)
            {
                bndry_curve.set_openTK_objects();
            }
        }

        public void set_geometric_properties()
        {
            //https://leancrew.com/all-this/2018/01/greens-theorem-and-section-properties/
            // Set the cross- section area

            double c_area = 0.0;
            int n = closed_bndry_pts.Count;

            for (int i = 0; i < n - 1; i++)
            {
                c_area = c_area + (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y -
                                   closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y);
            }

            this.bndry_area = (c_area + closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y -
                                     closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y) / 2.0;

            if (this.bndry_area < 0.0)
            {
                // Reverse the points if the area is negative
                this.bndry_area = -1 * this.bndry_area;
                closed_bndry_pts.Reverse();
            }

            // Set the x & y centroid
            double x_center = 0.0, y_center = 0.0;

            for (int i = 0; i < n - 1; i++)
            {
                x_center = x_center + ((closed_bndry_pts.ElementAt(i).d_x + closed_bndry_pts.ElementAt(i + 1).d_x) *
                    (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y - closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y));

                y_center = y_center + ((closed_bndry_pts.ElementAt(i).d_y + closed_bndry_pts.ElementAt(i + 1).d_y) *
                    (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y - closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y));
            }

            x_center = x_center + ((closed_bndry_pts.ElementAt(n - 1).d_x + closed_bndry_pts.ElementAt(0).d_x) *
                    (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y - closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y));
            y_center = y_center + ((closed_bndry_pts.ElementAt(n - 1).d_y + closed_bndry_pts.ElementAt(0).d_y) *
                    (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y - closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y));


            this.centroid_x = (x_center / (6 * this.bndry_area));
            this.centroid_y = (y_center / (6 * this.bndry_area));


            // Set the Moments and product of inertia about centroid
            double s_xx = 0.0, s_yy = 0.0, s_xy = 0.0;

            for (int i = 0; i < n - 1; i++)
            {
                s_xx = s_xx + (Math.Pow(closed_bndry_pts.ElementAt(i).d_y, 2) + closed_bndry_pts.ElementAt(i).d_y * closed_bndry_pts.ElementAt(i + 1).d_y + Math.Pow(closed_bndry_pts.ElementAt(i + 1).d_y, 2)) *
                    (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y - closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y);
                s_yy = s_yy + (Math.Pow(closed_bndry_pts.ElementAt(i).d_x, 2) + closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_x + Math.Pow(closed_bndry_pts.ElementAt(i + 1).d_x, 2)) *
                    (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y - closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y);
                s_xy = s_xy + (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y + 2 * closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i).d_y + 2 * closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i + 1).d_y + closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y) *
                    (closed_bndry_pts.ElementAt(i).d_x * closed_bndry_pts.ElementAt(i + 1).d_y - closed_bndry_pts.ElementAt(i + 1).d_x * closed_bndry_pts.ElementAt(i).d_y);

            }
            s_xx = s_xx + (Math.Pow(closed_bndry_pts.ElementAt(n - 1).d_y, 2) + closed_bndry_pts.ElementAt(n - 1).d_y * closed_bndry_pts.ElementAt(0).d_y + Math.Pow(closed_bndry_pts.ElementAt(0).d_y, 2)) *
                   (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y - closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y);
            s_yy = s_yy + (Math.Pow(closed_bndry_pts.ElementAt(n - 1).d_x, 2) + closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_x + Math.Pow(closed_bndry_pts.ElementAt(0).d_x, 2)) *
                (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y - closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y);
            s_xy = s_xy + (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y + 2 * closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(n - 1).d_y + 2 * closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(0).d_y + closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y) *
                (closed_bndry_pts.ElementAt(n - 1).d_x * closed_bndry_pts.ElementAt(0).d_y - closed_bndry_pts.ElementAt(0).d_x * closed_bndry_pts.ElementAt(n - 1).d_y);

            this.moi_x = s_xx;
            this.moi_y = s_yy;
            this.moi_xy = s_xy;

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

        public void paint_closed_boundary_label()
        {
            // Paint the curves
            // Set openTK becore calling this function
            foreach (curve_store bndry_curve in this.boundary_curves)
            {
                bndry_curve.paint_curve_label();
            }
        }
    }
}
