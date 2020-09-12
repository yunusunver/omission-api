using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    
    public class Hashtag :BaseEntity {

        [Column("name")]
        public string Name { get; set; }

        
    }
}