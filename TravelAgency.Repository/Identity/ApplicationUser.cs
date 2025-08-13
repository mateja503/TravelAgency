using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelAgency.Domain.Models;

namespace TravelAgency.Repository.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; } 
        public int CustomerId { get; set; }

    }
}
