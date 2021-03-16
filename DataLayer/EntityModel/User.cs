using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.EntityModel
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password(Length >=5 & One Special Character)")]
        public string UserPassword { get; set; }

        public string UserRole { get; set; } 

        public virtual IList<Event> Events { get; set; }
    }
}
