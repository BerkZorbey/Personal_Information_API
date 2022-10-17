using Personal_Information_API.Models;

namespace Personal_Information_API.Services
{
    public interface ILoggerService
    {
        IEnumerable<Log> LogRead();
        public Task LogWrite(List<Log> logList);
    }
}
