using Personal_Information_API.Models;

namespace Personal_Information_API.Services
{
    public interface IPersonalService
    {
        public IEnumerable<Personal_Information> GetPersonals();
        public Task CreateNewPersonalList(List<Personal_Information> personalList);

    }
}
