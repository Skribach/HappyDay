using HappyDay.Domain;
using HappyDay.Models;
using HappyDay.Repositories;

namespace HappyDay.Services
{
    public interface IBirthdayService
    {        
        public void Add(BirthdayModel birthday);
        public void Edit(BirthdayModel birthday);
        public void DeleteById(string id);
        public BirthdayModel GetById(string id);
        public BirthdaysModel GetByUserId(string userId);
    }
}
