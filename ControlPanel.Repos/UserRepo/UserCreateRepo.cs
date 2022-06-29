﻿using ControlPanel.DAL;
using ControlPanel.Entities;
using ControlPanel.IRepos;
using ControlPanel.IRepos.IUserRepo;
using Microsoft.Extensions.Logging;

namespace ControlPanel.Repos.UserRepo
{
    public class UserCreateRepo : IUserCreateRepo
    {
        private readonly ILogger _logger;
        private readonly CPContext _db;
        public UserCreateRepo(ILogger<UserCreateRepo> logger, CPContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void CreateUser(User user)
        {
            try
            {
                _db.Users.Add(user); 
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in CreateUser");
            }
        }

    }
}