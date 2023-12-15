namespace Domain.Entities
{
    public class Emp_Holidays_Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsAccepted { get; set; }
        public int ProccedByUserId { get; set; }
        public int HolidaysTypeId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }



    }
}
