using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementDemo.Models
{
    public class ProjectMappingModel
    {
        public IEnumerable<EmployeeDetailsTable> ProjectEmployees { get; set; }
        public IEnumerable<EmployeeDetailsTable> NonProjectEmployees { get; set; }
        public ProjectDetailsTable Project { get; set; }
    }
}