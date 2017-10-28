using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Infrastructure
{
    public interface IMessage
    {
        Guid Id { get; }
    }

    public class Message:IMessage
    {
        public Message()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
