using System.Collections.Generic;

namespace mvc_decea.Repository.Views.Charts
{
    public class ChartsItemView
    {
        public IList<string> especie { get; set; }
        public IList<string> tipo { get; set; }
        public IList<string> tipo_descr { get; set; }
        public IList<string> nome { get; set; }
        public IList<string> IcaoCode { get; set; }
        public IList<string> dt { get; set; }
        public IList<string> link { get; set; }
        public IList<string> tabcode { get; set; }
    }
}