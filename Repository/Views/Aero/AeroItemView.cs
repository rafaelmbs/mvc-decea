using System.Collections.Generic;

namespace mvc_decea.Repository.Views.Aero
{
    public class AeroItemView
    {
        public IList<string> AeroCode { get; set; }
        public IList<string> name { get; set; }
        public IList<string> city { get; set; }
        public IList<string> uf { get; set; }
        public IList<string> lat { get; set; }
        public IList<string> lng { get; set; }
        public IList<string> fir { get; set; }
        public IList<AeroRotaerView> rotaer { get; set; }
    }
}