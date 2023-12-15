namespace Domain.Entities
{
    public class Employment_Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<WorkingHour> WorkingHours { get; set; }

    }
}
