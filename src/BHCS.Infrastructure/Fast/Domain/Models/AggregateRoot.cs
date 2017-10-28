using BHCS.Infrastructure.FastDbCommon.Persistents.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Domain.Models
{
    public interface IAggregateRoot:IEntity
    {
    }

    public class AggregateRoot:IAggregateRoot
    { }
}
