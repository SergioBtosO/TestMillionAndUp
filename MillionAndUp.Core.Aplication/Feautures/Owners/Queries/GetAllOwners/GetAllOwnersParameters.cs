using MillionAndUp.Core.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersParameters : RequestParameter
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime MinBirthday { get; set; }
        public DateTime MaxBirthday { get; set; }
    }
}
