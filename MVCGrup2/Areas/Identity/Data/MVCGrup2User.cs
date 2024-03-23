using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;

namespace MVCGrup2.Data;

// Add profile data for application users by adding properties to the MVCGrup2User class
public class MVCGrup2User : IdentityUser
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }

    [NotMapped]
    public string FullName => Name + " " + Surname;


    [Required]
    public Gender UserGender { get; set; }
    [Required]
    public DateOnly Birthday { get; set; }

    public ICollection<Order> Orders { get; set; }

 
}

