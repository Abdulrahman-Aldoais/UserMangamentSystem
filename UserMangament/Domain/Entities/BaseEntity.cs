using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [StringLength(50, ErrorMessage = "يجب أن يكون الحقل بين {30} و {10} حرف.", MinimumLength = 30)]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
