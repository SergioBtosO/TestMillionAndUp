using MillionAndUp.Core.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyTraces.Queries.GetAllPropertyTraces
{
    public class GetAllPropertyTracesParameters : RequestParameter
    {
        public string Name { get; set; }
        public int IdProperty { get; set; }
        public DateTime MinDateSale { get; set; }
        public DateTime MaxDateSale { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double MinTax { get; set; }
        public double MaxTax { get; set; }
    }                 
}
