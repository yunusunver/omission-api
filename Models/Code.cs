using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    public class Code : BaseEntity
    {

        [Column("createdby")]
        public int CreatedBy { get; set; }
        [Column("updatedby")]

        public int UpdatedBy { get; set; }
        [Column("lookupid")]

        public int LookupId { get; set; }
        [Column("title")]

        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }
        [Column("body")]

        public string Body { get; set; }

        [Column("hashtags")]

        public int[] Hashtags { get; set; }

    }
}