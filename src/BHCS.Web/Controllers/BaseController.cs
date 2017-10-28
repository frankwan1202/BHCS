using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Web.Controllers
{
    public class BaseController:Controller
    {
        private readonly ICommandBus _commandBus;

        public BaseController()
        {
            _commandBus = ObjectContainer.Resolve<ICommandBus>();
        }

        protected ICommandBus CommandBus => _commandBus;
    }
}
