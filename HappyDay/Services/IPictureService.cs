namespace HappyDay.Services
{
    public interface IPictureService
    {
        void SaveByBirthdayId(IFormFile image, string userId);
        string GetUrlByBirthdayId(string userId);
        void DeleteByBirthdayId(string userId);
    }
}
