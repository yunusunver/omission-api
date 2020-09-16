namespace omission.api.Models.DTO
{
    public class LookupUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; } = 1;

    }
}