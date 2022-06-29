using ControlPanel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.IRepos.IAddressRepo
{
    public interface IAddressCreate
    {
        void CreateAddress(Guid userId,Address address);
    }
}
