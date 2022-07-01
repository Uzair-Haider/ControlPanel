using ControlPanel.DAL;
using ControlPanel.Entities;
using ControlPanel.IRepos.IUserAccountRepo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Repos.UserAccountRepo
{
    public class UserAccountCreate : IUserAccountCreate
    {
        private readonly ILogger _logger;
        private readonly CPContext _db;
        public UserAccountCreate(ILogger<UserAccountCreate> logger, CPContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void AccountCreate(Guid userId, UserAccount userAccount)
        {
            try
            {
                userAccount.UserId = userId;
                _db.UserAccounts.Add(userAccount);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in AccountCreate");
            }
        }
    }
}
