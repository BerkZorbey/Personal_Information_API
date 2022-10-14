using Personal_Information_API.Models;
using System.Text.Json;

namespace Personal_Information_API.Services
{
    public class PersonalService
    {
        private string FileName => Path.Combine("Data", "Personal_Information.json");


        public IEnumerable<Personal_Information> GetPersonals()
        {
            var FileReader = File.ReadAllText(FileName);
           
            return JsonSerializer.Deserialize<Personal_Information[]>(FileReader, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
        public async Task CreateNewPersonalList(List<Personal_Information> personalList)
        {
            var outputStream = File.Create(FileName);
           
           await JsonSerializer.SerializeAsync(outputStream, personalList,new JsonSerializerOptions
           {
               WriteIndented = true,
           });
           outputStream.Close();
        }
    }
}
