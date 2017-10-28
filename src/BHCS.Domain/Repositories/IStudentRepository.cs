using BHCS.Domain.Models.Users;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface IStudentRepository:IRepository<Student>
    {
        bool IsExistStudent(string studentNo);
    }
}
