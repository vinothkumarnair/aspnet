using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security.Sample.Core.Services;
using Security.Sample.Core.Model;
using Security.Sample.Core.Data;

namespace Security.Sample.Service
{
    public class MenuService : ServiceBase, IMenuService
    {
        private IUnitOfWork _unitOfWork;
        public MenuService()
        {
            _unitOfWork = UnitOfWork;
        }
        public IEnumerable<Menu> GetMenuForGivenRole(int currentUserRole)
        {
            int[] selectedMenuForRole = _unitOfWork.MenuMappedToRoleRepository.Where(i => i.Role.Id == currentUserRole && i.AccessFlag == true).Select(j => j.Menu.Id).ToArray();
            IEnumerable<Menu> menus = _unitOfWork.MenuRepository.GetAll().Where(i => selectedMenuForRole.Contains(i.Id)).OrderBy(j => j.Id);
            return menus;

        }

        public List<Menu> GetAllMenus()
        {
            List<Menu> menus = _unitOfWork.MenuRepository.GetAll().ToList();
            return menus;

        }

        public Dictionary<Menu, int[]> GetAllMenuWithRoles()
        {
            Dictionary<Menu, int[]> menusWithRole = new Dictionary<Menu, int[]>();
           
            IEnumerable<Menu> menus = _unitOfWork.MenuRepository.GetAll();
            foreach (var menu in menus)
            {
                int[] roleForMenu = _unitOfWork.MenuMappedToRoleRepository.Where(j=>j.Menu.Id==menu.Id).Select(k => k.Role.Id).ToArray();
                menusWithRole.Add(menu, roleForMenu);
            }
            return menusWithRole;
        }

        public void UpdateMenuAccessToRoles(Dictionary<Menu, int[]> menus)
        {
            //to:do - implement this later
        }

    }
}
