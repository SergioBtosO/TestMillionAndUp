using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Aplication.Interfaces
{
    public interface IDateTimeService
    {
        public DateTime NowUtc { get; } 
    }
}
