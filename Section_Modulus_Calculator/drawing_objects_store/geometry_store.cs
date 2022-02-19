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
        public HashSet<surface_store> all_surfaces { get; private set;  }
        public bool is_geometry_set { get; private set; }


        public geometry_store()
        {
            // Empty constructor
            this.is_geometry_set = false;
        }


        public void add_surface(HashSet<surface_store> t_all_surfaces)
        {
            // Add all the surfaces
            this.all_surfaces = new HashSet<surface_store>(t_all_surfaces);
            
        }

        public void set_openTK_objects()
        {
            // Set the surfaces openTK 
            foreach (surface_store surf in this.all_surfaces)
            {
                surf.set_openTK_objects();
            }
            this.is_geometry_set = true;
        }

        public void paint_geometry()
        {
            if(this.is_geometry_set == true)
            {
                foreach (surface_store surf in this.all_surfaces)
                {
                    surf.paint_boundaries();
                }
            }
        }
    }
}
