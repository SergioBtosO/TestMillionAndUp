using Ardalis.Specification;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Specifications
{
    class PagedPropertyTraceSpecification : Specification<PropertyTrace>
    {
        public PagedPropertyTraceSpecification(int pageNumber, int pageSize, string name, int idProperty,DateTime minDateSale,DateTime maxDateSale,double minValue, double maxValue,double minTax,double maxTax)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderBy(o => o.Id);

            if (!string.IsNullOrEmpty(name))
                Query.Where(o => o.Name.Contains( name ));


            if (0 > idProperty)
                Query.Where(o => o.IdProperty == idProperty);

            if (minDateSale.GetHashCode() != 0)
            {
                Query.Where(o => o.DateSale >= minDateSale );
            }

            if (maxDateSale.GetHashCode() != 0)
            {
                Query.Where(o => o.DateSale <= maxDateSale);
            }

            if (minValue.GetHashCode() != 0)
            {
                Query.Where(o => o.Value >= minValue);
            }

            if (maxValue.GetHashCode() != 0)
            {
                Query.Where(o => o.Value <= maxValue);
            }

            if (minTax.GetHashCode() != 0)
            {
                Query.Where(o => o.Tax >= minTax);
            }

            if (maxTax.GetHashCode() != 0)
            {
                Query.Where(o => o.Tax <= maxTax);
            }

        }
    }
}
