using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingTinderWepApi.Entities
{
    [Table("Users")]
    public class User
    {
        [Required(ErrorMessage = "Name is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(75, ErrorMessage = "Username cannot be longer than 75 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(250, ErrorMessage = "Email cannot be longer than 250 characters")]
        public string Email { get; set; }
    }
}
