using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.DTOs
{
    public class PropertyTraceDTO
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public int IdProperty { get; set; }
    }
}
