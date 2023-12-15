namespace Domain.Entities
{
    public class Officiol_Holidays
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Year { get; set; }
        public int UserId { get; set; }
        public bool IsStatic { get; set; }
    }
}
