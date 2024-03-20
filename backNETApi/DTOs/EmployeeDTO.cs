namespace backNETApi.DTOs
{
    public class employeeDTO
    {

        public int IdEmployee { get; set; }

        public int IdDepartment { get; set; }

        public string DepartmentName { get; set; }

        public string EmpoyeeName { get; set; } = null!;

        public string EmployeeSurname { get; set; } = null!;

        public double Salary { get; set; }

        public string ContractDate { get; set; }
    }
}
