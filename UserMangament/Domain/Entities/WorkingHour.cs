namespace Domain.Entities
{
    public class WorkingHour
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public int Employment_TypeId { get; set; }
        public virtual Employment_Type Employment_Type { get; set; }
    }
}
