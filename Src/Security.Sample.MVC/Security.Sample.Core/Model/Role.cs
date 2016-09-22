using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Sample.Core.Model
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role ReportingTo { get; set; }
    }
}
