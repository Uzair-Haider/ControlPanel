using ControlPanel.DAL;
using ControlPanel.Entities;
using ControlPanel.IRepos.IAddressRepo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Repos.AddressRepo
{
    public class AddressCreate : IAddressCreate
    {
        private readonly ILogger _logger;
        private readonly CPContext _db;
        public AddressCreate(ILogger<AddressCreate> logger, CPContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void CreateAddress(Guid userId, Address address)
        {
            try
            {
                address.UserId = userId;
                _db.Addresses.Add(address);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in CreateAddress");
            }
        }
    }
}
