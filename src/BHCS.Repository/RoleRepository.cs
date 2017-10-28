using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repository.EntityFramework
{
    public class RoleRepository:EntityFrameworkRepository<Role>,IRoleRepository
    {
    }
}
