using System;
using System.Collections.Generic;

namespace omission.api.Models.ViewModels
{
    public class CodeListVM {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProgrammingLanguage { get; set; }

        public string Code { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<string> HashTags { get; set; }
    }
}