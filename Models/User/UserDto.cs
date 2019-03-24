using System;
using System.Collections.Generic;

namespace MP.Models.User {
    public class UserDto {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPicUrl { get; set; }
        public DateTime Birthdate { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        
        public List<string> UserClaims { get; set; }
        public List<string> UserRoles { get; set; }
    }
}