namespace Domain.Entities
{
    public class Employee : BaseEntity
    {

        public bool IsActive { get; set; }
        public int AccountCancellationStatusBy { get; set; }
        public int CreateBy { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public double Salary { get; set; }
        public int WorkingHourId { get; set; }
        public virtual WorkingHour WorkingHour { get; set; }
        public ICollection<Emp_Holidays_Order> Emp_Holidays_Orders { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

    }
}
