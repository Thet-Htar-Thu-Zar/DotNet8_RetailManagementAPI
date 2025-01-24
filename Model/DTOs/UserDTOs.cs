﻿namespace MODEL.DTOs
{
    public class LoginUserDTOs
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class CreateUserDTOs
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public decimal? Amount { get; set; }
        public string UserRole { get; set; }
        public string? CreatedBy { get; set; }
    }
}
