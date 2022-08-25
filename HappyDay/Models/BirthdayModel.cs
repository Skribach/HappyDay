using HappyDay.Validation;
using System.ComponentModel.DataAnnotations;

namespace HappyDay.Models
{
    public class BirthdayModel
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The first name should be less than 30 symbols")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The last name should be less than 30 symbols")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Birthday")]
        public DateTime? BiDay { get; set; }

        //TODO: Move to appsettings?
        /*[Required]*/
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".bmp" })]
        public IFormFile? Photo { get; set; }

        public string PhotoUrl {get; set;}
    }
}
