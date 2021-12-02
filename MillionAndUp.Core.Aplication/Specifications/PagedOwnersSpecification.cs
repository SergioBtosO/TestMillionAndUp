using Ardalis.Specification;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Specifications
{
    public class PagedOwnersSpecification : Specification<Owner>
    {
        public PagedOwnersSpecification(int pageNumber, int pageSize, string name, string address, DateTime minBirthday, DateTime maxBirthday)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderBy(o => o.Id);

            if (!string.IsNullOrEmpty(name))
                Query.Where(o => o.Name.ToUpper().Contains( name.ToUpper()));

            if (!string.IsNullOrEmpty(address))
                Query.Where(o => o.Address.ToUpper().Contains(address.ToUpper()));

            if(minBirthday.GetHashCode() != 0)
            {
                Query.Where(o => o.Birthday > minBirthday);
            }

            if (maxBirthday.GetHashCode() != 0)
            {
                Query.Where(o => o.Birthday < maxBirthday);
            }

        }
    }
}
