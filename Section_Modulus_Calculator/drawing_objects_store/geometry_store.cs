using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Section_Modulus_Calculator.drawing_objects_store.drawing_elements;

namespace Section_Modulus_Calculator.drawing_objects_store
{
    public class geometry_store
    {
        public HashSet<surface_store> all_surfaces { get; private set; }

        public double geometry_cs_area { get; private set; }

        public double geometry_x_center { get; private set; }

        public double geometry_y_center { get; private set; }

        public double geometry_x_moi { get; private set; }

        public double geometry_y_moi { get; private set; }

        public double geometry_xy_moi { get; private set; }

        public HashSet<ellipse_store> all_endpoints { get; private set; }

        ellipse_store geom_centroid;

        public bool is_geometry_set { get; private set; }

        private bool is_geometry_calculated = false;

        public geometry_store()
        {
            // Empty constructor
            this.is_geometry_set = false;
            this.is_geometry_calculated = false;
        }


        public void add_geometry(HashSet<surface_store> t_all_surfaces, HashSet<ellipse_store> t_all_ellipses)
        {
            // Add all the surfaces
            this.all_surfaces = new HashSet<surface_store>(t_all_surfaces);
            this.all_endpoints = new HashSet<ellipse_store>(t_all_ellipses);
            this.is_geometry_calculated = false;

            // set the initial geometry properties
            this.geometry_cs_area = 0.0;

            // Centroid
            this.geometry_x_center = 0.0;
            this.geometry_y_center = 0.0;

            // moment of inertia
            this.geometry_x_moi = 0.0;
            this.geometry_y_moi = 0.0;
            this.geometry_xy_moi = 0.0;
        }

        public void set_geometric_parameter()
        {
            double surf_area = 0.0;
            //List<double> s_area = new List<double>();
            double s_x = 0.0;
            double s_y = 0.0;

            double sx_moi = 0.0;
            double sy_moi = 0.0;
            double sxy_moi = 0.0;

            this.is_geometry_calculated = false;

            // Return the area of the surface
            if (this.is_geometry_set == true)
            {
                foreach (surface_store surf in this.all_surfaces)
                {
                    // Add to the surface area list
                    double s_area = surf.surf_area;

                    // centroid geometric decomoposition
                    s_x = s_x + (surf.x_centroid * s_area);
                    s_y = s_y + (surf.y_centroid * s_area);

                    surf_area = surf_area + s_area;

                    // moment of inertia of geometry
                    sx_moi = sx_moi + surf.x_area_moment;
                    sy_moi = sy_moi + surf.y_area_moment;
                    sxy_moi = sxy_moi + surf.xy_area_moment;
                }

                // set the geometry center if geometry is available (loaded)
                this.geometry_cs_area = surf_area;

                // centroid
                this.geometry_x_center = (s_x / surf_area);
                this.geometry_y_center = (s_y / surf_area);

                // moment of inertia
                this.geometry_x_moi = sx_moi;
                this.geometry_y_moi = sy_moi;
                this.geometry_xy_moi = sxy_moi;


                geom_centroid = new ellipse_store(1000, this.geometry_x_center, this.geometry_y_center, System.Drawing.Color.Violet, global_variables.gvariables_static.ellipse_size_control * 0.01);
                this.is_geometry_calculated = true;

            }
        }

        public void set_openTK_objects()
        {
            // Set the surfaces openTK 
            foreach (surface_store surf in this.all_surfaces)
            {
                surf.set_openTK_objects();
            }

            // Set the surface end points openTK
            foreach (ellipse_store ellipse in this.all_endpoints)
            {
                ellipse.set_openTK_objects();
            }

            if (this.is_geometry_calculated == true)
            {
                geom_centroid.set_openTK_objects();
            }

            this.is_geometry_set = true;
        }

        public void paint_geometry()
        {
            if (this.is_geometry_set == true)
            {
                // Paint the surface boundaries
                foreach (surface_store surf in this.all_surfaces)
                {
                    surf.paint_boundaries();
                }
                // Paint the end points boundaries
                foreach (ellipse_store ellipse in this.all_endpoints)
                {
                    ellipse.paint_ellipse();
                }

                if (this.is_geometry_calculated == true)
                {
                    geom_centroid.paint_ellipse();
                }
            }
        }

        public void paint_text()
        {
            // Set the shader to text shader before calling this
            if (this.is_geometry_set == true)
            {
                // Paint the surface boundaries
                foreach (surface_store surf in this.all_surfaces)
                {
                    surf.paint_boundaries_label();
                }
            }
            if (this.is_geometry_set == true)
            {
                fretypefont_store ffnt = new fretypefont_store(64);

                ffnt.RenderText("This is a sample text", 00.0f, 00.0f, 1.0f, new OpenTK.Vector2(1f, 0f));
            }
        }
    }
}
