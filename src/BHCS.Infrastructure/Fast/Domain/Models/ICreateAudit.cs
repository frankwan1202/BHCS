using System;

namespace BHCS.Infrastructure.Fast.Domain.Models
{
    public interface ICreateAudit
    {
        DateTime CreateTime{get;set;}

        Guid CreateBy{get;set;}

        string CreateByName{get;set;}
    }
}