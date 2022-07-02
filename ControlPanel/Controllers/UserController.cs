using ControlPanel.Entities;
using ControlPanel.IRepos.IAddressRepo;
using ControlPanel.IRepos.IUserAccountRepo;
using ControlPanel.IRepos.IUserRepo;
using ControlPanel.ViewModels;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using X.PagedList;

namespace ControlPanel.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserAccountCreate _userAccountCreate;
        private readonly IAddressCreate _addressCreate;
        private readonly IUserRetrieve _userRetrieve;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, IUserAccountCreate userAccountCreate, IAddressCreate addressCreate, IUserRetrieve userRetrieve)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userAccountCreate = userAccountCreate;
            _addressCreate = addressCreate;
            _userRetrieve = userRetrieve;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userExists = await _userManager.FindByNameAsync(model.UserName);
                    if (userExists != null)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                    User user = GetUserModel(model);

                    IdentityResult? result = await _userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                    //Creating User Accounts
                    CreateUserAccount(model, user);

                    //Createing User Address
                    CreateUserAddress(model, user);

                    //adding user role
                    await _userManager.AddToRoleAsync(user, UserRoles.User);

                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception occured in Register");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("register-admin")]
        //[Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userExists = await _userManager.FindByNameAsync(model.UserName);
                    if (userExists != null)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
                    User user = GetUserModel(model);

                    IdentityResult? result = await _userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                    //Creating User Accounts
                    CreateUserAccount(model, user);

                    //Createing User Address
                    CreateUserAddress(model, user);

                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                        await _roleManager.CreateAsync(new ApplicationRole(UserRoles.Admin));
                    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                        await _roleManager.CreateAsync(new ApplicationRole(UserRoles.User));

                    //Adding Admin role to user
                    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }

                    //adding User role to user
                    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }
                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private static User GetUserModel(UserVM model)
        {
            return new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                CreatedOn = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
                PersonalId = model.PersonalId,
                PhoneNumber = model.MobileNumber,
                Photo = model.Photo,
                Sex = model.Sex,
            };
        }

        private void CreateUserAccount(UserVM model, User user)
        {
            foreach (var item in model.AccountNumber)
            {
                UserAccount userAccount = new UserAccount()
                {
                    AccountNumber = item
                };
                _userAccountCreate.AccountCreate(user.Id, userAccount);
            }
        }

        private void CreateUserAddress(UserVM model, User user)
        {
            Address address = new Address()
            {
                Address1 = model.Address1,
                City = model.City,
                Country = model.Country,
                CreatedOn = DateTime.Now,
                Street = model.Street,
                ZipCode = model.ZipCode,
            };
            _addressCreate.CreateAddress(user.Id, address);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpGet]
        [Route("get-users")]
        [Authorize(Roles = UserRoles.Admin)]

        public async Task<GetUsersVM> GetUsers(string search, string orderBy, string orderOn, int pageNumber = 1, int pageSize = 20)
        {

            GetUsersVM usersVM = new GetUsersVM();
            try
            {
                IPagedList<User>? pagedList = await _userRetrieve.GetAllUser(search, orderBy, orderOn).Result.ToPagedListAsync(pageNumber, pageSize);
                usersVM.users = pagedList.ToList();
                usersVM.TotalCount = pagedList?.TotalItemCount ?? 0;
                usersVM.PageCount = pagedList?.PageCount ?? 0;
                usersVM.PageNumber = pagedList?.PageNumber ?? 0;
                usersVM.PageSize = pagedList?.PageSize ?? 0;
                usersVM.Count = pagedList?.Count ?? 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured in GetUsers");
            }
            return usersVM;
        }
    }
}