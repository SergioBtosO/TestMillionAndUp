
using MillionAndUp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Domain.Entities
{
    public class Owner: AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Birthday { get; set; }
        public List<Property> Properties { get; set; }
    }
}
