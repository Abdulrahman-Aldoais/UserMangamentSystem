using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        //تم استخدام طريقة التحقق من صحة البيانات عن طريق fluent validation and data annotations
        // fluent validation => fluent validation
        [Required(ErrorMessage = "حقل مطلوب")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [Range(22, 60, ErrorMessage = "الرجاء إدخال قيمة بين 1 و 100")]
        public int Age { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression(@"^(\+?967)?\d{9}$", ErrorMessage = "الرجاء إدخال رقم الهاتف اليمني بالصيغة الصحيحة")]
        public string Phone { get; set; }
        public bool IsActive { get; set; } = false;
        public int AccountCancellationStatusBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public ICollection<Attendance> Attendances { get; set; }


    }
}
