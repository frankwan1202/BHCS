using BHCS.Domain.Models.Menus;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface IPageRepository:IRepository<Page>
    {
        bool IsExist(Guid menuId, string name);
    }
}
