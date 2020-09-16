namespace omission.api.Models.DTO
{
    public class LookupCreateDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; } = 1;
    }
}