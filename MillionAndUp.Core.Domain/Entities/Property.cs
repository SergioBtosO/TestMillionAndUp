using MillionAndUp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Domain.Entities
{
    public class Property : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public Owner Owner { get; set; }
        public List<PropertyImage> Images {get;set;}
        public List<PropertyTrace> Traces { get; set; }
    }
}
