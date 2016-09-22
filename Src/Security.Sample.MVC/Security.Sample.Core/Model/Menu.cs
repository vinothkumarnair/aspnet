using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Core.Model
{
    public class Menu : Entity
    {
        public string MenuName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        public string LeftMenuIcon { get; set; }
    }
}
