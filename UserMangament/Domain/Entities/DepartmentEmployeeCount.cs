namespace Domain.Entities
{
    public class DepartmentEmployeeCount
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DepartmentName { get; set; }
        public int EmployeeCount { get; set; }


    }

    public class DepartmentEmployeeCountParameters
    {
        public int DepartmentId { get; set; } = 0;
    }
}
