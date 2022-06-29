using ControlPanel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.DAL
{
    public class CPContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public CPContext(DbContextOptions<CPContext> options) : base(options)
        {
        }
        public DbSet<UserAccount> UserAccounts  { get; set; }
        public DbSet<Address> Addresses  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
