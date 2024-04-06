using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Identity: It is membership pgm which provides authentication(credentials) and authorization(editable:access right) features.
//Authentication Process: Register IdentityUser class - Id(Guid), UserName, Password, Email, Phone
                        // SignInManager - check whether user SignIn or not

namespace ConcertBooking_Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
    }
}
