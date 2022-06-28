using ControlPanel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.DAL
{
    public class CPContext : DbContext
    {
        public CPContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users  { get; set; }
        public DbSet<Account> Accounts  { get; set; }
        public DbSet<UserAccount> UserAccounts  { get; set; }
        public DbSet<Address> Addresses  { get; set; }
    }
}
