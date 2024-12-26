﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Username is Required!")]
        [StringLength(20, ErrorMessage ="Username must be maximum {1} characters")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "{0} is Required!")]
        [StringLength(10, ErrorMessage = "{0} must be maximum {1} characters")]
        public string Password { get; set; }

        public bool IsActive { get; set; }


        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
