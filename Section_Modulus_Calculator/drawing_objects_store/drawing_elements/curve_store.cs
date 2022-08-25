using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Section_Modulus_Calculator.drawing_objects_store.drawing_elements
{
    public class curve_store
    {
        public int curve_id { get; private set; }

        public points_list_store curve_end_pts { get; private set; }

        public points_list_store curve_cntrl_pts { get; private set; }

        public points_list_store curve_t_pts { get; private set; }

        public lines_list_store curve_as_tlines { get; private set; }

        public label_store curve_label { get; private set; }

        public curve_types_enum curve_type { get; private set; }

        public Color curve_color { get; private set; }

        public enum curve_types_enum
        {
            line,
            arc,
            bezier
        }

        public curve_store(int t_cur_id, point_store t_curve_start_pt, point_store t_curve_end_pt, List<point_store> t_curve_cntrl_pts, int t_count, curve_types_enum t_ctype, Color t_curve_clr)
        {
            // Main constructor
            this.curve_id = t_cur_id;
            // Add curve end points
            this.curve_end_pts = new points_list_store();
            // Start pt
            this.curve_end_pts.add_point(t_curve_start_pt);

            // Add curve control points
            this.curve_cntrl_pts = new points_list_store();
            foreach (point_store c_pts in t_curve_cntrl_pts)
            {
                this.curve_cntrl_pts.add_point(c_pts);
            }

            // End pt
            this.curve_end_pts.add_point(t_curve_end_pt);

            this.curve_type = t_ctype;
            this.curve_color = t_curve_clr;

            // Discretize the curve into segments
            // Find all the curve points & lines
            Tuple<lines_list_store, points_list_store> temp = new curve_discretization().get_all_points_at_t(100, t_ctype, this.curve_end_pts, this.curve_cntrl_pts, this.curve_color);

            // Add all the lines and points to the list
            this.curve_as_tlines = temp.Item1;
            this.curve_t_pts = temp.Item2;

            // Create labels for this curve
            double label_pt_x = 0.0, label_pt_y = 0.0;


            if (t_ctype == curve_types_enum.line)
            {
                // line label
                label_pt_x = (t_curve_start_pt.d_x + t_curve_end_pt.d_x) * 0.5;
                label_pt_y = (t_curve_start_pt.d_y + t_curve_end_pt.d_y) * 0.5;
            }
            else if (t_ctype == curve_types_enum.arc)
            {
                // arc label
                label_pt_x = t_curve_cntrl_pts[0].d_x;
                label_pt_y = t_curve_cntrl_pts[0].d_y;
            }
            else
            {
                // bezier label
                label_pt_x = t_curve_cntrl_pts[0].d_x;
                label_pt_y = t_curve_cntrl_pts[0].d_y;
            }

            this.curve_label = new label_store(t_cur_id,32,new point_store(t_cur_id,label_pt_x,label_pt_y, Color.BlueViolet),"h");
            //this.curve_label.add_label(t_cur_id, 32, label_pt_x, label_pt_y, "h", Color.BlueViolet);
        }

        public void set_openTK_objects()
        {
            // Set the discretized lines openTK 
            this.curve_as_tlines.set_openTK_objects();
        }

        public void paint_curve()
        {
            // Paint the curves
            // Set openTK becore calling this function
            this.curve_as_tlines.paint_all_lines();

        }

        public void paint_curve_label()
        {
            // Paint the curve label
            this.curve_label.paint_label();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as curve_store);
        }

        public bool Equals(curve_store other_curve)
        {
            // Check 1 (curve ids should not match)
            if (this.Equals(other_curve.curve_id) == true)
            {
                return true;
            }

            // Check 2 (Whether curve type and curve control points match)
            if (this.curve_type.Equals(other_curve.curve_type))
            {
                // Check the end points
                HashSet<point_store> other_curve_endpts_list = other_curve.curve_end_pts.all_pts;
                if (this.curve_end_pts.all_pts.Except(other_curve_endpts_list).Count() == 0 ||
                    this.curve_end_pts.all_pts.Except(other_curve_endpts_list.Reverse()).Count() == 0)
                {
                    // Not tested Yet !!
                    return true;
                }

                // Check the control points
                HashSet<point_store> other_curve_cntrlpts_list = other_curve.curve_cntrl_pts.all_pts;
                if (this.curve_cntrl_pts.all_pts.Except(other_curve_cntrlpts_list).Count() == 0 ||
                    this.curve_cntrl_pts.all_pts.Except(other_curve_cntrlpts_list.Reverse()).Count() == 0)
                {
                    // Not tested Yet !!
                    return true;
                }

            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.curve_id, this.curve_end_pts.GetHashCode(), this.curve_cntrl_pts.GetHashCode());
        }

    }
}
