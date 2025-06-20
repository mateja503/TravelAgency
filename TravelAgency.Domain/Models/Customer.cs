using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.ValueObjects;

namespace TravelAgency.Domain.Models
{
    public class Customer 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Address? Address { get; set; }

    }
}
