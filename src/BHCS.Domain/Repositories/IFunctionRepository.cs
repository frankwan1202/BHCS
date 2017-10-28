using BHCS.Domain.Models.Menus;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface IFunctionRepository:IRepository<Function>
    {
        bool IsExist(Guid pageId, string name);
    }
}
