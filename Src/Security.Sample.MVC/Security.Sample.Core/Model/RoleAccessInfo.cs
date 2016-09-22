using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Core.Model
{
    public class RoleAccessInfo : Entity
    {
        public Role Role { get; set; }
        public bool AccessFlag { get; set; }
    }
}
