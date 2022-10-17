namespace Personal_Information_API.Models
{
    public class Log
    {

        public int Id { get; set; } = 1;
        public string? LogMessage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
