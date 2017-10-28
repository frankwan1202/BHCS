using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.Menus
{
    public class Function:AggregateRoot
    {
        public Function()
        {
        }

        public Function(Guid pageId,string url, string name, int operationNum)
        {
            Ensure.NotNullOrWhiteSpace(url, "Url不能为空！");
            Ensure.NotNullOrWhiteSpace(name, "功能名称不能为空！");
            Ensure.NotNull(pageId, "所属页面不能为空！");
            Ensure.GrandThan(operationNum, 0, "操作值不能小于零！");

            FuncId = Guid.NewGuid();
            PageId = pageId;
            Url = url;
            Name = name;
            OperationNum = operationNum;
        }

        public Guid FuncId { get; set; }

        public Guid PageId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public int OperationNum { get; set; }
    }
}
