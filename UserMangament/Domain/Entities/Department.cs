namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        public ICollection<Emp_Holidays_Order> Emp_Holidays_Orders { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
