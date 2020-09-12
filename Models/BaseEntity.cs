using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace omission.api.Models
{
    public class BaseEntity  {

        [Column("id")]
        public int Id { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Column("updateddate")]

        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        [Column("isdeleted")]
        public bool isDeleted { get; set; } = false;


    }
}