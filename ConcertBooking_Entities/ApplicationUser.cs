using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// Identity: It is membership pgm which provides authentication(credentials) and authorization(editable:access right) features.
// Authentication Process: Register IdentityUser class - Id(Guid), UserName, Password, Email, Phone
// SignInManager - check whether user SignIn or not
// UserManager - store user data in dbs, get user info from dbs, add role to user
// IdentityRole has 4 property eg: Id, Name
// Claim: Piece of info about user eg: adhar card
// ClaimsIdentity: List<Claim> - List of Claim 


namespace ConcertBooking_Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? Phone { get; set; }
    }
}
