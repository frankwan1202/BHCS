using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Model
{
    public class EntitySet 
    {
        public EntitySet(IEntity entity, OperationState operationState)
        {
            Entity = entity;
            OperationState = operationState;
        }

        public IEntity Entity { get; private set; }

        public OperationState OperationState { get; private set; }
    }
}
