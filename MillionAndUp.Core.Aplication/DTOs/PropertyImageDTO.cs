using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.DTOs
{
    public class PropertyImageDTO
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public byte[] File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdProperty { get; set; }
    }
}
