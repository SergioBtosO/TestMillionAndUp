using MillionAndUp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Domain.Entities
{
    public class PropertyTrace : AuditableBaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public int IdProperty { get; set; }
        public Property Property { get; set; }
    }
}
