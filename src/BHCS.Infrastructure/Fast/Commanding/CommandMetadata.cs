using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection;

namespace BHCS.Infrastructure.Fast.Commanding
{
    public class CommandMetadataContainer
    {
        private readonly ConcurrentDictionary<string, CommandMetadata> _metadataDic = new ConcurrentDictionary<string, CommandMetadata>();

        public void AddMetadata(CommandMetadata metadata)
        {
            if (metadata == null) return;
            if (_metadataDic.ContainsKey(metadata.CommandType.Name)) _metadataDic[metadata.CommandType.Name] = metadata;
            else _metadataDic.TryAdd(metadata.CommandType.Name, metadata);
        }

        public CommandMetadata Get(string commandName)
        {
            CommandMetadata metadata;
            if (!_metadataDic.TryGetValue(commandName, out metadata)) return null;
            return metadata;
        }
    }

    public class CommandMetadata
    {
        public CommandMetadata(Type commandType, Type commandServicerType, MethodInfo commandMethod, bool isNeedReturn)
        {
            CommandType = commandType;
            CommandServicerType = commandServicerType;
            CommandMethod = commandMethod;
            IsNeedReturn = isNeedReturn;
        }

        public Type CommandType { get; set; }

        public Type CommandServicerType { get; set; }

        public MethodInfo CommandMethod { get; set; }

        public bool IsNeedReturn { get; set; }
    }
}
