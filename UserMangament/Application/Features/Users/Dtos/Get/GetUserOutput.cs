﻿namespace Application.Features.Users.Dtos.Get
{
    public class GetUserOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = false;
        public int AccountCancellationStatusBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
