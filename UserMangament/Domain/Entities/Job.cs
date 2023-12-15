﻿namespace Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
