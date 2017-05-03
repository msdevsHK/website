using MSDevsHK.Website.Data.Models;
using System.Threading.Tasks;

namespace MSDevsHK.Website.Data
{
    public interface IDataRepository
    {
        Task CreateUser(User user);
        User GetUserByMeetupId(string meetupId);
    }
}