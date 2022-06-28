using ControlPanel.Entities;
using ControlPanel.IRepos;
using ControlPanel.IRepos.IUserRepo;

namespace ControlPanel.Repos.UserRepo
{
    public class UserCreateRepo : IUserCreateRepo
    {
        public UserCreateRepo()
        {

        }
        public void CreateUser(User user,Guid AddressId)
        {
            try
            {
                if(user!=null)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}