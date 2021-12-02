using MillionAndUp.Core.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetAllPropertyImages
{
    public class GetAllPropertyImagesParameters : RequestParameter
    {
        public string Tittle { get; set; }
        public string Description { get; set; }
        public int IdProperty { get; set; }
        public bool Enabled { get; set; }
    }
}
