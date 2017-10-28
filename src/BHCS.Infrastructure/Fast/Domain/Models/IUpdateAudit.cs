using System;

namespace BHCS.Infrastructure.Fast.Domain.Models
{
    public interface IUpdateAudit
    {
        DateTime UpdateTime{get;set;}

        Guid UpdateBy{get;set;}

        string UpdateByName{get;set;}
    }
}