using System;

namespace omission.api.Models.ViewModels
{
    public class CodeListVM {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProgrammingLanguage { get; set; }

        public string Code { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}