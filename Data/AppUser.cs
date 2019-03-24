using System;
using Microsoft.AspNetCore.Identity;

namespace MP.Data {
    public class AppUser : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPicUrl { get; set; }
        public DateTime Birthdate { get; set; }
    }
}