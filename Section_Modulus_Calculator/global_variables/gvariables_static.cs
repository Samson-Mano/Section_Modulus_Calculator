using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Section_Modulus_Calculator.global_variables
{
    public static class gvariables_static
    {
        public static Color glcontrol_background_color = Color.White;

        // Garphics Control variables
        public static bool Is_panflg = false;
        public static bool Is_cntrldown = false;

        public static int RoundOff(this int i)
        {
            // Roundoff to nearest 10 (used to display zoom value)
            return ((int)Math.Round(i / 10.0)) * 10;
        }
    }
}
