using Security.Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Core.Services
{
    public interface IMenuService: IServiceBase
    {
        IEnumerable<Menu> GetMenuForGivenRole(int roleId);
        List<Menu> GetAllMenus();
        Dictionary<Menu, int[]>  GetAllMenuWithRoles();
        void UpdateMenuAccessToRoles(Dictionary<Menu, int[]> menus);
    }
}
