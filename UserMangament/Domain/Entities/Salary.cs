using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public int EmployeeId { get; set; }
        public Employee Empoloey { get; set; }
        public double Amount { get; set; }
        public DateTime SalaryDate { get; set; } = DateTime.Today;
    }
}
