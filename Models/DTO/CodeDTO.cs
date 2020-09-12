namespace omission.api.Models.DTO
{
    public class CodeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int LookupId { get; set; }

        public string Code { get; set; }

        public int[] Hashtags { get; set; }
    }
}