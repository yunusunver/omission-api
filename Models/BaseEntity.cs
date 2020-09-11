using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    public class BaseEntity  {

        [Column("id")]
        public int Id { get; set; }

        [Column("createdby")]
        public int CreatedBy { get; set; }

        [Column("updatedby")]
        public int UpdatedBy { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]

        public DateTime? UpdatedDate { get; set; }
        [Column("isdeleted")]
        public bool isDeleted { get; set; }
    }
}