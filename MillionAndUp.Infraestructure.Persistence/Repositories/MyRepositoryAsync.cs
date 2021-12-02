using Ardalis.Specification.EntityFrameworkCore;
using MillionAndUp.Core.Application.Interfaces;
using MillionAndUp.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Infraestructure.Persistence.Repositories
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T:class
    {
        private readonly ApplicationDBContext _dbContext;

        public MyRepositoryAsync(ApplicationDBContext dbContext):base (dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
