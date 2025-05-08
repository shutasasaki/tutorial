using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssksSimulation
{
    public class DODProfile
    {
        public string Name { get; set; }    // プロファイル名
        public float Lp { get; set; }
        public float Leq { get; set; }
        public float Le { get; set; }
        public float Lmax { get; set; }
        public float Lmin { get; set; }
        public float Add { get; set; }
        public float L5 { get; set; }
        public float L10 { get; set; }
        public float L50 { get; set; }
        public float L90 { get; set; }
        public float SubLp { get; set; }
        public int Over { get; set; }
        public int Under { get; set; }
    }

}
