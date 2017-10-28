using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.Menus
{
    public class Page:AggregateRoot
    {
        public Page()
        {
        }

        public Page(Guid menuId,string name, string url)
        {
            Ensure.NotNullOrWhiteSpace(name, "名称不能为空！");
            Ensure.NotNullOrWhiteSpace(url, "地址不能为空！");
            Ensure.NotNull(menuId, "所属菜单不能为空！");

            PageId = Guid.NewGuid();
            MenuId = menuId;
            Name = name;
            Url = url;
        }

        public Guid PageId { get; set; }

        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
