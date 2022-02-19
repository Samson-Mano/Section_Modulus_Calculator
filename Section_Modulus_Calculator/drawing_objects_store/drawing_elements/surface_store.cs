using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_Modulus_Calculator.drawing_objects_store.drawing_elements
{
    public class surface_store
    {
        public int surf_id { get; private set; }

        public closed_boundary_store closed_outer_bndry { get; private set; }

        public HashSet<closed_boundary_store> closed_inner_bndries { get; private set; }

        public surface_store(int t_surf_id, closed_boundary_store t_closed_outer_bndry, HashSet<closed_boundary_store> t_closed_inner_bndries)
        {
            // Main constructor
            this.surf_id = t_surf_id;

            // Closed outter boundary
            this.closed_outer_bndry = t_closed_outer_bndry;

            // Closed inner boundaries
            this.closed_inner_bndries = new HashSet<closed_boundary_store>(t_closed_inner_bndries);
        }

        public void set_openTK_objects()
        {
            // Set the closed boundaries openTK 
            this.closed_outer_bndry.set_openTK_objects();

            foreach(closed_boundary_store inner_bndry in this.closed_inner_bndries)
            {
                inner_bndry.set_openTK_objects();
            }
        }

        public void paint_boundaries()
        {
            // Paint the boundaries (outer and inner)
            // Set openTK becore calling this function
            this.closed_outer_bndry.paint_closed_boundary();

            foreach (closed_boundary_store inner_bndry in this.closed_inner_bndries)
            {
                inner_bndry.paint_closed_boundary();
            }
        }


    }
}
