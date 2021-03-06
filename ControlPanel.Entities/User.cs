using Helpers.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.Entities
{
    public class User : IdentityUser<Guid>
    {
        
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "User Name is required")]
        //public string UserName { get; set; }

        [Required]
        [MaxLength(11)]
        public string PersonalId { get; set; }
        public Byte[] Photo { get; set; }
        public string? MobileNumber { get; set; }

        /// <summary>
        /// 0 = Not known;
        /// 1 = Male;
        /// 2 = Female;
        /// 9 = Not applicable.
        /// </summary>
        [Required]
        public Gender Sex { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}