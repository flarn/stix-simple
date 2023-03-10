using System.ComponentModel.DataAnnotations.Schema;

namespace Stix.Core.Entities
{
    public class EntityBase
    {
        [Column("id")]
        public string Id { get; set; }
    }
}