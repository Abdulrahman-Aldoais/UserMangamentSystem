namespace Domain.Entities
{
    public class Emp_Holidays_Stock
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int HolidayTotal { get; set; }
        public int HolidayRest { get; set; }
        public DateTime Years { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);




    }
}
