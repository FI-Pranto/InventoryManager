﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IRepositories.Common
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
