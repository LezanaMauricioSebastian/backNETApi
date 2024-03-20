using System;
using System.Collections.Generic;

namespace backNETApi.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public int IdDepartment { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public double Salary { get; set; }

    public DateTime ContractDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;
}
