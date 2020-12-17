using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementDemo.Models
{
    public class TechnologyManagementModel
    {
        public IEnumerable<TechnologyDetailsTable> techKnown { get; set; }
        public IEnumerable<TechnologyDetailsTable> techUnKnown { get; set; }
        public EmployeeDetailsTable employee { get; set; }
    }
}