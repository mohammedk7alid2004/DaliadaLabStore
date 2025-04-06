using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class User:IdentityUser
    {
        public string Address { get; set; }=string.Empty;
    }
}
