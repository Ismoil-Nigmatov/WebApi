﻿namespace project.Dto
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<int> CourseIds { get; set; }
    }
}
