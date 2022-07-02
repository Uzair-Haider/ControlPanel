using ControlPanel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.IRepos.IUserRepo
{
    public interface IUserCreate
    {
        void CreateUser(User user);
    }
}
