using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.Utilities
{
    public interface IUserSession
    {
        Guid UserId { get; }

        string Account { get; }

        string UserName { get; }
        
    }

    public class NullUserSession : IUserSession
    {
        private readonly Guid _userID;

        public NullUserSession()
        {
            _userID = Guid.NewGuid();
        }

        public Guid UserId => _userID;
        public string Account => "";
        public string UserName => "";
    }
}
