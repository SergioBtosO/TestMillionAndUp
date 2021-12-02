using MillionAndUp.Core.Domain.Common;

namespace MillionAndUp.Core.Domain.Entities
{
    public class PropertyImage : AuditableBaseEntity
    {
        public bool Enabled { get; set; }
        public byte[] File { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdProperty { get; set; }
        public Property Property { get; set; }

    }
}
