using Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControlPanel.ViewModels
{
    public class UserVM
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(11)]
        public string PersonalId { get; set; }

        
        public Byte[] Photo { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? MobileNumber { get; set; }

        /// <summary>
        /// 0 = Not known;
        /// 1 = Male;
        /// 2 = Female;
        /// 9 = Not applicable.
        /// </summary>
        [Required]
        public Gender Sex { get; set; }

        public string Address1 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public List<string> AccountNumber { get; set; }
    }
}
