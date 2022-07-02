using ControlPanel.DAL;
using ControlPanel.Entities;
using ControlPanel.IRepos.IUserRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Repos.UserRepo
{
    public class UserRetrieve : IUserRetrieve
    {
        private readonly ILogger _logger;
        private readonly CPContext _db;
        private readonly IConfiguration _configuration;

        public UserRetrieve(ILogger<UserRetrieve> logger, CPContext db, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _configuration = configuration;
        }

        public async Task<IQueryable<User>> GetAllUser(string search, string orderBy, string orderOn)
        {
            IQueryable<User>? query = _db.Users.AsQueryable();
            try
            {

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(w => w.FirstName.Contains(search)
                    || w.LastName.Contains(search)
                    || w.UserName.Contains(search)
                    || w.Email.Contains(search)
                    || w.MobileNumber.Contains(search)
                    || w.PersonalId.Contains(search));
                switch (orderOn)
                {
                    case "firstName":
                        if (orderBy == "asc")
                        {
                            query = query.OrderBy(o => o.FirstName);
                        }
                        else if (orderBy == "desc")
                        {
                            query = query.OrderByDescending(o => o.FirstName);
                        }
                        break;
                    case "lastName":
                        if (orderBy == "asc")
                        {
                            query = query.OrderBy(o => o.LastName);
                        }
                        else if (orderBy == "desc")
                        {
                            query = query.OrderByDescending(o => o.LastName);
                        }
                        break;
                    case "userName":
                        if (orderBy == "asc")
                        {
                            query = query.OrderBy(o => o.UserName);
                        }
                        else if (orderBy == "desc")
                        {
                            query = query.OrderByDescending(o => o.UserName);
                        }
                        break;
                    case "email":
                        if (orderBy == "asc")
                        {
                            query = query.OrderBy(o => o.Email);
                        }
                        else if (orderBy == "desc")
                        {
                            query = query.OrderByDescending(o => o.Email);
                        }
                        break;
                    default:
                        query = query.OrderByDescending(o => o.CreatedOn);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in GetAllUser");
            }
            return query;
        }
    }
}
