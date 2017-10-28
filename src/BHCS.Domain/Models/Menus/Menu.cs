using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Models.Menus
{
    public class Menu:AggregateRoot
    {
        public Menu()
        { }

        public Menu(string name,int sort)
        {
            Ensure.NotNullOrWhiteSpace(name, "名称不能为空！");
            Ensure.GrandThan(sort, 0, "排序序号不能小于零！", false);

            MenuId = Guid.NewGuid();
            Name = name;
            Sort = sort;
        }

        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
