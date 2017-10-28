using BHCS.Infrastructure.FastCommon.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Commanding
{
    public interface ICommandBus
    {
        ICommandResult Send(ICommand command);

        Task<ICommandResult> SendAsync(ICommand command);

        void SendOneWay(ICommand command);
    }

    public class CommandBus : ICommandBus
    {
        private readonly CommandHandler _commandHandler;

        public CommandBus()
        {
            _commandHandler = ObjectContainer.Resolve<CommandHandler>();
        }

        public ICommandResult Send(ICommand command)
        {
            return _commandHandler.Execute(command);
        }

        public async Task<ICommandResult> SendAsync(ICommand command)
        {
            return await _commandHandler.ExecuteAsync(command);
        }

        public void SendOneWay(ICommand command)
        {
            _commandHandler.Execute(command);
        }
    }
}
