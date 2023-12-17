namespace Application.Features.Employees.Dtos.Get
{
    public class GetEmployeeOutput
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int AccountCancellationStatusBy { get; set; }
        public int CreateBy { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public double Salary { get; set; }
        public int WorkingHourId { get; set; }
    }
}
