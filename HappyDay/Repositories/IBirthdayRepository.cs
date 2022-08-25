using HappyDay.Domain;

namespace HappyDay.Repositories
{
    public interface IBirthdayRepository
    {
        Birthday Add(Birthday birthday);
        Birthday GetById(string id);
        IEnumerable<Birthday> GetByUserId(string UserId);
        void Update(Birthday birthday);
        void DeleteById(string Id);
    }
}
