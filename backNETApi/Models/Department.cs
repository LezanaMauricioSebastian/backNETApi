using System;
using System.Collections.Generic;

namespace backNETApi.Models;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
