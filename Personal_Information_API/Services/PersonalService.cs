using Personal_Information_API.Models;
using System.Text.Json;

namespace Personal_Information_API.Services
{
    public class PersonalService
    {
        private string FileName => Path.Combine("Data", "Personal_Information.json");


        public IEnumerable<Personal_Information> GetPersonals()
        {
            var FileReader = File.OpenText(FileName);
            return JsonSerializer.Deserialize<Personal_Information[]>(FileReader.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

    }
}
