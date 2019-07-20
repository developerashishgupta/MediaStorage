﻿using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Menu;
using MediaStorage.Data;
using MediaStorage.Data.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MediaStorage.Service
{

    public class MenuService : IMenuService
    {
        private UnitOfWork uow;
        private Repository<Menu> menuRepository;
        private Repository<MenuItem> menuItemRepository;

        public MenuService()
        {
            MediaContext context = new MediaContext();
            this.uow = new UnitOfWork(context); 
            this.menuRepository = new Repository<Menu>(context);
            this.menuItemRepository = new Repository<MenuItem>(context);
        }

        public List<MenuViewModel> GetAllMenus()
        {
            List<MenuViewModel> data = null;
            var canGetAllMenuValue = ConfigurationManager.AppSettings["CanGetAllMenus"];
            bool canGetAllMenu;
            if (bool.TryParse(canGetAllMenuValue, out canGetAllMenu))
            {

                data = menuRepository
                   .GetAll()
                   .Select(s => new MenuViewModel
                   {
                       Id = s.Id,
                       Name = s.Name,
                       Description = s.Description
                   }).ToList();
            }
            return data;
        }

        public List<CustomSelectListItem> GetAllMenusBySelectListItem(int? id)
        {
            return menuRepository
                .GetAll()
                .Select(s => new CustomSelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = id.HasValue ? s.MenuItems.Any(a => a.Id == id.Value) : false
                }).ToList();
        }

        public MenuViewModel GetMenuById(int id)
        {
            var menu = menuRepository.Find(id);
            return menu == null ? null : new MenuViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description
            };
        }

        public ServiceResult AddMenu(MenuViewModel entity)
        {
            menuRepository.Add(new Menu
            {
                Name = entity.Name,
                Description = entity.Description
            });

            return ServiceResult.GetAddResult(uow.Commit() == 1);
        }

        public ServiceResult UpdateMenu(MenuViewModel entity)
        {
            menuRepository.Update(new Menu
            {
                Id = entity.Id.Value,
                Name = entity.Name,
                Description = entity.Description
            });

            return ServiceResult.GetUpdateResult(uow.Commit() == 1);
        }

        public ServiceResult RemoveMenu(int id, bool cascadeRemove = false)
        {
            if (cascadeRemove)
            {
                var menuItems = menuItemRepository.GetAll(w => w.MenuId == id, i => i.UserRoles).ToList();
                if (menuItems.Count > 0)
                    menuItemRepository.DeleteRange(menuItems);
            }
            menuRepository.Delete(id);
            return ServiceResult.GetRemoveResult(uow.Commit() > 0);
        }
    }
}
