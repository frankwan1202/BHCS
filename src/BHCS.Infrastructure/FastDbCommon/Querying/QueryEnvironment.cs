using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying
{
    public class QueryEnvironment
    {
        private IInterpreterExecutor _interpreterExecutor;
        private IInterpreterProvider _interpreterProvider;

        public static readonly QueryEnvironment Current = new QueryEnvironment();

        private QueryEnvironment()
        {
            _interpreterExecutor = ObjectContainer.Resolve<IInterpreterExecutor>();
            _interpreterProvider = ObjectContainer.Resolve<IInterpreterProvider>();
        }

        public IInterpreterExecutor InterpreterExecutor { get { return this._interpreterExecutor; } }

        public IInterpreterProvider InterpreterProvider { get { return this._interpreterProvider; } }

        public FromSection<TEntity> GetFromSection<TEntity>()where TEntity:class
        {
            return new FromSection<TEntity>(_interpreterProvider, _interpreterExecutor);
        }

        public QuickSqlSection GetQuickSqlSection() 
        {
            return new QuickSqlSection(_interpreterExecutor, _interpreterProvider);
        }
        
    }
}
