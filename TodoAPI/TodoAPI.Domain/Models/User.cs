using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPI.Domain.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
    }
}
