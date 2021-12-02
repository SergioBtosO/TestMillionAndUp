using MillionAndUp.Core.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesParameters : RequestParameter
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CodeInternal { get; set; }
        public int IdOwner { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }        
        public int MaxYear { get; set; }
        public int MinYear { get; set; }
    }
}
