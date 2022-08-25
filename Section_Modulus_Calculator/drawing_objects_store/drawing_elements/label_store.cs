using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_Modulus_Calculator.drawing_objects_store.drawing_elements
{
    public class label_store
    {
        public int label_id { get; private set; }

        public uint label_size { get; private set; }

        public point_store label_pt { get; private set; }

        public string label_str { get; private set; }

        private fretypefont_store ffnt;

        public label_store(int t_label_id, uint t_label_size, point_store t_label_pt, string t_label_str)
        {
            // Main constructor
            this.label_id = t_label_id;
            this.label_size = t_label_size;
            this.label_pt = t_label_pt;
            this.label_str = t_label_str;

            ffnt =  new fretypefont_store(t_label_size);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as label_store);
        }

        public void paint_label()
        {
           ffnt.RenderText(this.label_str,  (float)this.label_pt.d_x,  (float)this.label_pt.d_y, 1.0f, new OpenTK.Vector2(1f, 0f));
        }

        public bool Equals(label_store other_label)
        {
            // Check 1 (Line ids should not match)
            if (this.Equals(other_label.label_id) == true)
            {
                return true;
            }

            // Check 2 (Whether line end points match)
            if (this.label_pt.Equals(other_label.label_pt))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.label_id, this.label_pt.pt_id);
        }
    }
}
