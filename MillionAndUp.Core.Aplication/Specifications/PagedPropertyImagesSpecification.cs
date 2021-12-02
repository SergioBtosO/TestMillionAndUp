using Ardalis.Specification;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Specifications
{
    class PagedPropertyImagesSpecification : Specification<PropertyImage>
    {
        public PagedPropertyImagesSpecification(int pageNumber, int pageSize, string tittle, string description, int idProperty, bool enabled)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).OrderBy(o => o.Id);

            if (!string.IsNullOrEmpty(tittle))
                Query.Where(o => o.Title.ToUpper().Contains(tittle.ToUpper()));

            if (!string.IsNullOrEmpty(description))
                Query.Where(o => o.Description.ToUpper().Contains( description.ToUpper()));

            if (enabled.GetHashCode()!=0)
                Query.Where(o => o.Enabled == enabled);

            if (0 > idProperty)
                Query.Where(o => o.IdProperty == idProperty);

        }
    }
}
