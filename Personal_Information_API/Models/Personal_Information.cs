using Personal_Information_API.Models.Value_Object;
using System.Text.Json;

namespace Personal_Information_API.Models
{
    public class Personal_Information : Personal
    {
        public int Id { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
        
    }
}
