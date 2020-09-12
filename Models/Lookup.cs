using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    public class Lookup:BaseEntity {
        
        [Column("name")]
        public string Name { get; set; }

        [Column("type")]

        public string Type { get; set; } 

        [Column("parentid")]
        public int? ParentId { get;set;} 

        [Column("orderid")]
        public int OrderId {get;set;}

    }
}