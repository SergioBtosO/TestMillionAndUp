using Ardalis.Specification;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Specifications
{
    class PagedPropertiesSpecification : Specification<Property>
    {
        public PagedPropertiesSpecification(int pageNumber, int pageSize, string name, string address,string codeInternal,int idOwner,double minPrice,double maxPrice, int maxYear, int minYear = 1800)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderBy(o => o.Id);

            if (!string.IsNullOrEmpty(name))
                Query.Where(o => o.Name.ToUpper().Contains(name.ToUpper()));

            if (!string.IsNullOrEmpty(address))
                Query.Where(o => o.Address.ToUpper().Contains(address.ToUpper()));

            if (!string.IsNullOrEmpty(codeInternal))
                Query.Where(o => o.CodeInternal.ToUpper().Contains(codeInternal.ToUpper()));

            if (maxPrice.GetHashCode() != 0)
                Query.Where(o => o.Price <= maxPrice);

            if (minPrice.GetHashCode() != 0)
                Query.Where(o => o.Price >= minPrice);

            if (maxYear.GetHashCode() != 0)
                Query.Where(o => o.Year <= maxYear);

            if ( minYear.GetHashCode() != 0)
                Query.Where(o => o.Year >= minYear);

            if (0 > idOwner)
                Query.Where(o => o.IdOwner == idOwner);

        }
    }
}
