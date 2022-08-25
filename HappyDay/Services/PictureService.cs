namespace HappyDay.Services
{
    public class PictureService : IPictureService
    {
        private readonly string _uploadFolder;

        public PictureService(IWebHostEnvironment environment)
        {
            _uploadFolder = Path.Combine(environment.WebRootPath, "uploads");            
        }

        public void DeleteByBirthdayId(string id)
        {
            string[] files = Directory.GetFiles(_uploadFolder);
            var path = files.Where(file => file.Contains(id)).FirstOrDefault();

            if (path == null)
            {
                return;
            }
            else
            {
                File.Delete(path);
            }
        }

        public string GetUrlByBirthdayId(string id)
        {
            string[] files = Directory.GetFiles(_uploadFolder);
            var path = files.Where(file => file.Contains(id)).FirstOrDefault();

            if(path == null)
            {
                path = "noImage.jpg";
            }

            path = "/uploads/" + path.Split('\\').Last();

            return path;
        }

        public void SaveByBirthdayId(IFormFile picture, string id)
        {
            var ext = System.IO.Path.GetExtension(picture.FileName);
            var uniqueFileName = $"user_{id}{ext}";            
            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using (var stream = File.OpenWrite(filePath))
            {
                picture.CopyTo(stream);
            }      
        }
    }
}
