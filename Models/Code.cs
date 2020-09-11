namespace omission.api.Models
{
    public class Code {

            public int LookupId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public string Body { get; set; }

            public int[] Hashtags { get; set; }

    }
}