namespace Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime AttendTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


        /*
         يتم منح الموظفين حسابات بصلاحيات معينة للدخول الى النظام واداء المهام 
         وتسجيل الحظور والغياب 


         يمكن للمستخدم ان يدخل على النظام وتقديم طلب اجازة عن طريق (رقم المستخدم) و يمكنه التواصل مع مسؤول الموارد البشرية 
         او من يملك صلاحية التقديم طلب اجازة وإعطائه (الرقم الوظيفي) الخاص به 
         

         */


    }
}
