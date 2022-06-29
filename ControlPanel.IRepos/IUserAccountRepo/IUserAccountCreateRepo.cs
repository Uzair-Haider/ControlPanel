﻿using ControlPanel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.IRepos.IUserAccountRepo
{
    public interface IUserAccountCreateRepo
    {
        void AccountCreate(Guid userId, UserAccount userAccount);
    }
}
