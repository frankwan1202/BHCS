using BHCS.Infrastructure.Fast.Domain.UnitOfWorks;
using BHCS.Infrastructure.Fast.Infrastructure;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Commanding
{
    public class CommandHandler
    {
        private readonly CommandMetadataContainer _metadataContainer;
        private readonly IRepositoryContextManager _repositoryContextManager;

        public CommandHandler()
        {
            _metadataContainer = ObjectContainer.Resolve<CommandMetadataContainer>();
            _repositoryContextManager = ObjectContainer.Resolve<IRepositoryContextManager>();
        }

        public Task<ICommandResult> ExecuteAsync(ICommand command)
        {
            return Task.Run(() => { return Execute(command); });
        }

        public ICommandResult Execute(ICommand command)
        {
            try
            {
                var metadata = _metadataContainer.Get(command.GetType().Name);
                if (metadata == null) throw new NullReferenceException("The command message can not found metadata info!");
                var commandServicer = ObjectContainer.Resolve(metadata.CommandServicerType);
                if (commandServicer == null) throw new NullReferenceException("The command can not found command servicer!");

                using (var repositoryContext = _repositoryContextManager.Create())
                {
                    if (metadata.IsNeedReturn)
                    {
                        var commandResult = metadata.CommandMethod.Invoke(commandServicer, new object[] { command }) as ICommandResult;
                        repositoryContext.Commit();
                        return commandResult;
                    }
                    else
                    {
                        metadata.CommandMethod.Invoke(commandServicer, new object[] { command });
                        repositoryContext.Commit();
                        return new CommandResult(ResultCode.Ok, "该命令无返回值");
                    }
                }
            }
            catch (Exception ex)
            {
                if(ex is EnsureException) return new CommandResult(ResultCode.BussinessError, ex.Message);
                if (ex.InnerException!=null&&ex.InnerException is EnsureException) return new CommandResult(ResultCode.BussinessError, ex.InnerException.Message);
                return new CommandResult(ResultCode.Exception, ex.Message);
            }
        }
    }
}
