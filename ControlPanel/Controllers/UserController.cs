using ControlPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ControlPanel.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool AddUser(UserVM user)
        {
            bool isUserCreated = false;
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in AddUser");
            }
            return isUserCreated;
        }
    }
}