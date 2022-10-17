using Personal_Information_API.Models;
using System.Text.Json;

namespace Personal_Information_API.Services
{
    public class LoggerService : ILoggerService
    {
        
        private string FileName => Path.Combine("Data", "Log_Message.json");
        
        public IEnumerable<Log> LogRead()
        {
            var FileReader = File.ReadAllText(FileName);

            return JsonSerializer.Deserialize<Log[]>(FileReader, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
        public async Task LogWrite(List<Log> logList)
        {
            var outputStream = File.OpenWrite(FileName);
            await JsonSerializer.SerializeAsync(outputStream, logList,new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            outputStream.Close();
        }
    }
}
