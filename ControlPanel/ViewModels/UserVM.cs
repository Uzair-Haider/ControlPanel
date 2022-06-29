﻿using Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControlPanel.ViewModels
{
    public class UserVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
        public string? UserName { get; set; }
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
    }
}
