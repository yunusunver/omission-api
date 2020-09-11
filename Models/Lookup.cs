namespace omission.api.Models
{
    public class Lookup:BaseEntity {
        
        public string Name { get; set; }

        public string Type { get; set; } 

        public int? ParentId { get;set;} 

        public int OrderId {get;set;}

    }
}