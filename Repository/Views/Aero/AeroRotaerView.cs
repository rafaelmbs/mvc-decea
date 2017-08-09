using System.Collections.Generic;

namespace mvc_decea.Repository.Views.Aero
{
    public class AeroRotaerView
    {
        public IList<string> alt_m { get; set; }
        public IList<string> alt_p { get; set; }
        public IList<string> caract { get; set; }
        public IList<string> pista { get; set; }
        public IList<string> cmb { get; set; }
        public IList<string> rffs { get; set; }
        public IList<string> met { get; set; }
        public IList<string> com { get; set; }
        public IList<string> rdonav { get; set; }
        public IList<string> ais { get; set; }
        public IList<string> rmk { get; set; }
        public IList<string> vfr { get; set; }
    }
}