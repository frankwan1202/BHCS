using BHCS.Application.Commanding;
using BHCS.Domain.Models.Menus;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.CommandingServicers
{
    public class PageCommandServicer:ICommandServicer
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IFunctionRepository _functionRepository;

        public PageCommandServicer(IMenuRepository menuRepository,IPageRepository pageRepository,IFunctionRepository functionRepository)
        {
            _menuRepository = menuRepository;
            _pageRepository = pageRepository;
            _functionRepository = functionRepository;
        }

        public ICommandResult CreateNewMenu(CreateNewMenuCommand command)
        {
            var menu = new Menu(command.Name, command.Sort);
            Ensure.MustBeFalse(_menuRepository.IsExist(menu.Name), "该菜单名已存在！");
            _menuRepository.Insert(menu);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }

        public ICommandResult CreateNewPage(CreateNewPageCommand command)
        {
            var page = new Page(command.MenuId,command.Name, command.Url);
            Ensure.MustBeFalse(_pageRepository.IsExist(page.MenuId,page.Name), "该菜单下页面已存在！");
            _pageRepository.Insert(page);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }

        public ICommandResult CreateNewFunction(CreateNewFunctionCommand command)
        {
            var function = new Function(command.PageId,command.Url, command.Name, command.OperationNum);
            Ensure.MustBeFalse(_functionRepository.IsExist(function.PageId, function.Name), "该功能已存在！");
            _functionRepository.Insert(function);
            return new CommandResult(Infrastructure.Fast.Infrastructure.ResultCode.Ok, "操作成功！");
        }
    }
}
