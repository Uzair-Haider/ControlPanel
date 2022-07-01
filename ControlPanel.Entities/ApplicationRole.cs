using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            Name = roleName;
            Description = roleName + $@" added on {DateTime.Now.ToShortDateString()}";
        }

        public string Description { get; set; }
    }
}
